using RestSharp;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SDETAPI_CSharp.Tests;

//Fifth Pull Request
public class NasaPullRequests
{

    [Test]
    [Category("Fourth")]
    public static void FourthPullTestNasa()
    {
        //Loading json file
        string fileName = CurrentPath("Requests/NasaOpenAPI/Gets/nasaGetRequest.json");

        //Read json file
        var body = JsonReader.readHCGJsonFile(fileName);

        //Rest Response after GET Request
        IRestResponse response = RestCore.CreateRequestWithStatus(body.Url, body.Method, body.Status, body.StatusCode);

        //Content obtained from the responde
        JObject content = JObject.Parse(response.Content.ToString());

        //Searches for all near earth objects on the date 2015-09-08 with and id GREATER THAN 50000000
        var language =
            from p in content["near_earth_objects"]["2015-09-08"]
            where Int32.Parse((string)p["id"]) >= Int32.Parse("50000000")
            //where (string)p["id"] == "es"
            select (string)p["name"];

        //Print each item found
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

        //Print each item found
        foreach (var c in categories)
        {
            Console.WriteLine("\nAbsolute magnitud: " + c.Absolute_magnitud + " - Count: " + c.Count);
        }
    }


    [Test]
    [Category("Third")]
    public static void ThirdPullTestNasa()
    {
        //Loading json file
        string fileName = CurrentPath("Requests/NasaOpenAPI/Gets/nasaGetRequest.json");

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
    public static void SecondPullTestNasa()
    {
        //Loading json file
        string fileName = CurrentPath("Requests/NasaOpenAPI/Gets/nasaGetRequest.json");

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
    public static void FirstPullTestNasa()
    {
        //Loading json file
        string fileName = CurrentPath("Requests/NasaOpenAPI/Gets/nasaGetRequest.json");

        //Read json file
        (string Method, string Url, string Status, string StatusCode) data = JsonReader.readJsonFile(fileName);

        //Rest Response after GET Request
        IRestResponse response = RestCore.CreateRequestWithHeaders(data.Url, data.Method);

        //Print Status Code & Content
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