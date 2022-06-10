using System.Text.Json;
using Newtonsoft.Json;
using SDETAPI_CSharp.Features.HealthCareGov;
using SDETAPI_CSharp.Features.JsonPlaceHolder;
using SDETAPI_CSharp.Features.NasaOpenAPI;

namespace SDETAPI_CSharp;

public class JsonReader
{
    //Reads and loads NasaOpenAPI json file
    public static nasaFeature readNasaJsonFile(string fileName)
    {
        string jsonString;
        using (StreamReader r = new StreamReader(fileName)) 
            jsonString = r.ReadToEnd();
            nasaFeature nasafeatureData = JsonConvert.DeserializeObject<nasaFeature>(jsonString);
        var nasaBody = new nasaFeature { Url = nasafeatureData.Url,
            Method = nasafeatureData.Method,
            status = nasafeatureData.status,
            statusCode = nasafeatureData.statusCode
        };
        return nasaBody;
    }

    //Reads and loads HealthCare json file
    public static hcFeature readHCGJsonFile(string fileName)
    {
        string jsonString;
        using (StreamReader r = new StreamReader(fileName))
            jsonString = r.ReadToEnd();
        hcFeature hcfeatureData = JsonConvert.DeserializeObject<hcFeature>(jsonString);
        var hcBody = new hcFeature
        {
            Url = hcfeatureData.Url,
            Method = hcfeatureData.Method,
            Status = hcfeatureData.Status,
            StatusCode = hcfeatureData.StatusCode
        };
        return hcBody;
    }

    //Reads and loads JsonPlaceHolder json file
    public static jsonPhFeature readJPHJsonFile(string fileName)
    {
        string jsonString;
        using (StreamReader r = new StreamReader(fileName)) 
            jsonString = r.ReadToEnd();
            jsonPhFeature jsonPhfeatureData = JsonConvert.DeserializeObject<jsonPhFeature>(jsonString);
        var jsonBody = new jsonPhFeature { Url = jsonPhfeatureData.Url,
            Method = jsonPhfeatureData.Method,
            id = jsonPhfeatureData.id,
            name = jsonPhfeatureData.name,
            lastName = jsonPhfeatureData.lastName,
            featureType = jsonPhfeatureData.featureType,
            StatusCode = jsonPhfeatureData.StatusCode,
            Status = jsonPhfeatureData.Status
        };
        return jsonBody; 
    }

    //Reads and loads default json file
    public static (string, string, string, string) readJsonFile(string fileName)
    {
        string jsonString = File.ReadAllText(fileName);
        hcFeature hcFeature = System.Text.Json.JsonSerializer.Deserialize<hcFeature>(jsonString)!;

        string method = $"{hcFeature?.Method}";
        string url = $"{hcFeature?.Url}";
        string status = $"{hcFeature?.Status}";
        string statuscode = $"{hcFeature?.StatusCode}";
        //Console.WriteLine("Method: " + method);
        //Console.WriteLine("URL: " + url);
        //Console.WriteLine("Status: " + status);
        //Console.WriteLine("Status Code: " + statuscode);
        return (method, url, status, statuscode);
    }

}