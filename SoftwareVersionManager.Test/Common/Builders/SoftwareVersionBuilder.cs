using SoftwareVersionManager.Entities.Models;

namespace SoftwareVersionManager.Test.Common.Builders;

/// <summary>
/// Object Mother / Builder para <see cref="SoftwareVersion"/> em cenários de teste.
/// </summary>
public sealed class SoftwareVersionBuilder
{
    private int _id;
    private string? _versionNumber = "1.0.0";
    private DateTime _releaseDate = new(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc);
    private int _softwareId = 1;
    private bool _deprecated;
    private string? _description = "Release notes";

    public SoftwareVersionBuilder WithVersionNumber(string? versionNumber)
    {
        _versionNumber = versionNumber;
        return this;
    }

    public SoftwareVersionBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public SoftwareVersionBuilder ForSoftware(int softwareId)
    {
        _softwareId = softwareId;
        return this;
    }

    public SoftwareVersionBuilder Deprecated(bool value = true)
    {
        _deprecated = value;
        return this;
    }

    public SoftwareVersionBuilder WithReleaseDate(DateTime releaseDate)
    {
        _releaseDate = releaseDate;
        return this;
    }

    public SoftwareVersion Build() => new SoftwareVersion
    {
        Id = _id,
        VersionNumber = _versionNumber,
        ReleaseDate = _releaseDate,
        SoftwareId = _softwareId,
        isDeprecated = _deprecated,
        Description = _description
    };
}
