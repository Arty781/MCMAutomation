
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
        private static readonly string _id = "595478648";

        public static async Task SendMessage()
        {
            _clientM = new TelegramBotClient(token);
            string filePath = ScreenShotHelper.MakeScreenShot();
            FileStream stream = System.IO.File.OpenRead(filePath);
            var inputOnlineFile = new InputOnlineFile(stream, filePath);

            Message message1 = await _clientM.SendTextMessageAsync(
                chatId: _id,
                text: "The test-case \"" + TestContext.CurrentContext.Test.Name.ToString() +
                "\" has failed" + "\n" + "\n" + TestContext.CurrentContext.Result.Message);

            Message message2 = await _clientM.SendPhotoAsync(
                chatId: _id, 
                photo: inputOnlineFile
                );
        }

    }
}
