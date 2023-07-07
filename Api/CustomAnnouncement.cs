using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ApiIsolated
{
    public class CustomAnnouncement
    {
        [Function("CustomAnnouncement")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "secured/CustomAnnouncement")] HttpRequest req, ILogger log)
        {
            var value = req.Query["url"];

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject(requestBody);

            var responseMessage = string.IsNullOrEmpty(value) ? "empty" : "success";
            return new OkObjectResult(responseMessage);
        }
    }
}