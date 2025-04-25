namespace dispatchcal_proj3;

class Program
{
    static void Main(string[] args)
    {
        Menu dispMenu = new();
        Console.WriteLine("Please enter a calculation or 'menu' for help this calculator does not support parentheses");
        while (!dispMenu.exit)
        {
            dispMenu.DisplayMenu();
        }
    }
}
