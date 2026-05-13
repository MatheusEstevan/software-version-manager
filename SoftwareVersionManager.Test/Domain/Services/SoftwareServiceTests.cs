using Moq;
using SoftwareVersionManager.Domain.Interfaces;
using SoftwareVersionManager.Domain.Services;
using SoftwareVersionManager.Entities.Models;
using SoftwareVersionManager.Test.Common.Builders;

namespace SoftwareVersionManager.Test.Domain.Services;

[TestFixture]
public sealed class SoftwareServiceTests
{
    private Mock<IRepository<Software>> _repository = null!;
    private SoftwareService _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _repository = new Mock<IRepository<Software>>(MockBehavior.Strict);
        _sut = new SoftwareService(_repository.Object);
    }

    [Test]
    public async Task ListAsync_DelegatesToRepository()
    {
        var list = new List<Software> { new SoftwareBuilder().Build() };
        _repository.Setup(r => r.ListAsync()).ReturnsAsync(list);

        var result = await _sut.ListAsync();

        Assert.That(result, Is.SameAs(list));
        _repository.Verify(r => r.ListAsync(), Times.Once);
    }

    [Test]
    public async Task GetByIdAsync_DelegatesToRepository()
    {
        var entity = new SoftwareBuilder().WithId(5).Build();
        _repository.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(entity);

        var result = await _sut.GetByIdAsync(5);

        Assert.That(result, Is.SameAs(entity));
        _repository.Verify(r => r.GetByIdAsync(5), Times.Once);
    }

    [Test]
    public async Task CreateAsync_DelegatesToRepository()
    {
        var input = new SoftwareBuilder().Build();
        _repository.Setup(r => r.CreateAsync(input)).ReturnsAsync(input);

        var result = await _sut.CreateAsync(input);

        Assert.That(result, Is.SameAs(input));
        _repository.Verify(r => r.CreateAsync(input), Times.Once);
    }

    [Test]
    public async Task UpdateAsync_DelegatesToRepository()
    {
        var input = new SoftwareBuilder().WithId(1).Build();
        _repository.Setup(r => r.UpdateAsync(input)).ReturnsAsync(input);

        var result = await _sut.UpdateAsync(input);

        Assert.That(result, Is.SameAs(input));
        _repository.Verify(r => r.UpdateAsync(input), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_DelegatesToRepository()
    {
        _repository.Setup(r => r.DeleteAsync(7)).ReturnsAsync(1);

        var result = await _sut.DeleteAsync(7);

        Assert.That(result, Is.EqualTo(1));
        _repository.Verify(r => r.DeleteAsync(7), Times.Once);
    }
}
