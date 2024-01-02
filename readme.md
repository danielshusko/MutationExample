# Mutation Test Example

This project is an example of how Mutation test work using [Stryker.Net](https://stryker-mutator.io/docs/stryker-net/introduction/).

Mutation testing is a way to ensure the quality of unit tests. It does this modifying source code (mutating) and running tests. Quality unit test should detect the modifications and fail.

This repository contains two simple examples which show low quality unit tests that provide 100% code coverage.

## Calculator

In this example, there is a simple method Add, which adds two numbers and returns the result.

```
public int Add(int x, int y)
{
    return x + y;
}
```

This method has a single unit test:

```
[Theory]
[InlineData(100,0,100)]
public void Add(int x, int y, int expectedResult)
{
    var calculator = new Calculator();
    var result = calculator.Add(x, y);
    Assert.Equal(expectedResult,result);
}
```

This unit test will run and provide 100% coverage on this method. However, the test parameters are fragile. If the method was accidently changed to a subtraction, the test would still pass.

## Service

In this example, a method performs some calculation and the passes that result to another service method.

```
public bool DoSomething(Guid id)
{
    var guidHash = id.GetHashCode();

    var value = guidHash%2 == 0 ? "even" : "odd";

    return service.Validate(value);
}
```

The unit test mocks the service using string.Any() and just verifies the other service is called. It completely ignores the internal logic of the method. It provides 100% coverage, but it does not fully test the logic in the method.

```
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
```

---
