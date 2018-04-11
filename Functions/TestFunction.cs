#region Directives

using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

#endregion

namespace Functions
{
    public static class TestFunction
    {
        #region Methods

        [FunctionName("TestFunction")]
        public static void Run([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            AccessCloudStorage(log);
        }

        private static void AccessCloudStorage(ILogger log)
        {
            const string connectionString =
                "<Azure Storage Connection String>";

            var client = new Lazy<CloudTableClient>(() =>
            {
                var account = CloudStorageAccount.Parse(connectionString);
                return account.CreateCloudTableClient();
            });
            var table = client.Value.GetTableReference("AlertDefinitions");
            log.LogInformation($"Retrieved table: {table.Name}");
        }

        #endregion
    }
}