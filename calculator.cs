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
            double secondNum = variables.ContainsKey(tokens.Current.Trim()) ?
                variables[tokens.Current.Trim()] : ParseOrThrow(tokens.Current);
            ops.DispatchTable.TryGetValue(op, out var operation);
            lastVal = operation(lastVal, secondNum);
        }
        return lastVal;
    }

    public double Organizer(List<string> tokenList)
    {
        double value = 0;
        while (tokenList.Count != 1)
        {
            (int index, int opValue) highestOp = (-1, 0);

            for (int i = 0; i < tokenList.Count; i++)
            {
                if (ops.Emdas.TryGetValue(tokenList[i], out int emdasValue)
                        && emdasValue > highestOp.opValue)
                {
                    highestOp = (i, emdasValue);
                }
            }
            if (highestOp.index < 0) throw new Exception("No operator found in calculation");

            List<string> evalStrings = new() {
                tokenList[highestOp.index - 1],
                tokenList[highestOp.index],
                tokenList[highestOp.index + 1]
            };
            value = Evaluator(evalStrings);
            tokenList[highestOp.index - 1] = value.ToString();
            tokenList.RemoveRange(highestOp.index, 2);
        }
        Console.WriteLine($"Answer is {value}");
        variables["ans"] = value;
        return value;
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
                Console.WriteLine($"{tokensList[0]} is {variables[tokensList[0]]}");
            }
            return;
        }
        if (tokensList.Count < 3) throw new Exception("Invalid input");
        string varName = tokensList[0];
        if (tokensList[1] == "=")
        {
            tokensList.RemoveRange(0, 2);
            if (tokensList.Count == 1)
            {
                double singleValue = Evaluator(tokensList);
                variables[varName] = singleValue;
                fmanager.WriteVariables(variables);
                Console.WriteLine($"{varName} is {singleValue}");
                return;
            }
            double value = Organizer(tokensList);
            variables[varName] = value;
            fmanager.WriteVariables(variables);
            return;
        }
        Organizer(tokensList);
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
