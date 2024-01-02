namespace MutationExample.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(100,0,100)]
        //[InlineData(100, 5, 105)]
        public void Add(int x, int y, int expectedResult)
        {
            var calculator = new Calculator();
            var result = calculator.Add(x, y);
            Assert.Equal(expectedResult,result);
        }
    }
}