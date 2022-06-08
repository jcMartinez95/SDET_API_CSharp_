
using Newtonsoft.Json;
using SDETAPI_CSharp.Features.HealthCareGov;
using SDETAPI_CSharp.Features.NasaOpenAPI;

namespace SDETAPI_CSharp;

public class JsonReader
{

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

    public static hcFeature readHCGJsonFile(string fileName)
    {
        string jsonString;
        using (StreamReader r = new StreamReader(fileName))
            jsonString = r.ReadToEnd();
        hcFeature hcfeatureData = JsonConvert.DeserializeObject<hcFeature>(jsonString);
        var hcBody = new hcFeature
        {
            Url = hcfeatureData.Url,
            Method = hcfeatureData.Method
        };
        return hcBody;
    }

}