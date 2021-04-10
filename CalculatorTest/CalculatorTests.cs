using System;
using Xunit;

namespace Calculator.Test
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_Signature()
        {
            var calculator = new Calculator();
            string numbers = string.Empty;
            int result = calculator.Add(numbers);
        }

        [Fact]
        public void Add_Empty_Parameters()
        {
            var input = string.Empty;
            var calculator = new Calculator();
            int result = calculator.Add(input);
            Assert.Equal(0, result);
        }
        [Theory]
        [InlineData("1", 1)]
        [InlineData("5", 5)]
        [InlineData("55", 55)]
        public void Add_Single_Parameters(string input, int expectedResult)
        {            
            var calculator = new Calculator();
            int result = calculator.Add(input);
            Assert.Equal(expectedResult, result);
        }
        [Theory]
        [InlineData("1,2", 3)]
        [InlineData("5,7", 12)]
        [InlineData("55,1", 56)]
        public void Add_Two_Parameters(string input, int expectedResult)
        {
            var calculator = new Calculator();
            int result = calculator.Add(input);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1,2,3", 6)]
        [InlineData("5,7,1,1", 14)]
        [InlineData("55,1,1,1,1", 59)]
        public void Add_Many_Parameters(string input, int expectedResult)
        {
            var calculator = new Calculator();
            int result = calculator.Add(input);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("1\n2,3", 6)]
        [InlineData("5,7,1\n1", 14)]
        [InlineData("55,1\n1,1,1", 59)]
        public void Add_New_Line_Delimiter(string input, int expectedResult)
        {
            var calculator = new Calculator();
            int result = calculator.Add(input);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("//;\n1;9", 10)]
        [InlineData("//*\n5*5*1*1", 12)]
        public void Add_Changed_Delimiter(string input, int expectedResult)
        {
            var calculator = new Calculator();
            int result = calculator.Add(input);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("-50", "-50")]
        [InlineData("20,-1", "-1")]
        [InlineData("20,-1,-5", "-1,-5")]
        public void Add_Throws_Negative_Numbers(string input, string negativeNumbers)
        {
            var calculator = new Calculator();
            var exception = Assert.Throws<ArgumentException>(() => calculator.Add(input));
            var expectedExceptionMessage = $"negatives not allowed {negativeNumbers}";
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
        [Theory]
        [InlineData("0,1001", 0)]
        [InlineData("1,1001", 1)]
        [InlineData("1\n1001", 1)]
        [InlineData("//;\n1;1001", 1)]
        [InlineData("//*\n1*1001", 1)]
        [InlineData("//,\n1,2,1001", 3)]
        public void Returns_CorrectSum_When_Ignoring_Numbers_Greater_Than_1000(string input, int expectedResult)
        {
            var calculator = new Calculator();
            int result = calculator.Add(input);
            Assert.Equal(expectedResult, result);
        }
        [Theory]
        [InlineData("//[££]\n1££1", 2)]
        [InlineData("//[$$]\n1$$1$$1", 3)]
        [InlineData("//[££]\n1££1,1££1", 4)]
        [InlineData("//[$$$]\n1$$$1,1\n1$$$1", 5)]
        public void Returns_CorrectSum_With_Custom_Delimiters_Of_Any_Length(string numbers, int expectedResult)
        {
            var calculator = new Calculator();
            int result = calculator.Add(numbers);
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData("//[$$][££]\n1££1", 2)]
        [InlineData("//[$$][££]\n1$$1££1", 3)]
        [InlineData("//[$$][££]\n1$$1,1££1", 4)]
        [InlineData("//[$$$][£££]\n1$$$1,1\n1£££1", 5)]

        public void Returns_CorrectSum_With_Custom_Delimiters_Of_Any_Length_And_Quantity(string numbers, int expectedResult)
        {
            var calculator = new Calculator();
            int result = calculator.Add(numbers);
            Assert.Equal(expectedResult, result);
        }
    }
}
