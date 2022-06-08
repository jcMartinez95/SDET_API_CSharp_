using RestSharp;

namespace SDETAPI_CSharp;


public class RestCore
{
    private static RestRequest? restRequest;

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
        IRestResponse response = AddRequestBody(restRequest);
        return response;
    }

    public static IRestResponse AddRequestBody(RestRequest restRequest)
    {
        RestClient restClient = new RestClient();
        restRequest.AddParameter("application/json; charset=utf-8", ParameterType.RequestBody);
        IRestResponse serviceReponse = restClient.Execute(restRequest);

        return serviceReponse;
        
        
    }
    
    
}