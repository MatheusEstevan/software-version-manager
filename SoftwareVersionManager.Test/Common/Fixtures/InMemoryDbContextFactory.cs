using Microsoft.EntityFrameworkCore;
using SoftwareVersionManager.Data.Context;

namespace SoftwareVersionManager.Test.Common.Fixtures;

/// <summary>
/// Fábrica para <see cref="SoftwareVersionManagerDbContext"/> isolado por teste (InMemory).
/// </summary>
public static class InMemoryDbContextFactory
{
    public static SoftwareVersionManagerDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<SoftwareVersionManagerDbContext>()
            .UseInMemoryDatabase($"test-db-{Guid.NewGuid():N}")
            .Options;

        return new SoftwareVersionManagerDbContext(options);
    }
}
