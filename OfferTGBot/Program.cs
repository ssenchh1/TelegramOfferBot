using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OfferTGBot
{
    class Program
    {
        static void Main(string[] args)
        {
            string token = "1429392176:AAGuXxFVvyWKXdMIqSctCX8W2bH_38dibNrandomtoken";
            int AdminChatId = -383874479;
            TelegramBotClient bot = new TelegramBotClient(token);
            Message message = new Message();
            long originalsender = 0;
            long originalchat = 0;


            bot.OnMessage += delegate (object s, Telegram.Bot.Args.MessageEventArgs e)
            {
                message = e.Message;

                if (e.Message.Text == "/start")
                {
                    originalsender = e.Message.From.Id;
                    originalchat = e.Message.Chat.Id;
                    bot.SendTextMessageAsync(originalchat, "Смело пиши сюда новости, мысли или отправляй картинки(мемы), а мы их запостим. А на вопросы и просьбы ответим");
                }
                else
                {

                    if (e.Message.ReplyToMessage != null)
                    {
                        if (e.Message.ReplyToMessage.Chat.Id == AdminChatId)
                        {
                            bot.SendTextMessageAsync(e.Message.ReplyToMessage.ForwardFromChat.Id, message.Text);
                        }
                    }
                    else
                    {
                        originalsender = e.Message.From.Id;
                        originalchat = e.Message.Chat.Id;

                        bot.ForwardMessageAsync(chatId: AdminChatId, e.Message.Chat.Id, e.Message.MessageId);
                        Console.WriteLine($"{e.Message.MessageId}, {e.Message.Type} in chat {e.Message.Chat.Id} with {e.Message.From.Username} {e.Message.From.Id}");
                    }
                }
            };



            bot.StartReceiving();

            Console.ReadKey();
        }
    }
}