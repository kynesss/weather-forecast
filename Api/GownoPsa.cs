using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

public static class GownoPsa
{
    [Function("GownoPsa")]
    public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger("GownoPsa");
        logger.LogInformation("C# HTTP trigger function processed a request.");

        // Retrieve the value of the "name" parameter from the query string
        var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
        string name = query.Get("name");

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        // Customize the response based on the provided name parameter
        if (string.IsNullOrEmpty(name))
        {
            response.WriteString("Welcome to Azure Functions!");
        }
        else
        {
            response.WriteString($"Hello, {name}! Welcome to Azure Functions!");
        }

        return response;
    }
}