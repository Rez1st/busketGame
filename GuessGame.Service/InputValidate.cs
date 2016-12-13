using GuessGame.Models;

namespace GuessGame.Service
{
    public static class InputValidate
    {
        public static bool SetPlayersCount(int playersCount)
        {
            if (playersCount <= Rules.MaxPlayers
                && playersCount >= Rules.MinPlayers)
            {
                return true;
            }

            return false;
        }
        public static bool SetBaseketWeight(int basketWeight)
        {
            if (basketWeight > Rules.MinBasketWeight
                && basketWeight < Rules.MaxBasketWaight)
            {
                return true;
            }

            return false;
        }
    }
}
