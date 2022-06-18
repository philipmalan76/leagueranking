namespace LeagueRanking.Helpers
{
    /// <summary>
    /// Static helper class
    /// </summary>
    internal static class ResultProcessorHelper
    {
        //Get the score from the input string - Last value after the space
        public static int GetScore(string str)
        {
            bool success = Int32.TryParse(str.Split(' ').Last(), out int value);

            if (success)
                return value;

            return 0;
        }

        //Get the team name from the input string
        public static string GetTeamName(string str)
        {
            return str[..str.LastIndexOf(' ')].Trim();
        }

        
    }
}
