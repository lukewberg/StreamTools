using System;
using System.Text;
using System.Threading.Tasks;
using TwitchUtils;

namespace FirstConsoleApp
{
    internal class Program
    {
        //private static readonly HttpClient client = new HttpClient();
        //private readonly Socket socket = new Socket(SocketType);

        static void Main()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Console.Title = "Twitch Console Chat";

            void MessageHandler(ITwitchMessage message)
            {
                Console.WriteLine($"{message.DisplayName}: {message.Message}");

                if (message.Emotes.Count > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(message.Emotes[0].EmoteId);
                    Console.ResetColor();
                }
            }
            Action<ITwitchMessage> action = new Action<ITwitchMessage>(MessageHandler);
            TwitchChatSocket twitchChatSocket = new TwitchChatSocket(action);
            twitchChatSocket.ConnectToTwitch();
            //Console.ForegroundColor = ConsoleColor.Blue;
            string userInput = Console.ReadLine();

            if (userInput == "beep")
            {
                Console.Write("Entered Beep mode. How many beeps: ");
                int count = Int32.Parse(Console.ReadLine());
                for (int i = count - 1; i >= 0; i--)
                {
                    Console.Beep();
                    Task.Delay(500);
                }
            }
            Console.Out.WriteLine(userInput);
        }
    }
}
