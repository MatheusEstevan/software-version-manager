using Moq;
using SoftwareVersionManager.Domain.Interfaces;
using SoftwareVersionManager.Domain.Services;
using SoftwareVersionManager.Entities.Models;
using SoftwareVersionManager.Test.Common.Builders;

namespace SoftwareVersionManager.Test.Domain.Services;

[TestFixture]
public sealed class SoftwareVersionServiceTests
{
    private Mock<IRepository<SoftwareVersion>> _repository = null!;
    private SoftwareVersionService _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<IRepository<SoftwareVersion>>(MockBehavior.Strict);
        _sut = new SoftwareVersionService(_repository.Object);
    }

    [Test]
    public async Task ListAsync_DelegatesToRepository()
    {
        var list = new List<SoftwareVersion> { new SoftwareVersionBuilder().Build() };
        _repository.Setup(r => r.ListAsync()).ReturnsAsync(list);

        var result = await _sut.ListAsync();

        Assert.That(result, Is.SameAs(list));
        _repository.Verify(r => r.ListAsync(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_DelegatesToRepository()
    {
        var entity = new SoftwareVersionBuilder().WithId(3).Build();
        _repository.Setup(r => r.GetByIdAsync(3)).ReturnsAsync(entity);

        var result = await _sut.GetByIdAsync(3);

        Assert.That(result, Is.SameAs(entity));
        _repository.Verify(r => r.GetByIdAsync(3), Times.Once);
    }

    [Test]
    public async Task CreateAsync_DelegatesToRepository()
    {
        var input = new SoftwareVersionBuilder().Build();
        _repository.Setup(r => r.CreateAsync(input)).ReturnsAsync(input);

        var result = await _sut.CreateAsync(input);

        Assert.That(result, Is.SameAs(input));
        _repository.Verify(r => r.CreateAsync(input), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_DelegatesToRepository()
    {
        var input = new SoftwareVersionBuilder().WithId(2).Deprecated().Build();
        _repository.Setup(r => r.UpdateAsync(input)).ReturnsAsync(input);

        var result = await _sut.UpdateAsync(input);

        Assert.That(result, Is.SameAs(input));
        _repository.Verify(r => r.UpdateAsync(input), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_DelegatesToRepository()
    {
        _repository.Setup(r => r.DeleteAsync(9)).ReturnsAsync(-1);

        var result = await _sut.DeleteAsync(9);

        Assert.That(result, Is.EqualTo(-1));
        _repository.Verify(r => r.DeleteAsync(9), Times.Once);
    }
}
