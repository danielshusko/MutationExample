using Moq;

namespace MutationExample.Tests;

public class ServiceTests
{
    private readonly Mock<IDependentService> _mockDependentService;
    private readonly Service _service;

    public ServiceTests()
    {
        _mockDependentService = new Mock<IDependentService>();
        _mockDependentService
            .Setup(x => x.Validate(It.IsAny<string>()))
            .Returns(true);

        _service = new Service(_mockDependentService.Object);
    }

    [Fact]
    public void DoSomethingTest()
    {
        var result = _service.DoSomething(Guid.NewGuid());

        Assert.True(result);
        _mockDependentService.Verify(x=>x.Validate(It.IsAny<string>()), Times.Once);
    }

    //[Theory]
    //[InlineData("0f080606-f1bc-43ac-90ee-d1860c826b66", "even")]
    //[InlineData("24c0a7c2-2864-4179-85f6-df386c7c9e12", "odd")]
    //public void DoSomethingTestFull_Full(string id, string expectedValue)
    //{
    //    var idGuid = Guid.Parse(id);
    //    var result = _service.DoSomething(idGuid);

    //    Assert.True(result);
    //    _mockDependentService.Verify(x => x.Validate(expectedValue), Times.Once);
    //}
}