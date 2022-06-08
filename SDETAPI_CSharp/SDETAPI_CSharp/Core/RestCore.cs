
using RestSharp;
using NUnit.Framework;
using SDETAPI_CSharp.Features.JsonPlaceHolder;

namespace SDETAPI_CSharp;


public class RestCore
{
    private static RestRequest? restRequest;

    
    public static IRestResponse CreatePostRequest(jsonPhFeature body)
    {
        switch (body.Method.ToUpper())
        {
            case "GET":
                restRequest = new RestRequest(body.Url, Method.GET);
                break;

            case "POST":
                restRequest = new RestRequest(body.Url, Method.POST);
                break;

            case "PUT":
                restRequest = new RestRequest(body.Url, Method.PUT);
                break;

            case "DELETE":
                restRequest = new RestRequest(body.Url, Method.DELETE);
                break;

            default:
                throw new NotImplementedException($"Rest Method not valid. Must specify correctly. Current value: [{body.Method}]" +
                                                  $"Current valid types: Get and Post");
        }
        restRequest.RequestFormat = DataFormat.Json;
        IRestResponse response = AddPostRequestBody(restRequest, body);
        return response;
    }

    public static IRestResponse AddPostRequestBody(RestRequest restRequest, jsonPhFeature body)
    {
        
        RestClient restClient = new RestClient();
        restRequest.AddParameter("application/json; charset=utf-8", restRequest.AddJsonBody(body));
        IRestResponse serviceResponse = restClient.Execute(restRequest);

        if (!string.Equals(serviceResponse.StatusCode.ToString(), body.Status, StringComparison.OrdinalIgnoreCase))
            Console.WriteLine("\nError: "+ serviceResponse.ErrorMessage);
        
        Assert.AreEqual(serviceResponse.StatusCode.ToString(), body.Status);
        Assert.That((int)serviceResponse.StatusCode, Is.EqualTo(body.StatusCode));
        
        Console.Write("\nStatus Code: " + serviceResponse.StatusCode);
        Console.Write("\nStatus Code Num: " + (int)serviceResponse.StatusCode);
        
        return serviceResponse;
        
        
    }

}