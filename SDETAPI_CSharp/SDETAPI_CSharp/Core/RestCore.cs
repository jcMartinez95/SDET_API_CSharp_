using RestSharp;
using NUnit.Framework;

namespace SDETAPI_CSharp;


public class RestCore
{
    private static RestRequest? restRequest;

   public static IRestResponse CreateRequestWithStatus(string Url, string methodType, string status, int statusCode)
    {
        switch (methodType.ToUpper())
        {
            case "GET":
                restRequest = new RestRequest(Url, Method.GET);
                break;

            case "POST":
                restRequest = new RestRequest(Url, Method.POST);
                break;

            case "PUT":
                restRequest = new RestRequest(Url, Method.PUT);
                break;

            case "DELETE":
                restRequest = new RestRequest(Url, Method.DELETE);
                break;

            default:
                throw new NotImplementedException($"Rest Method not valid. Must specifiy correctly. Current value: [{methodType}]" +
                                                  $"Current valid types: Get and Post");
        }
        restRequest.RequestFormat = DataFormat.Json;
        IRestResponse response = AddGETRequestBodyWithStatus(restRequest, status, statusCode);
        return response;
    }
    
    public static IRestResponse AddGETRequestBodyWithStatus(RestRequest restRequest, string status, int statusCode)
    {
        RestClient restClient = new RestClient();
        restRequest.AddParameter("application/json; charset=utf-8", ParameterType.RequestBody);
        IRestResponse serviceResponse = restClient.Execute(restRequest);
        
        if (!string.Equals(serviceResponse.StatusCode.ToString(), status.ToUpper(), StringComparison.OrdinalIgnoreCase))
            Console.WriteLine("\nError: "+ serviceResponse.ErrorMessage);
        
        Assert.AreEqual(serviceResponse.StatusCode.ToString(), (status.ToUpper()));
        Assert.That((int)serviceResponse.StatusCode, Is.EqualTo(statusCode));
        
        Console.Write("\nStatus Code: " + serviceResponse.StatusCode);
        Console.Write("\nStatus Code Num: "+ (int)serviceResponse.StatusCode + "\n");
        
        return serviceResponse;
        
        
    }
    
    
}