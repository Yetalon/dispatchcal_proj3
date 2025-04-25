namespace dispatchcal_proj3;
using System.Text.Json;

public class FileManager
{
    public void WriteVariables(Dictionary<string, double> dict)
    {
        string data = JsonSerializer.Serialize(dict);
        File.WriteAllText("output.json", data);
    }

    public Dictionary<string, double> ReadVariables()
    {
        try
        {
            string json = File.ReadAllText("output.json");
            return JsonSerializer.Deserialize<Dictionary<string, double>>(json);
        }
        catch
        {
            return new Dictionary<string, double>();
        }
    }
}
