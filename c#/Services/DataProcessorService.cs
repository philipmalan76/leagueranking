using LeagueRanking.Helpers;
using LeagueRanking.Interfaces;
using LeagueRanking.Models;

namespace LeagueRanking.Services
{
    /// <summary>
    /// This class processes the data from either the command line or file input. It implements IDataProcessor interface
    /// </summary>
    internal class DataProcessorService : IDataProcessor
    {
        
        List<Models.ResultModel.MatchResult> matchResults = new();

        /// <summary>
        /// Method to process a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public List<ResultModel.MatchResult> ProcessFile(string file)
        {
            string[] lines = File.ReadAllLines(file);

            string joined = string.Join(Environment.NewLine, lines);

            return ProcessResult(joined);
        }

        /// <summary>
        /// Method to process scores on the command line
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public List<ResultModel.MatchResult> ProcessResult(string result)
        {
            //Check if we have more than 1 result
            if (result.Contains(Environment.NewLine))
            {
                string[] resultPairs = result.Split(Environment.NewLine);

                for (int i = 0; i < resultPairs.Length; i++)
                    AddMatchResult(resultPairs[i]);
            }
            else AddMatchResult(result); //Only one result

            return matchResults;
        }

        /// <summary>
        /// Private method to add the match result to the match results list to be used later
        /// </summary>
        /// <param name="result"></param>
        private void AddMatchResult(string result)
        {
            //As we have 2 teams we split by comma
            string[] splitString = result.Split(',');

            //Add info for each team
            Models.ResultModel.TeamResult Team1 = new()
            {
                TeamName = ResultProcessorHelper.GetTeamName(splitString[0]),
                Score = ResultProcessorHelper.GetScore(splitString[0])
            };

            Models.ResultModel.TeamResult Team2 = new()
            {
                TeamName = ResultProcessorHelper.GetTeamName(splitString[1]),
                Score = ResultProcessorHelper.GetScore(splitString[1])
            };


            Team2.TeamName = ResultProcessorHelper.GetTeamName(splitString[1]);
            Team2.Score = ResultProcessorHelper.GetScore(splitString[1]);

            //Add to the matchresult list to be used later
            Models.ResultModel.MatchResult matchResult = new()
            {
                Team1 = Team1,
                Team2 = Team2
            };

            matchResults.Add(matchResult);
        }

    }
}
