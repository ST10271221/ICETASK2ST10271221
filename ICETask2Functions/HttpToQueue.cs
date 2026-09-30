using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ICETask2Functions
{
    public class HttpToQueue
    {
        private readonly ILogger<HttpToQueue> _logger;

        public HttpToQueue(ILogger<HttpToQueue> logger)
        {
            _logger = logger;
        }

        [Function("HttpToQueue")]
        public async Task<MultiResponse> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequest req)
        {
            using var reader = new StreamReader(req.Body);

            string message = await reader.ReadToEndAsync();

            _logger.LogInformation(
                "HTTP request received: {Message}",
                message);

            return new MultiResponse
            {
                Messages = new[] { message },

                HttpResponse = new OkObjectResult(new
                {
                    success = true,
                    message = "Message received and sent to queue."
                })
            };
        }
    }

    public class MultiResponse
    {
        [QueueOutput("ice-task-queue", Connection = "AzureWebJobsStorage")]
        public string[] Messages { get; set; } = Array.Empty<string>();

        [HttpResult]
        public IActionResult HttpResponse { get; set; } = new OkResult();
    }
}