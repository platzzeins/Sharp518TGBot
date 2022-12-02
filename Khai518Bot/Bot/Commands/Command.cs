﻿using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Khai518Bot.Bot.Commands;

public abstract class Command
{
    public abstract Task Execute(Service service);
    public required ITelegramBotClient BotClient { get; set; }
    public required Update Update { get; set; }
    protected string CallbackData => Update.CallbackQuery!.Data!;
    protected Message Message => Update.Message ?? Update.CallbackQuery!.Message!;
    protected int? NumberFromQuery()
    {
        var sNum = CallbackData.Split(":").ElementAtOrDefault(1);
        if (string.IsNullOrEmpty(sNum)) return null;
        if (!int.TryParse(sNum, out var num)) return null;
        return num;
    }
    protected async Task TryEditMessage(string text, InlineKeyboardMarkup? keyboard = null)
    {
        try
        {
            await BotClient.EditMessageTextAsync(Message.Chat.Id, Message.MessageId, text, ParseMode.Html,
                replyMarkup: keyboard, disableWebPagePreview: true);
        }
        catch (Exception)
        {
            // ignored
        }
        finally
        {
            if (Update.Type == UpdateType.CallbackQuery)
                await BotClient.AnswerCallbackQueryAsync(Update.CallbackQuery!.Id);
        }
    }
}