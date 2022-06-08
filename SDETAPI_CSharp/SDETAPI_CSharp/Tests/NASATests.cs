
using RestSharp;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace SDETAPI_CSharp.Tests;

//Fifth Pull Request
public class Program
{
    [Test]
    public static void FifthPullJph()
    {
        string fileName = CurrentPath("Requests/JsonPlaceHolder/Posts/jsonPhPostRequest.json");
        var body = JsonReader.readJPHJsonFile(fileName);
        
        IRestResponse response = RestCore.CreatePostRequest(body);
        JObject content = JObject.Parse(response.Content.ToString());
        
        Console.WriteLine("\n\nContent: " + content);
        
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

