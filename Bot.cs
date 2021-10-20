using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace _69
{
    class Bot
    {
        private readonly TelegramBotClient _bot;

        public Bot(string token)
        {
            _bot = new TelegramBotClient(token);
        }

        [Obsolete]
        public void StartBot()
        {
            _bot.OnMessage += OnMessageReceived;
            _bot.StartReceiving();

            while (true)
            {
                Console.WriteLine("Bot is worked alright");
                Thread.Sleep(int.MaxValue);
            }
        }


        private async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            try
            {
                Message message = messageEventArgs.Message;
                Console.WriteLine(message);
                var markup = new ReplyKeyboardMarkup(new[]
                {
                 new KeyboardButton("Привет"),

                 new KeyboardButton("Hello"),

                 new KeyboardButton("Heil"),

                });

                markup.OneTimeKeyboard = true;
                await _bot.SendTextMessageAsync(message.Chat.Id, message.Text,replyMarkup:markup);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
