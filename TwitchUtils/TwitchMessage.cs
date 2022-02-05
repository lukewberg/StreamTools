using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TwitchUtils
{
    public partial class TwitchMessageUtils
    {
        public struct TwitchMessage : ITwitchMessage
        {
            public string BadgeInfo { get; set; }
            public string Badges { get; set; }
            public string Color { get; set; }
            public string DisplayName { get; set; }
            public List<IEmote> Emotes { get; set; }
            public int FirstMsg { get; set; }
            public string Flags { get; set; }
            public string Id { get; set; }
            public int Mod { get; set; }
            public string RoomId { get; set; }
            public int Subscriber { get; set; }
            public string TmiSentTs { get; set; }
            public int Turbo { get; set; }
            public string UserId { get; set; }
            public string UserType { get; set; }

            private MatchCollection _metaContents { get; set; }
            private MatchCollection _messageContents { get; set; }
            public string Message { get; set; }

        }

    }

}
