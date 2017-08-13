using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace Bot_Application1.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            string reponse_text = await GetReply(activity.Text, 3);
            await context.PostAsync(reponse_text);

            context.Wait(MessageReceivedAsync);
        }

        private async Task<string> GetReply(string query, int count = 3)
        {
            string result = await myrest.GetDataFromRestCall(query, count);
            return string.Format("For : {0}\n you can check these: \n{1}", query, result);

        }
    }
}