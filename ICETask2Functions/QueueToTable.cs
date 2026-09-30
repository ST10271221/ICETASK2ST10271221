using ICETask2Functions.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ICETask2Functions
{
    public class QueueToTable
    {
        private readonly ILogger<QueueToTable> _logger;

        public QueueToTable(ILogger<QueueToTable> logger)
        {
            _logger = logger;
        }

        [Function("QueueToTable")]
        [TableOutput("IceTaskTable", Connection = "AzureWebJobsStorage")]
        public IceTaskEntity Run(
            [QueueTrigger("ice-task-queue", Connection = "AzureWebJobsStorage")]
            string message)
        {
            _logger.LogInformation(
                "Queue message received: {Message}",
                message);

            return new IceTaskEntity
            {
                PartitionKey = "IceTask2",
                RowKey = Guid.NewGuid().ToString(),
                Message = message
            };
        }
    }
}