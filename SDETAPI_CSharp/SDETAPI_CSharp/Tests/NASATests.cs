using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;



namespace SDETAPI_CSharp.Tests;

//Second Pull Request 

public class Program
{
    [Test]
    public static void SecondPullHealthCg()
    {   
        string fileName = "C:/Users/carlos.martinez/source/repos/SDETAPI_CSharp/SDETAPI_CSharp/Requests/HealthcareGov/Gets/hcgGetRequest.json";
        var body = JsonReader.readHCGJsonFile(fileName);
    
        IRestResponse response = RestCore.CreateRequestWithHeaders(body.Url, body.Method);
        JObject content = JObject.Parse(response.Content.ToString());
        
        Console.WriteLine("\nStatus Code: " + response.StatusCode + "\n" +
                          "\nContent: " + content);
    }

    [Test]
    public static void SecondPullNasaOpenApi()
    {   
        string fileName = "C:/Users/carlos.martinez/source/repos/SDETAPI_CSharp/SDETAPI_CSharp/Requests/NasaOpenAPI/Gets/nasaGetRequest.json";
        var body = JsonReader.readNasaJsonFile(fileName);
    
        IRestResponse response = RestCore.CreateRequestWithHeaders(body.Url, body.Method);
        JObject content = JObject.Parse(response.Content.ToString());
        
        if (!string.Equals(response.StatusCode.ToString(), "Ok", StringComparison.OrdinalIgnoreCase))
            Console.WriteLine("\nError: "+ response.ErrorMessage);
        else {
            Console.WriteLine("\nStatus Code: " + response.StatusCode + "\n" +
                              "\nContent: " + content);
        }
        
        
    }
}
