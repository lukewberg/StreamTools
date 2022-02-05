using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchUtils
{
    public class TwitchClient
    {
        private readonly TcpClient _tcpClient;
        private StreamWriter _writer;
        private StreamReader _reader;
        public Action<bool>? OnMessageSend { get; set; }
        public Action<ITwitchMessage>? OnMessageRecieve { get; set; }

        public TwitchClient()
        {
            _tcpClient = new TcpClient();
            TwitchMessageUtils = new TwitchMessageUtils();
        }
        public TwitchMessageUtils TwitchMessageUtils { get; set; }

        private void RequestTags()
        {
            WriteMessage("CAP REQ :twitch.tv/tags");
            WriteMessage("CAP REQ :twitch.tv/membership");
            WriteMessage("CAP REQ :twitch.tv/commands");
        }

        public bool WriteMessage(string message)
        {
            try
            {
                _writer.WriteLine(message);
                _writer.Flush();

                if (OnMessageSend != null)
                {
                    OnMessageSend(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                if (OnMessageRecieve != null)
                {
                    OnMessageSend(false);
                }
                return false;
            }
        }

        private async Task<string> ReadMessages()
        {
            try
            {
                string result = await _reader?.ReadLineAsync();
                if (result != null && result.Contains("PING"))
                {
                    Console.WriteLine("Replying with PONG...");
                    _writer.WriteLine("PONG :tmi.twitch.tv");
                    _writer.Flush();
                }
                else
                {
                    return result;
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return ex.Message;
            }
        }

        private async void LoopRead()
        {
            while (_tcpClient.Connected)
            {
                string message = await ReadMessages();
                ITwitchMessage twitchMessage = TwitchMessageUtils.ParseMessage(message);
                if (OnMessageRecieve != null)
                {
                    OnMessageRecieve(twitchMessage);
                }
            }
        }

        public void Authenticate(string username, string oAuthToken)
        {
            // TODO: Find some way to indicate success
            WriteMessage($"PASS {oAuthToken}");
            WriteMessage($"NICK {username}");
        }

        public void JoinChannel(string channel)
        {
            WriteMessage($"JOIN #{channel}");
            RequestTags();
        }

        public bool Connect()
        {
            if(_tcpClient.Connected == true)
            {
                return false;
            }
            _tcpClient.Connect("irc.chat.twitch.tv", 6667);
            _tcpClient.ReceiveTimeout = 5000;

            if (_tcpClient.Connected)
            {
                Console.WriteLine("Connected to Twitch IRC");
            }

            Stream tcpStream = _tcpClient.GetStream();
            _reader = new StreamReader(tcpStream);
            _writer = new StreamWriter(tcpStream);

            Thread thread = new Thread(LoopRead);
            thread.Start();
            return _tcpClient.Connected;
        }

    }
}