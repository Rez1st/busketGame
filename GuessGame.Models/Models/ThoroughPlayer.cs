using System.Collections.Generic;
using System.Linq;

namespace GuessGame.Models.Models
{
    public class ThoroughPlayer : Player
    {
        private int _incrementor = Rules.MinBasketWeight;

        protected override IEnumerable<int> GetRange()
        {
            _incrementor++;

            return Enumerable.Range(Rules.MinBasketWeight, Rules.MaxBasketWaight - Rules.MinBasketWeight).Where(i => i == _incrementor);
        }

        public override int MakeAGuess()
        {
            TryIncrementAttenpt();

            return _incrementor += 1;
        }
    }
}