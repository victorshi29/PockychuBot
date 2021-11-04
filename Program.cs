using System;
using System.Threading.Tasks;

namespace twitchchatbot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string uri = "irc.chat.twitch.tv";
            string password = "c7lmtmebed84ctn5fpbhtbppbt69bc";
            string botUsername = "pockychubot";
            int port = 6667;
            string line, message, sender;
            string[] split;
            int secondColonPosition;

            var tcpClient = new System.Net.Sockets.TcpClient();
            await tcpClient.ConnectAsync(uri, port);
            var streamWriter = new System.IO.StreamWriter(tcpClient.GetStream()) { NewLine = "\r\n", AutoFlush = true};
            var streamReader = new System.IO.StreamReader(tcpClient.GetStream());

            await streamWriter.WriteLineAsync($"PASS {password}");
            await streamWriter.WriteLineAsync($"NICK {botUsername}");
            await streamWriter.WriteLineAsync($"JOIN #morenaptime");
            await streamWriter.WriteLineAsync("PRIVMSG #morenaptime :This is a test message");
            
            while (true)
            {
                line = await streamReader.ReadLineAsync();
                Console.WriteLine(line);
                split = line.Split(" ");

                //PING :tmi.twitch.tv
                //Respond with PONG :tmi.twitch.tv
                if (line.StartsWith("PING"))
                {
                    Console.WriteLine("PING");
                    await streamWriter.WriteLineAsync($"PONG {split[1]}");
                }

                //sample line from streamReader
                //:morenaptime!morenaptime@morenaptime.tmi.twitch.tv PRIVMSG #morenaptime :!pika sfsfdsf
                if (split[1].Equals("PRIVMSG"))
                {
                    sender = split[2].Substring(1);
                    secondColonPosition = line.IndexOf(':', 1);
                    message = line.Substring(secondColonPosition + 1);
                    Console.WriteLine(message);
                    if (message.Equals("!pika"))
                    {
                        string pika = "⢀⣠⣾⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⠀⣠⣤⣶⣶ ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠀⠀⠀⢰⣿⣿⣿⣿ ⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣧⣀⣀⣾⣿⣿⣿⣿ ⣿⣿⣿⣿⣿⡏⠉⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣿ ⣿⣿⣿⣿⣿⣿⠀⠀⠀⠈⠛⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠛⠉⠁⠀⣿ ⣿⣿⣿⣿⣿⣿⣧⡀⠀⠀⠀⠀⠙⠿⠿⠿⠻⠿⠿⠟⠿⠛⠉⠀⠀⠀⠀⠀⣸⣿ ⣿⣿⣿⣿⣿⣿⣿⣷⣄⠀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣿⣿ ⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠠⣴⣿⣿⣿⣿ ⣿⣿⣿⣿⣿⣿⣿⣿⡟⠀⠀⢰⣹⡆⠀⠀⠀⠀⠀⠀⣭⣷⠀⠀⠀⠸⣿⣿⣿⣿ ⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⠈⠉⠀⠀⠤⠄⠀⠀⠀⠉⠁⠀⠀⠀⠀⢿⣿⣿⣿ ⣿⣿⣿⣿⣿⣿⣿⣿⢾⣿⣷⠀⠀⠀⠀⡠⠤⢄⠀⠀⠀⠠⣿⣿⣷⠀⢸⣿⣿⣿ ⣿⣿⣿⣿⣿⣿⣿⣿⡀⠉⠀⠀⠀⠀⠀⢄⠀⢀⠀⠀⠀⠀⠉⠉⠁⠀⠀⣿⣿⣿ ⣿⣿⣿⣿⣿⣿⣿⣿⣧⠀⠀⠀⠀⠀⠀⠀⠈⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢹⣿⣿ ⣿⣿⣿⣿⣿⣿⣿⣿⣿⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⣿";
                        await streamWriter.WriteLineAsync($"PRIVMSG #morenaptime : {pika}");
                    }
                }
            }
        }
    }
}
