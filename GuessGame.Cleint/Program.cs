using System;
using GuessGame.Models;
using GuessGame.Service;

namespace GuessGame.Cleint
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StartGame.GetDataToSetupTheGame();

            var game = new Game();

            game.Setup(Rules.RealWeight, StartGame.Players);

            game.Start();

            game.Result().ForEach(Console.WriteLine);

            Console.ReadKey();
        }
    }
}