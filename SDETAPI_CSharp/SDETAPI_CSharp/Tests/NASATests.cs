using JsonReader;
using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace SDETAPI_CSharp.Tests;

//Fourth Pull Request
public class Program
{
    [Test]
    public static void FourthPullHealthCg()
    {
        string fileName = CurrentPath("Requests/HealthcareGov/Gets/hcgGetRequest.json");
        
        var body = JsonReaderProgram.readHCGJsonFile(fileName);
            
        IRestResponse response = RestCore.CreateRequestWithStatus(body.Url, body.Method, body.Status, body.StatusCode);
            
        var categories =
            from c in JObject.Parse(response.Content.ToString())["articles"].Children()["categories"].Values<string>()
            //where c == "employees-shop" 
            group c by c
            into g
            select new { Category = g.Key, Count = g.Count()};
            
        foreach (var c in categories)
        {
            Console.WriteLine("\nCategory: " + c.Category + " - Count: " + c.Count);
        }
            
    }
    
    public static string CurrentPath(String file)
    {   
        string path = Directory.GetCurrentDirectory();
        path = path.Replace(@"\", "/");
        int index = path.IndexOf("bin");
        if (index >= 0)
            path = path.Substring(0, index);
        string fileName = path + file;
        return fileName;
    }
}
