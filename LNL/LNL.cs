using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;

namespace LNL
{
    public static class Lnl
    {
        private const string StorageAccountName = "lnlqueue";
        private const string StorageAccountKey = "";
        private const string QueueName = "lnlqueue";

        [FunctionName("Queue")]
        public static HttpResponseMessage Queue([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP Queue trigger function processed a request.");

            // parse query parameter
            var id = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "id", StringComparison.OrdinalIgnoreCase) == 0)
                .Value;

            var queue = GetQueue();
            
            queue.AddMessage(new CloudQueueMessage(id));

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [FunctionName("Dequeue")]
        public static HttpResponseMessage Dequeue([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP Dequeue trigger function processed a request.");

            var queue = GetQueue();

            var message = queue.GetMessage();

            if (message != null)
            {
                log.Info(message.AsString);
                queue.DeleteMessage(message);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private static CloudQueue GetQueue()
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), true);

            var client = storageAccount.CreateCloudQueueClient();

            var queue = client.GetQueueReference(QueueName);
            queue.CreateIfNotExists();

            return queue;
        }
    }
}
