using Microsoft.EntityFrameworkCore;
using SoftwareVersionManager.Data.Repositories;
using SoftwareVersionManager.Entities.Models;
using SoftwareVersionManager.Test.Common.Builders;
using SoftwareVersionManager.Test.Common.Fixtures;

namespace SoftwareVersionManager.Test.Data.Repositories;

[TestFixture]
public sealed class SoftwareRepositoryTests
{
    [Test]
    public async Task CreateAsync_PersistsAndReturnsEntity()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var sut = new SoftwareRepository(context);
        var entity = new SoftwareBuilder().Build();

        var result = await sut.CreateAsync(entity);

        Assert.That(result.Id, Is.Not.EqualTo(0));
        Assert.That(await context.Software.CountAsync(), Is.EqualTo(1));
    }

    [Test]
    public async Task ListAsync_ReturnsAllSoftware()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        context.Software.Add(new SoftwareBuilder().WithName("A").Build());
        context.Software.Add(new SoftwareBuilder().WithName("B").Build());
        await context.SaveChangesAsync();
        var sut = new SoftwareRepository(context);

        var list = (await sut.ListAsync()).ToList();

        Assert.That(list, Has.Count.EqualTo(2));
        Assert.That(list.Select(s => s.Name), Is.EquivalentTo(new[] { "A", "B" }));
    }

    [Test]
    public async Task GetByIdAsync_ReturnsEntity_WhenExists()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var entity = new SoftwareBuilder().Build();
        context.Software.Add(entity);
        await context.SaveChangesAsync();
        var sut = new SoftwareRepository(context);

        var found = await sut.GetByIdAsync(entity.Id);

        Assert.That(found, Is.Not.Null);
        Assert.That(found!.Name, Is.EqualTo(entity.Name));
    }

    [Test]
    public async Task GetByIdAsync_ReturnsNull_WhenMissing()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var sut = new SoftwareRepository(context);

        var found = await sut.GetByIdAsync(999);

        Assert.That(found, Is.Null);
    }

    [Test]
    public async Task UpdateAsync_ModifiesEntity()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var entity = new SoftwareBuilder().WithName("Old").Build();
        context.Software.Add(entity);
        await context.SaveChangesAsync();
        var sut = new SoftwareRepository(context);
        entity.Name = "New";

        await sut.UpdateAsync(entity);

        var reloaded = await context.Software.AsNoTracking().SingleAsync();
        Assert.That(reloaded.Name, Is.EqualTo("New"));
    }

    [Test]
    public async Task DeleteAsync_RemovesEntity_ReturnsPositive()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var entity = new SoftwareBuilder().Build();
        context.Software.Add(entity);
        await context.SaveChangesAsync();
        var sut = new SoftwareRepository(context);

        var affected = await sut.DeleteAsync(entity.Id);

        Assert.That(affected, Is.GreaterThan(0));
        Assert.That(await context.Software.CountAsync(), Is.Zero);
    }

    [Test]
    public async Task DeleteAsync_WhenNotFound_ReturnsMinusOne()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var sut = new SoftwareRepository(context);

        var affected = await sut.DeleteAsync(404);

        Assert.That(affected, Is.EqualTo(-1));
    }

    [Test]
    public async Task CreateRangeAsync_AddsMultiple()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var sut = new SoftwareRepository(context);
        var batch = new List<Software>
        {
            new SoftwareBuilder().WithName("One").Build(),
            new SoftwareBuilder().WithName("Two").Build()
        };

        var result = await sut.CreateRangeAsync(batch);

        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(await context.Software.CountAsync(), Is.EqualTo(2));
    }

    [Test]
    public async Task RemoveRangeAsync_RemovesEntities()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();
        var a = new SoftwareBuilder().WithName("X").Build();
        var b = new SoftwareBuilder().WithName("Y").Build();
        context.Software.AddRange(a, b);
        await context.SaveChangesAsync();
        var sut = new SoftwareRepository(context);
        var toRemove = await context.Software.ToListAsync();

        await sut.RemoveRangeAsync(toRemove);

        Assert.That(await context.Software.CountAsync(), Is.Zero);
    }
}
