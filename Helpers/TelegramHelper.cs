
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
        private static readonly string token = "5130591097:AAF6jNtd1H3l9baweL7QQsD5npn2ODqmlhk";
        private static TelegramBotClient? _client;
        private static readonly string _id = "595478648";

        public static async Task SendMessage()
        {
            try
            {
                _client = new TelegramBotClient(token);
                var filePath = ScreenShotHelper.TakeScreenshot();

                using (var stream = System.IO.File.OpenRead(filePath))
                {
                    var inputOnlineFile = new InputOnlineFile(stream, filePath);
                    var message = $"The test-case \"{TestContext.CurrentContext.Test.Name}\" has failed\n\n{TestContext.CurrentContext.Result.Message}";

                    await _client.SendTextMessageAsync(
                        chatId: _id,
                        text: message
                    );

                    await _client.SendPhotoAsync(
                        chatId: _id,
                        photo: inputOnlineFile
                    );
                }
            }
            catch (Exception)
            {
                // Handle the exception here
            }
        }
    }
}
