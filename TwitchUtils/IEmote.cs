using System.Collections.Generic;

namespace TwitchUtils
{
    public interface IEmote
    {
        string EmoteId { get; set; }
        Dictionary<int, int> Coords { get; set; }
    }
}
