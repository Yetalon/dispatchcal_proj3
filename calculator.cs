namespace dispatchcal_proj3;

public class Calculator
{
    private Operations ops { get; set; }
    private Dictionary<string, double> variables { get; set; }
    public double ans { get; set; }
    private FileManager fmanager { get; set; }
    public Calculator()
    {
        fmanager = new();
        ops = new();
        variables = fmanager.ReadVariables();
    }
    public double Evaluator(List<string> tokensList)
    {
        var tokens = tokensList.GetEnumerator();
        tokens.MoveNext();
        double lastVal = variables.ContainsKey(tokens.Current) ?
            variables[tokens.Current] : ParseOrThrow(tokens.Current);
        while (tokens.MoveNext())
        {
            string op = tokens.Current;
            tokens.MoveNext();
            double secondNum = variables.ContainsKey(tokens.Current) ?
                variables[tokens.Current] : ParseOrThrow(tokens.Current);
            ops.DispatchTable.TryGetValue(op, out var operation);
            lastVal = operation(lastVal, secondNum);
        }
        variables["ans"] = lastVal;
        Console.WriteLine($"Answer is {lastVal}");
        return lastVal;
    }

    public double ParseOrThrow(string token)
    {
        if (double.TryParse(token, out double result)) return result;
        throw new Exception("Unrecognized variable");
    }

    public void Execute(string input)
    {
        List<string> tokensList = Parser(input);
        if (tokensList.Count == 1)
        {
            if (variables.ContainsKey(tokensList[0]))
            {
                Console.WriteLine($"a is {variables[tokensList[0]]}");
            }
            return;
        }
        if (tokensList.Count < 3) throw new Exception("Invalid input");
        string varName = tokensList[0];
        if (tokensList[1] == "=")
        {
            tokensList.RemoveAt(0);
            tokensList.RemoveAt(0);
            double value = Evaluator(tokensList);
            variables[varName] = value;
            fmanager.WriteVariables(variables);
            return;
        }
        Evaluator(tokensList);
    }

    public List<string> Parser(string input)
    {
        List<string> parsedInput = new();
        string number = "";
        bool lastWasOp = true;
        foreach (char c in input)
        {
            if (!"+-*/%^=".Contains(c))
            {
                number += c;
                lastWasOp = false;
            }
            else
            {
                if (c == '-' && lastWasOp)
                {
                    number += c;
                }
                else
                {
                    parsedInput.Add(number.Trim());
                    number = "";
                    parsedInput.Add(c.ToString());
                    lastWasOp = true;
                }
            }
        }
        if (number != "")
        {
            parsedInput.Add(number);
        }
        return (parsedInput);
    }
    public void ClearCalc()
    {
        variables["ans"] = 0;
    }
}
