public class Calculator1
{
    // Calculate
    public double Calculate1(double num1, double num2, char op)
    {
        switch (op)
        {
            case '+':
                return num1 + num2;
            case '-':
                return num1 - num2;
            case '*':
                return num1 * num2;
            case '/':
                if (num2 == 0)
                    throw new DivideByZeroException("Cannot divide by zero.");
                return num1 / num2;
            default:
                throw new ArgumentException("Invalid operator.");
        }
    }
}

