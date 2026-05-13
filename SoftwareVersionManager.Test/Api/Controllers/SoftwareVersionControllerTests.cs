using Microsoft.AspNetCore.Mvc;
using Moq;
using SoftwareVersionManager.Controllers;
using SoftwareVersionManager.Domain.Interfaces;
using SoftwareVersionManager.Entities.Models;
using SoftwareVersionManager.Test.Common.Builders;

namespace SoftwareVersionManager.Test.Api.Controllers;

[TestFixture]
public sealed class SoftwareVersionControllerTests
{
    private Mock<IService<SoftwareVersion>> _service = null!;
    private SoftwareVersionController _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _service = new Mock<IService<SoftwareVersion>>(MockBehavior.Strict);
        _sut = new SoftwareVersionController(_service.Object);
    }

    [Test]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        var entity = new SoftwareVersionBuilder().WithId(5).Build();
        _service.Setup(s => s.GetByIdAsync(5)).ReturnsAsync(entity);

        var result = await _sut.Get(5);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task Post_ReturnsOk_WhenCreateSucceeds()
    {
        var entity = new SoftwareVersionBuilder().Build();
        _service.Setup(s => s.CreateAsync(entity)).ReturnsAsync(entity);

        var result = await _sut.Post(entity);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task Put_ReturnsOk_WhenUpdateSucceeds()
    {
        var entity = new SoftwareVersionBuilder().WithId(1).Build();
        _service.Setup(s => s.UpdateAsync(entity)).ReturnsAsync(entity);

        var result = await _sut.Put(entity);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task Put_ReturnsBadRequest_WhenServiceThrows()
    {
        var entity = new SoftwareVersionBuilder().Build();
        _service.Setup(s => s.UpdateAsync(entity)).ThrowsAsync(new InvalidOperationException("x"));

        var result = await _sut.Put(entity);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        Assert.That(((BadRequestObjectResult)result).Value, Is.EqualTo("x"));
    }

    [Test]
    public async Task Delete_ReturnsNotFound_WhenNoRowsAffected()
    {
        _service.Setup(s => s.DeleteAsync(2)).ReturnsAsync(0);

        var result = await _sut.Delete(2);

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Get_ReturnsOkWithList()
    {
        var data = new List<SoftwareVersion> { new SoftwareVersionBuilder().Build() };
        _service.Setup(s => s.ListAsync()).ReturnsAsync(data);

        var result = await _sut.Get();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task GetById_ReturnsNotFound_WhenMissing()
    {
        _service.Setup(s => s.GetByIdAsync(1)).Returns(Task.FromResult<SoftwareVersion>(null!));

        var result = await _sut.Get(1);

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Post_ReturnsBadRequest_OnFailure()
    {
        var entity = new SoftwareVersionBuilder().Build();
        _service.Setup(s => s.CreateAsync(entity)).ThrowsAsync(new Exception("db"));

        var result = await _sut.Post(entity);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Delete_ReturnsOk_WhenRowsAffected()
    {
        _service.Setup(s => s.DeleteAsync(10)).ReturnsAsync(2);

        var result = await _sut.Delete(10);

        Assert.That(result, Is.InstanceOf<OkResult>());
    }
}
