using System.Collections.Generic;
using System.Linq;

namespace GuessGame.Models.Models
{
    public class ThoroughCheaterPlayer : Player
    {
        protected override IEnumerable<int> GetRange()
        {
            return Enumerable.Range(Rules.MinBasketWeight, Rules.MaxBasketWaight - Rules.MinBasketWeight)
                        .Where(i => !GuessPool.Contains(i));
        }

        public override int MakeAGuess()
        {
            var range = GetRange();

            return range.First();
        }
    }
}