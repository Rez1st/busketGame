using System;
using System.Collections.Generic;
using System.Linq;

namespace GuessGame.Models.Models
{
    public abstract class Player
    {
        //static as it will be one random instance for all players
        //to avoid duplicating random values
        protected static readonly Random Getrandom = new Random();
        //sync block will be the same for all also
        protected static readonly object SyncLock = new object();

        protected static ICollection<int> GuessPool = new List<int>();

        public ICollection<int> GuessesMade = new List<int>();

        public bool IsAWinnrer { get; set; }

        protected Player()
        {
            AttemptsMade = 0;
        }

        public string Name { get; set; }

        public int AttemptsMade { get; set; }

        public int LastGuess { get; set; }

        public virtual int MakeAGuess()
        {
            lock (SyncLock)
            {
                TryIncrementAttenpt();
                //memory player difference from cheater palyer - he exludes only his guesses
                var range = GetRange();

                return range.ElementAt(Getrandom.Next(0, range.Count()));
            }
        }

        protected bool TryIncrementAttenpt()
        {
            if (AttemptsMade > Rules.MaxAttemptsCount)
            {
                return false;
            }

            AttemptsMade++;

            return true;
        }

        public void DoStep()
        {
            LastGuess = MakeAGuess();

            PostRoll(LastGuess);
        }

        private void PostRoll(int roll)
        {
            GuessesMade.Add(roll);
            GuessPool.Add(roll);
        }

        protected abstract IEnumerable<int> GetRange();
    }
}