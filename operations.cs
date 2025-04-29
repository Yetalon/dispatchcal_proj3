namespace dispatchcal_proj3;

public class Operations
{
    public Dictionary<string, Func<double, double, double>> DispatchTable { get; set; }
    public Dictionary<string, int> Emdas { get; set; }
    public int ans;

    public Operations()
    {
        ans = 0;
        DispatchTable = new(){
            { "+", Add },
            { "-", Subtract },
            { "*", Multiply },
            { "/", Divide },
            { "%", Mod },
            { "^", Exponentiate }
        };
        Emdas = new(){
            { "+", 1},
            { "-", 1},
            { "*", 2},
            { "/", 2},
            { "%", 2},
            { "^", 3},
        };
    }

    private double Add(double a, double b) => a + b;
    private double Subtract(double a, double b) => a - b;
    private double Multiply(double a, double b) => a * b;
    private double Mod(double a, double b) => a % b;
    private double Exponentiate(double a, double b) => Math.Pow(a, b);
    private double Divide(double a, double b) => b == 0 ? throw new DivideByZeroException() : a / b;
}
