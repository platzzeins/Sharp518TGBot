﻿using Telegram.Bot.Types.Enums;

namespace Khai518Bot.Bot.Commands.Entity;

[UsedImplicitly]
[Command(@"getrosp")]
public class GetRosp : Command
{
    public override async Task Execute(Service service)
    {
        var text = await service.GetOneDayText(service.TodayShowId);
        var keyboard = await service.GetOneDayKeyboard(service.TodayShowId);
        await BotClient.SendTextMessageAsync(Update.Message!.Chat.Id,
            text, ParseMode.Html, replyMarkup: keyboard);
    }
}