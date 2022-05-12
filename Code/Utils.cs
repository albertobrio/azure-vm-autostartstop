using Microsoft.Azure.Management.Compute.Fluent;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace TimerTrigger1.Code
{
    public static class Utils
    {
        /// <summary>
        ///  Deallocate all VM in selected resourceGroup and subscription
        /// </summary>
        public static async Task DeallocateResourceGroupVmAsync(
            AzureCredentials credentials,
            string subscriptionId,
            string resourceGroupName,
            ILogger log)
        {
            log.LogInformation(
                $"Inizio procedura deallocazione VM Sottoscrizione {subscriptionId}, Gruppo Risorse {resourceGroupName}");

            var azure = Azure
                        .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription(subscriptionId);

            foreach(var virtualMachine in await azure.VirtualMachines.ListByResourceGroupAsync(resourceGroupName))
            {
                log.LogInformation($"Trovata VM {virtualMachine.Name}, Stato: {virtualMachine.PowerState}");

                if(virtualMachine.PowerState != PowerState.Deallocated)
                {
                    log.LogInformation($"VM {virtualMachine.Name} {virtualMachine.PowerState}");
                    virtualMachine.DeallocateAsync();
                    log.LogInformation($"VM {virtualMachine.Name} inviato comando di deallocazione");
                }
            }

            log.LogInformation(
                $"Fine procedura deallocazione VM Sottoscrizione {subscriptionId}, Gruppo Risorse {resourceGroupName}");
        }

        /// <summary>
        ///  Deallocate all VM in selected subscription
        /// </summary>
        public static async Task DeallocateSubscriptionVmAsync(
            AzureCredentials credentials,
            string subscriptionId,
            ILogger log)
        {
            log.LogInformation($"Inizio procedura deallocazione VM Sottoscrizione {subscriptionId}");

            var azure = Azure
                        .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription(subscriptionId);

            foreach(var resourceGroup in await azure.ResourceGroups.ListAsync())
            {
                log.LogInformation($"Gruppo di risorse {resourceGroup.Name}");

                foreach(var virtualMachine in await azure.VirtualMachines.ListByResourceGroupAsync(resourceGroup.Name))
                {
                    log.LogInformation($"Trovata VM {virtualMachine.Name}, Stato: {virtualMachine.PowerState}");

                    if(virtualMachine.PowerState != PowerState.Deallocated)
                    {
                        log.LogInformation($"VM {virtualMachine.Name} {virtualMachine.PowerState}");
                        virtualMachine.DeallocateAsync();
                        log.LogInformation($"VM {virtualMachine.Name} inviato comando di deallocazione");
                    }
                }
            }

            log.LogInformation($"Fine procedura deallocazione VM Sottoscrizione {subscriptionId}");
        }

        /// <summary>
        /// Get Azure Credentials
        /// </summary>
        public static AzureCredentials GetCredentials(string clientId, string clientSecret, string tenantId)
        {
            return SdkContext.AzureCredentialsFactory
                .FromServicePrincipal(clientId, clientSecret, tenantId, AzureEnvironment.AzureGlobalCloud);
        }

        /// <summary>
        /// Start all VM in selected resourceGroup and subscription
        /// </summary>
        public static async Task StartResourceGroupVmAsync(
            AzureCredentials credentials,
            string subscriptionId,
            string resourceGroupName,
            ILogger log)
        {
            log.LogInformation(
                $"Inizio procedura avvio VM Sottoscrizione {subscriptionId}, Gruppo Risorse {resourceGroupName}");

            var azure = Azure
                        .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription(subscriptionId);

            foreach(var virtualMachine in await azure.VirtualMachines.ListByResourceGroupAsync(resourceGroupName))
            {
                log.LogInformation($"Trovata VM {virtualMachine.Name}, Stato: {virtualMachine.PowerState}");

                if(virtualMachine.PowerState != PowerState.Running)
                {
                    log.LogInformation($"VM {virtualMachine.Name} {virtualMachine.PowerState}");
                    virtualMachine.StartAsync();
                    log.LogInformation($"VM {virtualMachine.Name} inviato comando di avvio");
                }
            }

            log.LogInformation(
                $"Fine procedura avvio VM Sottoscrizione {subscriptionId}, Gruppo Risorse {resourceGroupName}");
        }

        /// <summary>
        /// Start all VM in selected subscription
        /// </summary>
        public static async Task StartSubscriptionVmAsync(
            AzureCredentials credentials,
            string subscriptionId,
            ILogger log)
        {
            log.LogInformation($"Inizio procedura avvio VM Sottoscrizione {subscriptionId}");

            var azure = Azure
                        .Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(credentials)
                .WithSubscription(subscriptionId);

            foreach(var resourceGroup in await azure.ResourceGroups.ListAsync())
            {
                log.LogInformation($"Gruppo di risorse {resourceGroup.Name}");

                foreach(var virtualMachine in await azure.VirtualMachines.ListByResourceGroupAsync(resourceGroup.Name))
                {
                    log.LogInformation($"Trovata VM {virtualMachine.Name}, Stato: {virtualMachine.PowerState}");

                    if(virtualMachine.PowerState != PowerState.Running)
                    {
                        log.LogInformation($"VM {virtualMachine.Name} {virtualMachine.PowerState}");
                        virtualMachine.StartAsync();
                        log.LogInformation($"VM {virtualMachine.Name} inviato comando di avvio");
                    }
                }
            }

            log.LogInformation($"Fine procedura avvio VM Sottoscrizione {subscriptionId}");
        }
    }
}
