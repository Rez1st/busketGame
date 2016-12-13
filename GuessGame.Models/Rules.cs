using System;
using static System.Configuration.ConfigurationSettings;

namespace GuessGame.Models
{
    public static class Rules
    {
        public static int MaxBasketWaight => Convert.ToInt32(AppSettings["MaxBasketWaight"]);

        public static int MinBasketWeight => Convert.ToInt32(AppSettings["MinBasketWeight"]);

        public static int MinPlayers => Convert.ToInt32(AppSettings["MinPlayers"]);

        public static int MaxPlayers => Convert.ToInt32(AppSettings["MaxPlayers"]);

        public static int MaxAttemptsCount => Convert.ToInt32(AppSettings["MaxAttemptsCount"]);

        public static int TimeLimit => Convert.ToInt32(AppSettings["MaxAttemptsCount"]);

        public static string GameWelcomeMessage => AppSettings["GameWelcomeMessage"];

        public static string GameDescription => AppSettings["GameDescription"];

        public static int RealWeight => Convert.ToInt32(AppSettings["RealWeight"]);
    }
}