using Azure;
using Azure.Data.Tables;

namespace ICETask2Functions.Models
{
    public class IceTaskEntity : ITableEntity
    {
        public string PartitionKey { get; set; } = "IceTask2";

        public string RowKey { get; set; } = Guid.NewGuid().ToString();

        public string Message { get; set; } = string.Empty;

        public DateTimeOffset? Timestamp { get; set; }

        public ETag ETag { get; set; }
    }
}