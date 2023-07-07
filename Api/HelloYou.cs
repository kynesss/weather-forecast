using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApiIsolated;

public static class HelloYou
{
    [FunctionName("HelloYou")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "HelloYou")]
        HttpRequest req,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        string name = req.Query["name"];

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic data = JsonConvert.DeserializeObject(requestBody);

        string responseMessage = string.IsNullOrEmpty(name)
            ? "This SECURED HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            : $"Bonjour Hi, {name}. This SECURED HTTP triggered function executed successfully.";

        return new OkObjectResult(responseMessage);
    }
}