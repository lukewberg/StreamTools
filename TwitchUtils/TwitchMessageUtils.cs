using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TwitchUtils
{
    public partial class TwitchMessageUtils
    {
        public struct Emote : IEmote
        {
            public string EmoteId { get; set; }
            public Dictionary<int, int> Coords { get; set; }
        }
        public TwitchMessage twitchMessage { get; set; }
        private string? _message { get; set; }

        private TwitchMessage GetTwitchMessage()
        {
            return new TwitchMessage();
        }

        public TwitchMessage ParseMessage(string message)
        {
            _message = message;
            TwitchMessage twitchMessage = GetTwitchMessage();
            twitchMessage.Emotes = new List<IEmote>();
            string metaPattern = @"[^@;]*?=[^; ]*";
            string messagePattern = @"PRIVMSG #[^:]*:(.*)";
            MatchCollection metaMatches = Regex.Matches(_message, metaPattern);
            MatchCollection messageContents = Regex.Matches(_message, messagePattern);
            if (metaMatches.Count > 0)
            {
                AssignProperties(ref twitchMessage, metaMatches);
            }
            if (messageContents.Count > 0)
            {
                twitchMessage.Message = messageContents[0].Groups[1].Value;
            }
            else
            {
                twitchMessage.Message = _message;
            }
            return twitchMessage;
        }
        private void AssignProperties(ref TwitchMessage twitchMessage, MatchCollection metaCollection)
        {
            foreach (Match match in metaCollection)
            {
                string value = match.Value.Split('=')[1];
                switch (match.Value.Split('=')[0])
                {
                    case "badge-info":
                        twitchMessage.BadgeInfo = value;
                        break;

                    case "badges":
                        twitchMessage.Badges = value;
                        break;

                    case "color":
                        twitchMessage.Color = value;
                        break;

                    case "display-name":
                        twitchMessage.DisplayName = value;
                        break;

                    case "emotes":
                        if (!string.IsNullOrEmpty(value))
                        {
                            foreach (string emote in value.Split('/'))
                            {
                                Emote emoteStruct = new Emote();
                                string[] emoteParts = emote.Split(':');
                                emoteStruct.EmoteId = emoteParts[0];
                                if (emoteParts.Length > 1)
                                {
                                    foreach (string coords in emoteParts[1].Split(','))
                                    {
                                        string[] coordsParts = coords.Split('-');
                                        Dictionary<int, int> coordsDict = new Dictionary<int, int>();
                                        //KeyValuePair<int, int> keyValuePair = new KeyValuePair<int, int>(int.Parse(coordsParts[0]), int.Parse(coordsParts[1]));
                                        coordsDict.Add(int.Parse(coordsParts[0]), int.Parse(coordsParts[1]));
                                        emoteStruct.Coords = coordsDict;
                                    }
                                }
                                twitchMessage.Emotes.Add(emoteStruct);
                            }
                        }
                        break;

                    case "first-msg":
                        twitchMessage.FirstMsg = Int32.Parse(value);
                        break;

                    case "flags":
                        twitchMessage.Flags = value;
                        break;

                    case "id":
                        twitchMessage.UserId = value;
                        break;


                    default:
                        break;
                }
            }
        }

    }

}
