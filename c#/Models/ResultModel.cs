
namespace LeagueRanking.Models
{
    /// <summary>
    /// Models for data storage
    /// </summary>
    internal class ResultModel
    {
        public class TeamResult
        {
            public string? TeamName { get; set; }
            public int Score { get; set; } = 0;
        }

        public class MatchResult
        {
            public TeamResult? Team1 { get; set; }
            public TeamResult? Team2 { get; set; }
        }

    }
}
