using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GuessGame.Models;
using GuessGame.Models.Models;

namespace GuessGame.Service
{
    public class Game
    {
        private static readonly CancellationTokenSource Source = new CancellationTokenSource();

        private readonly IDictionary<string, int> _closestGuesses = new Dictionary<string, int>();

        private readonly List<string> _gameResult = new List<string>();

        private readonly CancellationToken _token = Source.Token;

        private static int BasketWeight { get; set; }

        private ICollection<Player> Players { get; set; }

        private static bool GameEnded { get; set; }

        private void Process()
        {
            Parallel.ForEach(Players, player => UnleashThePlayer(player, _token).Wait());
        }

        private async Task UnleashThePlayer(Player player, CancellationToken token)
        {
            while (player.AttemptsMade < Rules.MaxAttemptsCount)
            {
                if (token.IsCancellationRequested)
                {
                    return;
                }

                player.DoStep();

                if (player.LastGuess == BasketWeight)
                {
                    GameEnded = true;
                    player.IsAWinnrer = true;

                    Source.Cancel();
                }
                else
                {
                    Thread.Sleep(Math.Abs(BasketWeight - player.LastGuess));
                }
            }
        }

        private void GetTheWinner()
        {
            if (GameEnded)
            {
                foreach (var player in Players)
                {
                    if (player.IsAWinnrer)
                    {
                        _gameResult.Add(string.Format("Player {0}, made {1} attempts to win the game", player.Name,
                            player.AttemptsMade));
                    }
                }
            }
            else
            {
                _gameResult.Add("No winner in this game");

                foreach (var player in Players)
                {
                    _closestGuesses.Add(player.Name,
                        player.GuessesMade.Aggregate(
                            (x, y) => Math.Abs(x - Rules.RealWeight) < Math.Abs(y - Rules.RealWeight) ? x : y));
                }

                var winners = _closestGuesses.Where(x => x.Value == _closestGuesses.Values.Max()).ToList();

                foreach (var winner in winners)
                {
                    _gameResult.Add(string.Format("{0} Was so close to win the game - > his most precise guess was {1}",
                        winner.Key, winner.Value));
                }
            }
        }

        /// <summary>
        ///     Setting up game
        /// </summary>
        /// <param name="basketWeight">Weight of basket</param>
        /// <param name="players">Players list</param>
        public void Setup(int basketWeight, ICollection<Player> players)
        {
            BasketWeight = basketWeight;

            Players = players;
        }

        public void Start()
        {
            Process();

            GetTheWinner();
        }

        public List<string> Result()
        {
            return _gameResult;
        }

    }
}