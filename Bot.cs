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
        public static string RockPaperScissors(string first, string second)
    => (first, second) switch
    {
        ("rock", "paper") => "rock is covered by paper. Paper wins.",
        ("rock", "scissors") => "rock breaks scissors. Rock wins.",
        ("paper", "rock") => "paper covers rock. Paper wins.",
        ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
        ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
        ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
        (_, _) => "ничья"
    };

        [Obsolete]
        private async void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            try
            {
                Message message = messageEventArgs.Message;
                var startup = new ReplyKeyboardMarkup(new[]
                {
                 new KeyboardButton("/start") });
                if (message.Text == "/start")
                {
                    var chooseup = new ReplyKeyboardMarkup(new[]
                    {
                    new KeyboardButton("/game"),
                    new KeyboardButton("/help")});
                    message = messageEventArgs.Message;
                    await _bot.SendTextMessageAsync(message.Chat.Id,message.Text, replyMarkup: chooseup);
                }
                if (message.Text == "/game")
                {
                    Console.WriteLine(message);
                    var markup = new ReplyKeyboardMarkup(new[]
                    {
                             new KeyboardButton("rock"),

                             new KeyboardButton("paper"),

                              new KeyboardButton("scissors"),

                        });
                    markup.OneTimeKeyboard = true;
                    await _bot.SendTextMessageAsync(message.Chat.Id, message.Text, replyMarkup: markup);

                }
                else if (message.Text == "/help")
                {
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Суть данной игры заключается в выборе кнопки с названием фигуры из игры камень-ножницы-бумаги." +
                        " Для начало игры пропишите /game в чат");

                }
                if(message.Text == "rock" || message.Text == "paper" || message.Text == "scissors")
                {
                    var markup = new ReplyKeyboardMarkup(new[]
{
                             new KeyboardButton("rock"),

                             new KeyboardButton("paper"),

                              new KeyboardButton("scissors"),
                              new KeyboardButton("/stop"),

                        });
                    markup.OneTimeKeyboard = true;
                    string[] arr =
{
                        "rock",
                         "scissors",
                        "paper"
                        };
                    Random rnd = new Random();
                    await _bot.SendTextMessageAsync(message.Chat.Id, RockPaperScissors(message.Text, arr[rnd.Next(1, 3)]), replyMarkup: markup);
                }
                if(message.Text == "/stop")
                {
                    await _bot.SendTextMessageAsync(message.Chat.Id, message.Text, replyMarkup: startup);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


        }
    }
}
