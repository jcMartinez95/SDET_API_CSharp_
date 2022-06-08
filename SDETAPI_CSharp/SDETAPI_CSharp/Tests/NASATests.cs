using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;

namespace SDETAPI_CSharp.Tests;

//Third Pull Request 
public class Program
{

    [Test]
    public static void ThirdPullNasaOpenApi()
    {   
        string fileName = "C:/Users/carlos.martinez/source/repos/SDET_API_CSharp/SDETAPI_CSharp/SDETAPI_CSharp/Requests/NasaOpenAPI/Gets/nasaGetRequest.json";
        var body = JsonReader.readNasaJsonFile(fileName);
    
        IRestResponse response = RestCore.CreateRequestWithStatus(body.Url, body.Method, body.status, body.statusCode);
        JObject content = JObject.Parse(response.Content.ToString());
        
        Console.WriteLine("\n\nContent: " + content);
        
    }
}

