using LeagueRanking.Models;

namespace LeagueRanking.Interfaces
{
    interface ICalculateRanking
    {
        public string CalculateRanking(List<ResultModel.MatchResult> MatchResult);
    }
}
