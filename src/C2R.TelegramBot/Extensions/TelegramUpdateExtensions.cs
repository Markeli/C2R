using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace C2RTelegramBot.Extensions
{
    internal static class TelegramUpdateExtensions
    {
        public static ChatId GetChatId(this Update update)
        {
            ChatId chatId;

            switch (update.Type)
            {
                case UpdateType.MessageUpdate:
                    chatId = update.Message.Chat.Id;
                    break;
                case UpdateType.ChannelPost:
                    chatId = update.ChannelPost.Chat.Id;
                    break;
                case UpdateType.CallbackQueryUpdate:
                    chatId = update.CallbackQuery.Message.Chat.Id;
                    break;
                case UpdateType.EditedMessage:
                    chatId = update.EditedMessage.Chat.Id;
                    break;
                default:
                    chatId = null;
                    break;
            }

            return chatId;
        }

        public static int GetMessageId(this Update update)
        {
            int msgId;

            switch (update.Type)
            {
                case UpdateType.MessageUpdate:
                    msgId = update.Message.MessageId;
                    break;
                case UpdateType.ChannelPost:
                    msgId = update.ChannelPost.MessageId;
                    break;
                case UpdateType.CallbackQueryUpdate:
                    msgId = update.CallbackQuery.Message.MessageId;
                    break;
                default:
                    msgId = default(int);
                    break;
            }

            return msgId;
        }

        
    }
}