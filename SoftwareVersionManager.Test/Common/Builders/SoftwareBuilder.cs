using SoftwareVersionManager.Entities.Models;

namespace SoftwareVersionManager.Test.Common.Builders;

/// <summary>
/// Object Mother / Builder para <see cref="Software"/> em cenários de teste.
/// </summary>
public sealed class SoftwareBuilder
{
    private int _id;
    private string _name = "Test Software";
    private string _description = "Description";
    private string _author = "Author";

    public SoftwareBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public SoftwareBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public Software Build() => new Software
    {
        Id = _id,
        Name = _name,
        Description = _description,
        Author = _author
    };
}
