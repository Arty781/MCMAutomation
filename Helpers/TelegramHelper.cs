
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;

namespace MCMAutomation.Helpers
{
    public partial class TelegramHelper
    {
        private static string token = "5130591097:AAF6jNtd1H3l9baweL7QQsD5npn2ODqmlhk";
        private static TelegramBotClient? _clientM;
        private static TelegramBotClient? _clientI;
        private static readonly string _id = "595478648";

        public static async Task SendMessage()
        {
            _clientM = new TelegramBotClient(token);
            using var cts = new CancellationTokenSource();

            _clientM.StartReceiving(
                cancellationToken: cts.Token);

            Message message = await _clientM.SendTextMessageAsync(_id, "The test-case \"" +
                TestContext.CurrentContext.Test.Name.ToString() +
                "\" has failed" + "\n" + "\n" +
                TestContext.CurrentContext.Result.Message.ToString());
            cts.Cancel();
            _clientM.StopReceiving();

        }

        public static async Task SendImage()
        {
            _clientI = new TelegramBotClient(token);
            using var cts = new CancellationTokenSource();

            _clientI.StartReceiving(
                cancellationToken: cts.Token);
            using (FileStream stream = System.IO.File.OpenRead(ScreenShotHelper.MakeScreenShot()))
            {
                InputOnlineFile inputOnlineFile = new InputOnlineFile(stream, ScreenShotHelper.MakeScreenShot());
                await _clientI.SendDocumentAsync(_id, inputOnlineFile);
            }
            cts.Cancel();
            _clientI.StopReceiving();
        }

    }
}
