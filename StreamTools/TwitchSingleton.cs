using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchUtils;

namespace StreamTools
{
    public sealed class TwitchSingleton : TwitchClient
    {
        private static readonly Lazy<TwitchSingleton> _instance = new Lazy<TwitchSingleton>(() => new TwitchSingleton());
        public static TwitchSingleton Instance => _instance.Value;

        private TwitchSingleton()
        {

        }
    }
}
