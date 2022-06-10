
using RestSharp;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SDETAPI_CSharp.Tests;

//Fifth Pull Request
public class JsonPlaceHolderPullRequests
{

    [Test]
    [Category("Fifth")]
    public static void FifthPullTestNasa()
    {
        //Loading json file
        string fileName = CurrentPath("Requests/JsonPlaceHolder/Posts/jsonPhPostRequest.json");

        //Read json file
        var body = JsonReader.readJPHJsonFile(fileName);

        //Rest Response after POST Request
        IRestResponse response = RestCore.CreatePostRequest(body);

        //Content obtained from the responde
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