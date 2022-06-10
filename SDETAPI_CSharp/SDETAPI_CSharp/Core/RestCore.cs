
using RestSharp;
using NUnit.Framework;
using SDETAPI_CSharp.Features.JsonPlaceHolder;

namespace SDETAPI_CSharp;


public class RestCore
{
    private static RestRequest? restRequest;

    //POST Request and adds the body along with assert response
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

    //GET Request with ASSERT response captured inside this method and not on main test class
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

    //GET Request with response
    public static IRestResponse CreateRequestWithHeaders(string Url, string methodType)
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
        IRestResponse response = AddRequestBody(restRequest, methodType);
        return response;
    }

    //USED FOR: CreateRequestWithHeaders
    public static IRestResponse AddRequestBody(RestRequest restRequest, string methodType)
    {
        RestClient restClient = new RestClient();
        restRequest.AddParameter("application/json; charset=utf-8", ParameterType.RequestBody);
        IRestResponse serviceResponse = restClient.Execute(restRequest);

        return serviceResponse;


    }

    //USED FOR: CreateRequestWithStatus
    public static IRestResponse AddGETRequestBodyWithStatus(RestRequest restRequest, string status, int statusCode)
    {
        RestClient restClient = new RestClient();
        restRequest.AddParameter("application/json; charset=utf-8", ParameterType.RequestBody);
        IRestResponse serviceResponse = restClient.Execute(restRequest);

        if (!string.Equals(serviceResponse.StatusCode.ToString(), status.ToUpper(), StringComparison.OrdinalIgnoreCase))
            Console.WriteLine("\nError: " + serviceResponse.ErrorMessage);

        Assert.AreEqual(serviceResponse.StatusCode.ToString(), (status.ToUpper()));
        Assert.That((int)serviceResponse.StatusCode, Is.EqualTo(statusCode));

        Console.Write("\nStatus Code: " + serviceResponse.StatusCode);
        Console.Write("\nStatus Code Num: " + (int)serviceResponse.StatusCode + "\n");

        return serviceResponse;


    }

    //USED FOR: CreatePostRequest
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