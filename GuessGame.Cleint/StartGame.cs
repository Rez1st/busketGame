using System;
using System.Collections.Generic;
using GuessGame.Models;
using GuessGame.Models.Models;
using GuessGame.Service;

namespace GuessGame.Cleint
{
    public static class StartGame
    {
        public static ICollection<Player> Players = new List<Player>();
        private static int _playersCount { get; set; }

        public static void GetDataToSetupTheGame()
        {
            Console.WriteLine(Rules.GameWelcomeMessage);
            Console.WriteLine();
            Console.WriteLine(Rules.GameDescription);
            Console.WriteLine();

            RequestPlayerCountInput();

            for (var i = 0; i < _playersCount; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Player {0} setup", i + 1);

                ReguestPlayer();

                Console.WriteLine();
                Console.WriteLine("Player {0} setup finished succesfully", i + 1);
            }

            Console.WriteLine();
            Console.WriteLine("Real basket weight: {0}", Rules.RealWeight);

            Console.WriteLine("Processing...");
        }

        private static void RequestPlayerCountInput()
        {
            Console.WriteLine("How many awesome guys going to play?");

            var playersCount = GetNumer();

            if (InputValidate.SetPlayersCount(playersCount))
            {
                _playersCount = playersCount;
            }
            else
            {
                Console.WriteLine("Players count should be between {0} and {1}", Rules.MinPlayers, Rules.MaxPlayers);

                RequestPlayerCountInput();
            }
        }

        private static void ReguestPlayer()
        {
            Console.WriteLine();
            Console.WriteLine("Hello challanger - how shold we call you?");

            var name = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Pick challenger type");

            foreach (PlayerType player in Enum.GetValues(typeof(PlayerType)))
            {
                Console.WriteLine("{0} = {1}", player, Convert.ToInt32(player));
            }

            Console.WriteLine();

            GetPlayerType(name);
        }

        private static void GetPlayerType(string name)
        {
            var pick = GetNumer();

            if (pick > 0 && pick < 6)
            {
                switch (pick)
                {
                    case 1:
                        Players.Add(new RandomPlayer {Name = name});
                        break;
                    case 2:
                        Players.Add(new MemoryPlayer {Name = name});
                        break;
                    case 3:
                        Players.Add(new CheaterPlayer {Name = name});
                        break;
                    case 4:
                        Players.Add(new ThoroughPlayer {Name = name});
                        break;
                    case 5:
                        Players.Add(new ThoroughCheaterPlayer {Name = name});
                        break;
                }
            }
            else
            {
                Console.WriteLine("Input digit should be between {0} and {1}", 1,
                    Enum.GetValues(typeof(PlayerType)).Length);
                GetPlayerType(name);
            }
        }

        private static int GetNumer()
        {
            int numValue;

            Console.Write("Waiting for input: ");

            if (int.TryParse(Console.ReadLine(), out numValue))
            {
                return numValue;
            }

            Console.WriteLine("Input must be number");

            return GetNumer();
        }
    }
}