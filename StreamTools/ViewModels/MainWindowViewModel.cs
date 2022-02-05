using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreamTools.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string? oauthToken;
        private string? username;
        private string? channel;

        public string OAuthToken
        {
            get => "";
            set => this.RaiseAndSetIfChanged(ref oauthToken, value);
        }
        public string Username
        {
            get => "";
            set => this.RaiseAndSetIfChanged(ref username, value);
        }
        public string Channel
        {
            get => "";
            set => this.RaiseAndSetIfChanged(ref channel, value);
        }

        public void AuthAndConnect()
        {
            if (username != null && oauthToken != null)
            {
                bool isConnected = TwitchSingleton.Instance.Connect();
                if (isConnected)
                {
                    System.Diagnostics.Debug.WriteLine("Connected to Twitch!");
                    TwitchSingleton.Instance.Authenticate(username, oauthToken);
                }
            }
        }
    }
}
