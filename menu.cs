namespace dispatchcal_proj3;

public class Menu
{
    Calculator calc { get; set; }
    Dictionary<string, Action> menu { get; set; }
    public bool exit { get; set; }

    public Menu()
    {
        calc = new();
        menu = new(){
            {"menu", DisplayCommands},
            {"exit", ExitProgram},
            {"clear", calc.ClearCalc},
            {"cleardisplay", ClearDisplay}
        };
        exit = false;
    }

    public void DisplayMenu()
    {
        string? userinput = Console.ReadLine();
        ReadInput(userinput);
    }

    public void DisplayCommands()
    {
        Console.WriteLine(@"
        Welcome to the Basic Calculator!

        This calculator supports the following operations:
        - Add, Subtract, Multiply, Divide, Modulus, and Exponents.

        Features:
        - To use your previous answer, type 'ans' followed by the operation you'd like to perform.
        - To assign a result to a variable, use the format: 'a = 2 + 3'.
        - All variables are saved to a file and automatically loaded when you restart the calculator.
        - Type 'clear' to reset the previous answer to 0.
        - Type 'cleardisplay' to clear the display screen.
        - Type 'exit' to close the program.

        Enjoy calculating!
        ");
    }

    public void ClearDisplay()
    {
        Console.Clear();
    }

    public void ExitProgram()
    {
        Console.WriteLine("Exiting program ....");
        exit = true;
    }

    public void ReadInput(string? input)
    {
        if (menu.TryGetValue(input.Trim().ToLower(), out var action))
        {
            try
            {
                action();
            }
            catch
            {
                Console.WriteLine("Invalid command");
            }
            return;
        }
        try
        {
            calc.Execute(input);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}
