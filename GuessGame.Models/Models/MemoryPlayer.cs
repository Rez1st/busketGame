using System.Collections.Generic;
using System.Linq;

namespace GuessGame.Models.Models
{
    public class MemoryPlayer : Player
    {
        protected override IEnumerable<int> GetRange()
        {
            //exlude only his own list of guesses
            return Enumerable.Range(Rules.MinBasketWeight, Rules.MaxBasketWaight - Rules.MinBasketWeight).Where(i => !GuessesMade.Contains(i));
        }
    }
}