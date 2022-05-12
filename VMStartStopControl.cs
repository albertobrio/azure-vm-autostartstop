using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TimerTrigger1.Code;

namespace TimerTrigger1
{
    public static class VMStartStopControl
    {
        static string clientId = "App Client Id";
        static string clientSecret = "App Secret Id";
        static string tenantId = "Tenant Id";
        #region "Sample 1"
        // Start at 07:00, everyday  => 0 00 7 * * *
        [FunctionName("StartSample1")]
        public static async Task StartSample1([TimerTrigger("0 00 7 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Funzione Timer Start eseguita alle: {DateTime.Now}");
            var credentials = Utils.GetCredentials(clientId, clientSecret, tenantId);
            await Utils.StartSubscriptionVmAsync(credentials, "Subscription Id", log);
        }

        // Deallocate at  00:45, everyday => 0 45 0 * * *
        [FunctionName("DeallocateSample1")]
        public static async Task Run([TimerTrigger("0 45 0 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Funzione Timer spegnimento eseguita alle: {DateTime.Now}");
            var credentials = Utils.GetCredentials(clientId, clientSecret, tenantId);
            await Utils.DeallocateSubscriptionVmAsync(credentials, "Subscription Id", log);
        }
            #endregion

        #region "Sample 2"
        // Start at 07:00, everyday  => 0 00 7 * * *
        [FunctionName("StartSample2")]
        public static async Task StartSample2([TimerTrigger("0 00 7 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Funzione Timer Start eseguita alle: {DateTime.Now}");
            var credentials = Utils.GetCredentials(clientId, clientSecret, tenantId);
            await Utils.StartResourceGroupVmAsync(
                credentials,
             "Subscription Id",
                "RES GROUP NAME",
                log);
        }

        // Deallocate at  00:45, everyday => 0 45 0 * * *
        [FunctionName("DeallocateSample2")]
        public static async Task DeallocateSample2([TimerTrigger("0 45 0 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Funzione Timer spegnimento eseguita alle: {DateTime.Now}");
            var credentials = Utils.GetCredentials(clientId, clientSecret, tenantId);
            await Utils.DeallocateResourceGroupVmAsync(
                credentials,
                "Subscription Id",
                "RES GROUP NAME",
                log);
        }
        #endregion

        #region "Sample 3"
        // Start at 07:00, lun-ven  => 0 00 7 * * 1-5
        [FunctionName("StartSample3")]
        public static async Task StartSample3([TimerTrigger("0 00 7 * * 1-5")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Funzione Timer Start eseguita alle: {DateTime.Now}");
            var credentials = Utils.GetCredentials(clientId, clientSecret, tenantId);
            await Utils.StartResourceGroupVmAsync(
                credentials,
                  "Subscription Id",
                "RES GROUP NAME",
                log);
        }


        // Spegnimento ore 20:00, lun-ven  => 0 00 20 * * 1-5
        [FunctionName("DeallocateSample3")]
        public static async Task DeallocateSample3([TimerTrigger("0 00 20 * * 1-5")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Funzione Timer spegnimento eseguita alle: {DateTime.Now}");
            var credentials = Utils.GetCredentials(clientId, clientSecret, tenantId);
            await Utils.DeallocateResourceGroupVmAsync(
                credentials,
                "Subscription Id",
                "RES GROUP NAME",
                log);
        }
        #endregion
    }
}
