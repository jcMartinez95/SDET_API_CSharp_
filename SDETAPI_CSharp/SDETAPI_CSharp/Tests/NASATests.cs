using JsonReader;
using RestSharp;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SDETAPI_CSharp.Tests;

//Fourth Pull Request
public class FourthPullHealthCg
{
    [Test]
    [Category("HCG")]
    public static void HealthCgTest()
    {
        string fileName = CurrentPath("Requests/HealthcareGov/Gets/hcgGetRequest.json");

        var body = JsonReaderProgram.readHCGJsonFile(fileName);

        IRestResponse response = RestCore.CreateRequestWithStatus(body.Url, body.Method, body.Status, body.StatusCode);

        JObject content = JObject.Parse(response.Content.ToString());

        //Searches for all languages = es
        var language =
            from p in content["topics"]
            where (string)p["lang"] == "es"
            select (string)p["title"];

        foreach (var item in language)
        {
            Console.WriteLine("\n" + item);
        }

        //searches and counts all categories
        var categories =
            from c in JObject.Parse(response.Content.ToString())["topics"].Children()["categories"].Values<string>()
            group c by c
            into g
            select new { Category = g.Key, Count = g.Count() };

        foreach (var c in categories)
        {
            Console.WriteLine("\nCategory: " + c.Category + " - Count: " + c.Count);
        }

    }

    [Test]
    [Category("NASA")]
    public static void NasaApiTest()
    {

        string fileName = CurrentPath("Requests/NasaOpenAPI/Gets/nasaGetRequest.json");

        var body = JsonReaderProgram.readHCGJsonFile(fileName);

        IRestResponse response = RestCore.CreateRequestWithStatus(body.Url, body.Method, body.Status, body.StatusCode);

        JObject content = JObject.Parse(response.Content.ToString());

        //Searches for all near earth objects on the date 2015-09-08 with and id GREATER THAN 50000000
        var language =
            from p in content["near_earth_objects"]["2015-09-08"]
            where Int32.Parse((string)p["id"]) >= Int32.Parse("50000000")
            //where (string)p["id"] == "es"
            select (string)p["name"];

        foreach (var item in language)
        {
            Console.WriteLine("\n" + item);
        }

        //searches and absolute magnituds LESS THAN 23
        var categories =
            from c in JObject.Parse(response.Content.ToString())["near_earth_objects"]["2015-09-07"].Children()["absolute_magnitude_h"].Values<string>()
            where float.Parse(c) <= 23
            group c by c
            into g
            select new { Absolute_magnitud = g.Key, Count = g.Count() };

        foreach (var c in categories)
        {
            Console.WriteLine("\nAbsolute magnitud: " + c.Absolute_magnitud + " - Count: " + c.Count);
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