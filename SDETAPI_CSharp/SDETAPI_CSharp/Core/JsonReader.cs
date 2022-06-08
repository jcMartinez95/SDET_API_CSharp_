using System.Text.Json;
using SDETAPI_CSharp.Features.HealthCareGov;

namespace SDETAPI_CSharp;

public class JsonReader
{

    public static (string, string) readJsonFile(string fileName)
    {
        string jsonString = File.ReadAllText(fileName);
        hcFeature hcFeature = JsonSerializer.Deserialize<hcFeature>(jsonString)!;
        
        string method = $"{hcFeature?.Method}";
        string url = $"{hcFeature?.Url}";
        Console.WriteLine("Method: " + method);
        Console.WriteLine("URL: " + url);
        return (method, url);
    }
}