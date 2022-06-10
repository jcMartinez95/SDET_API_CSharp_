using RestSharp;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SDETAPI_CSharp.Tests;

//Fifth Pull Request
public class HcgPullRequests
{
    [Test]
    [Category("Fourth")]
    public static void FourthPullTestHCG()
    {
        //Loading json file
        string fileName = CurrentPath("Requests/HealthcareGov/Gets/hcgGetRequest.json");

        //Read json file
        var body = JsonReader.readHCGJsonFile(fileName);

        //Rest Response after GET Request 
        IRestResponse response = RestCore.CreateRequestWithStatus(body.Url, body.Method, body.Status, body.StatusCode);

        //Content obtained from the responde
        JObject content = JObject.Parse(response.Content.ToString());

        //Searches for all languages = es
        var language =
            from p in content["topics"]
            where (string)p["lang"] == "es"
            select (string)p["title"];

        //Print each item found
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

        //Print each item found
        foreach (var c in categories)
        {
            Console.WriteLine("\nCategory: " + c.Category + " - Count: " + c.Count);
        }

    }

    [Test]
    [Category("Third")]
    public static void ThirdPullTestHCG()
    {
        //Loading json file
        string fileName = CurrentPath("Requests/HealthcareGov/Gets/hcgGetRequest.json");

        //Read json file
        var body = JsonReader.readNasaJsonFile(fileName);

        //Rest Response after GET Request
        IRestResponse response = RestCore.CreateRequestWithHeaders(body.Url, body.Method);

        //Content obtained from the responde
        JObject content = JObject.Parse(response.Content.ToString());

        //Assert the response using Assert.True(); Assert.Equal(); Successfull responses should have code 200
        Assert.AreEqual(response.StatusCode.ToString(), (body.status.ToUpper()));
        Assert.That((int)response.StatusCode, Is.EqualTo(body.statusCode));

        //Print status code
        Console.Write("\nStatus Code: " + response.StatusCode);
        Console.Write("\nStatus Code Num: " + (int)response.StatusCode + "\n");

        //Console.WriteLine("\n\nContent: " + content); //Optional

    }

    [Test]
    [Category("Second")]
    public static void SecondPullTestHCG()
    {
        //Loading json file
        string fileName = CurrentPath("Requests/HealthcareGov/Gets/hcgGetRequest.json");

        //Read json file
        var body = JsonReader.readHCGJsonFile(fileName);

        //Rest Response after GET Request
        IRestResponse response = RestCore.CreateRequestWithHeaders(body.Url, body.Method);

        //Parse content obtained from the responde
        JObject content = JObject.Parse(response.Content.ToString());

        //Print Status Code & Content
        Console.WriteLine("\nContent: " + content);
    }

    [Test]
    [Category("First")]
    public static void FirstPullTestHCG()
    {
        //Loading json file
        string fileName = CurrentPath("Requests/HealthcareGov/Gets/hcgGetRequest.json");

        //Read json file
        (string Method, string Url, string Status, string StatusCode) data = JsonReader.readJsonFile(fileName);

        //Rest Response after GET Request
        IRestResponse response = RestCore.CreateRequestWithHeaders(data.Url, data.Method);

        //Print Content
        Console.WriteLine("\nContent: " + response.Content);
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