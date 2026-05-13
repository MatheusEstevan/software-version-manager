using Microsoft.EntityFrameworkCore;
using SoftwareVersionManager.Test.Common.Fixtures;

namespace SoftwareVersionManager.Test.Data.Context;

[TestFixture]
public sealed class SoftwareVersionManagerDbContextTests
{
    [Test]
    public async Task DbSets_AreAvailable_ForSoftwareAndVersions()
    {
        await using var context = InMemoryDbContextFactory.CreateContext();

        Assert.That(context.Software, Is.Not.Null);
        Assert.That(context.Versions, Is.Not.Null);
        Assert.That(await context.Software.ToListAsync(), Is.Empty);
    }
}
