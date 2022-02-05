using System.Collections.Generic;

namespace TwitchUtils
{
    public interface ITwitchMessage
    {
        string BadgeInfo { get; set; }
        string Badges { get; set; }
        string Color { get; set; }
        string DisplayName { get; set; }
        List<IEmote> Emotes { get; set; }
        int FirstMsg { get; set; }
        string Flags { get; set; }
        string Id { get; set; }
        int Mod { get; set; }
        string RoomId { get; set; }
        int Subscriber { get; set; }
        string TmiSentTs { get; set; }
        int Turbo { get; set; }
        string UserId { get; set; }
        string UserType { get; set; }

        //MatchCollection _metaContents { get; set; }
        //MatchCollection _messageContents { get; set; }
        string Message { get; set; }
    }
}
