namespace dispatchcal_proj3;

public class Calculator
{
    public Dictionary<string, Func<double, double, double>> DispatchTable { get; set; }

    public Calculator()
    {
        DispatchTable = new(){
            { "add", Add },
            { "subtract", Subtract }
        };
    }
    private double Add(double a, double b) => a + b;

    private double Subtract(double a, double b) => a - b;

    private double Multiply(double a, double b) => a * b;

    private double Divide(double a, double b)
    {
        if (b == 0) throw new DivideByZeroException();
        return a / b;
    }

    private double Mod(double a, double b) => a % b;

    private double Exponentiate(double a, double b) => Math.Pow(a, b);
}

