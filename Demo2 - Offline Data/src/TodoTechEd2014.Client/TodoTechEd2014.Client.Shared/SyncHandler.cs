using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace TodoTechEd2014.Client
{

    public class SyncHandler : IMobileServiceSyncHandler
    {
        public async Task<JObject> ExecuteTableOperationAsync(IMobileServiceTableOperation operation)
        {
            while (true)
            {
                MobileServicePreconditionFailedException error;

                try
                {
                    return await operation.ExecuteAsync();
                }
                catch (MobileServicePreconditionFailedException ex)
                {
                    error = ex;
                }

                if (error == null) break;

                var localValue = operation.Item.ToObject<TodoItem>();
                var remoteValue = error.Value;

                var conflictResolutionDialog = new MessageDialog("How shall we proceed?\r\n\r\n" +
                    "Local Item: " + localValue.Text + "\r\n" +
                    "Remote Item: " + remoteValue.ToObject<TodoItem>().Text,
                    "Conflict detected! "
                );

                conflictResolutionDialog.Commands.Add(new UICommand("Use Local Version"));
                conflictResolutionDialog.Commands.Add(new UICommand("Use Remote Version"));
                conflictResolutionDialog.Commands.Add(new UICommand("I can't decide! Get me out of here.."));

                var command = await conflictResolutionDialog.ShowAsync();

                if (command.Label.Contains("Local"))
                {
                    operation.Item[MobileServiceSystemColumns.Version] = 
                        remoteValue[MobileServiceSystemColumns.Version];

                    continue;
                }
                else if (command.Label.Contains("Remote"))
                {
                    return remoteValue;
                }
                else
                {
                    operation.AbortPush();
                }
            }

            return null;
        }

        public async Task OnPushCompleteAsync(MobileServicePushCompletionResult result)
        {
            await Task.Yield();
        }
    }
}
