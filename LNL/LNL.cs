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

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [FunctionName("Dequeue")]
        public static HttpResponseMessage Dequeue([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP Dequeue trigger function processed a request.");

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private static CloudQueue GetQueue()
        {
            return null;
        }
    }
}
