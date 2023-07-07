using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.Azure.Functions.Worker;

namespace ApiIsolated
{
    public static class HelloYou
    {
        [FunctionName("HelloYou")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req,
            ILogger log,
            ClaimsPrincipal principal)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            bool isClaimValid = true;

            if (principal == null && !principal.Identity.IsAuthenticated)
            {
                log.LogWarning("Request was not authenticated.");
                isClaimValid = false;
            }

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This SECURED HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Bonjour Hi, {name}. This SECURED HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }
    }
}