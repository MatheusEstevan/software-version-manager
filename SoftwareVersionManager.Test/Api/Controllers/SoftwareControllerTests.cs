using Microsoft.AspNetCore.Mvc;
using Moq;
using SoftwareVersionManager.Controllers;
using SoftwareVersionManager.Domain.Interfaces;
using SoftwareVersionManager.Entities.Models;
using SoftwareVersionManager.Test.Common.Builders;

namespace SoftwareVersionManager.Test.Api.Controllers;

[TestFixture]
public sealed class SoftwareControllerTests
{
    private Mock<IService<Software>> _service = null!;
    private SoftwareController _sut = null!;

    [SetUp]
    public void SetUp()
    {
        _service = new Mock<IService<Software>>(MockBehavior.Strict);
        _sut = new SoftwareController(_service.Object);
    }

    [Test]
    public async Task Get_ReturnsOkWithList()
    {
        var data = new List<Software> { new SoftwareBuilder().Build() };
        _service.Setup(s => s.ListAsync()).ReturnsAsync(data);

        var result = await _sut.Get();

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var ok = (OkObjectResult)result;
        Assert.That(ok.Value, Is.SameAs(data));
    }

    [Test]
    public async Task GetById_ReturnsOk_WhenFound()
    {
        var entity = new SoftwareBuilder().WithId(1).Build();
        _service.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(entity);

        var result = await _sut.Get(1);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task GetById_ReturnsNotFound_WhenMissing()
    {
        _service.Setup(s => s.GetByIdAsync(99)).Returns(Task.FromResult<Software>(null!));

        var result = await _sut.Get(99);

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Post_ReturnsOk_WhenCreateSucceeds()
    {
        var entity = new SoftwareBuilder().Build();
        _service.Setup(s => s.CreateAsync(entity)).ReturnsAsync(entity);

        var result = await _sut.Post(entity);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task Post_ReturnsBadRequest_WhenServiceThrows()
    {
        var entity = new SoftwareBuilder().Build();
        _service.Setup(s => s.CreateAsync(entity)).ThrowsAsync(new InvalidOperationException("fail"));

        var result = await _sut.Post(entity);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        var bad = (BadRequestObjectResult)result;
        Assert.That(bad.Value, Is.EqualTo("fail"));
    }

    [Test]
    public async Task Put_ReturnsOk_WhenUpdateSucceeds()
    {
        var entity = new SoftwareBuilder().WithId(2).Build();
        _service.Setup(s => s.UpdateAsync(entity)).ReturnsAsync(entity);

        var result = await _sut.Put(entity);

        Assert.That(result, Is.InstanceOf<OkObjectResult>());
    }

    [Test]
    public async Task Put_ReturnsBadRequest_WhenServiceThrows()
    {
        var entity = new SoftwareBuilder().Build();
        _service.Setup(s => s.UpdateAsync(entity)).ThrowsAsync(new Exception("err"));

        var result = await _sut.Put(entity);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task Delete_ReturnsOk_WhenDeleted()
    {
        _service.Setup(s => s.DeleteAsync(3)).ReturnsAsync(1);

        var result = await _sut.Delete(3);

        Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public async Task Delete_ReturnsNotFound_WhenNothingDeleted()
    {
        _service.Setup(s => s.DeleteAsync(3)).ReturnsAsync(0);

        var result = await _sut.Delete(3);

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Delete_ReturnsNotFound_WhenRepositoryReturnsMinusOne()
    {
        _service.Setup(s => s.DeleteAsync(3)).ReturnsAsync(-1);

        var result = await _sut.Delete(3);

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }
}
