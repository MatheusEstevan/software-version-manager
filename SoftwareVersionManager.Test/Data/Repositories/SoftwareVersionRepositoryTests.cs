using Microsoft.EntityFrameworkCore;
using SoftwareVersionManager.Data.Repositories;
using SoftwareVersionManager.Entities.Models;
using SoftwareVersionManager.Test.Common.Builders;
using SoftwareVersionManager.Test.Common.Fixtures;

namespace SoftwareVersionManager.Test.Data.Repositories;

[TestFixture]
public sealed class SoftwareVersionRepositoryTests
{
    [Test]
    public async Task CreateAsync_PersistsVersion_LinkedToSoftware()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var software = new SoftwareBuilder().Build();
        context.Software.Add(software);
        await context.SaveChangesAsync();
        var sut = new SoftwareVersionRepository(context);
        var version = new SoftwareVersionBuilder().ForSoftware(software.Id).Build();

        var result = await sut.CreateAsync(version);

        Assert.That(result.Id, Is.Not.EqualTo(0));
        Assert.That(await context.Versions.CountAsync(), Is.EqualTo(1));
    }

    [Test]
    public async Task ListAsync_ReturnsVersions()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var software = new SoftwareBuilder().Build();
        context.Software.Add(software);
        await context.SaveChangesAsync();
        context.Versions.Add(new SoftwareVersionBuilder().ForSoftware(software.Id).WithVersionNumber("1.0").Build());
        context.Versions.Add(new SoftwareVersionBuilder().ForSoftware(software.Id).WithVersionNumber("2.0").Build());
        await context.SaveChangesAsync();
        var sut = new SoftwareVersionRepository(context);

        var list = (await sut.ListAsync()).ToList();

        Assert.That(list, Has.Count.EqualTo(2));
        Assert.That(list.Select(v => v.VersionNumber), Is.EquivalentTo(new[] { "1.0", "2.0" }));
    }

    [Test]
    public async Task UpdateAsync_UpdatesDeprecatedAndReleaseDate()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var software = new SoftwareBuilder().Build();
        context.Software.Add(software);
        await context.SaveChangesAsync();
        var version = new SoftwareVersionBuilder().ForSoftware(software.Id).Build();
        context.Versions.Add(version);
        await context.SaveChangesAsync();
        var sut = new SoftwareVersionRepository(context);
        version.isDeprecated = true;
        version.ReleaseDate = new DateTime(2026, 5, 1, 0, 0, 0, DateTimeKind.Utc);

        await sut.UpdateAsync(version);

        var reloaded = await context.Versions.AsNoTracking().SingleAsync();
        Assert.That(reloaded.isDeprecated, Is.True);
        Assert.That(reloaded.ReleaseDate.Year, Is.EqualTo(2026));
    }

    [Test]
    public async Task DeleteAsync_RemovesVersion()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var software = new SoftwareBuilder().Build();
        context.Software.Add(software);
        await context.SaveChangesAsync();
        var version = new SoftwareVersionBuilder().ForSoftware(software.Id).Build();
        context.Versions.Add(version);
        await context.SaveChangesAsync();
        var sut = new SoftwareVersionRepository(context);

        var affected = await sut.DeleteAsync(version.Id);

        Assert.That(affected, Is.GreaterThan(0));
        Assert.That(await context.Versions.CountAsync(), Is.Zero);
    }
}
