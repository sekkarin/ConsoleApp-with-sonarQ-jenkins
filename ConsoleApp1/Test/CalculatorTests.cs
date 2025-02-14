using System;
using Xunit;

public class CalculatorTests
{
    private readonly Calculator _calculator;

    public CalculatorTests()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void Addition_ShouldReturn_CorrectSum()
    {
        double result = _calculator.Calculate(5, 3, '+');
        Assert.Equal(8, result);
    }

    [Fact]
    public void Subtraction_ShouldReturn_CorrectDifference()
    {
        double result = _calculator.Calculate(10, 4, '-');
        Assert.Equal(6, result);
    }

    [Fact]
    public void Multiplication_ShouldReturn_CorrectProduct()
    {
        double result = _calculator.Calculate(6, 7, '*');
        Assert.Equal(42, result);
    }

    [Fact]
    public void Division_ShouldReturn_CorrectQuotient()
    {
        double result = _calculator.Calculate(20, 5, '/');
        Assert.Equal(4, result);
    }

    [Fact]
    public void Division_ByZero_ShouldThrow_Exception()
    {
        Assert.Throws<DivideByZeroException>(() => _calculator.Calculate(10, 0, '/'));
    }

    [Fact]
    public void InvalidOperator_ShouldThrow_Exception()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Calculate(10, 5, '?'));
    }
}
