using LeagueRanking.Interfaces;

namespace LeagueRanking.Services
{
    /// <summary>
    /// Class used to calculate the rankings and format the final ranking
    /// </summary>
    internal class CalculateRankingService : ICalculateRanking
    {
        public List<Models.ResultModel.TeamResult> teamResults = new();
        
        /// <summary>
        /// Main method used to calculate the rankings
        /// </summary>
        /// <param name="matchResults"></param>
        /// <returns></returns>
        public string CalculateRanking(List<Models.ResultModel.MatchResult> matchResults)
        {
            //Iterate through all the scores and calculate
            foreach (var matchResult in matchResults)
            {
                AddTeams(matchResult);

                //Now we can add the score to the relevant team
                if (matchResult.Team1.Score > matchResult.Team2.Score) //Team 1 Won
                    AddScore(matchResult.Team1.TeamName, (int)Rule.Win);
                else if (matchResult.Team2.Score > matchResult.Team1.Score) //Team 2 won
                    AddScore(matchResult.Team2.TeamName, (int)Rule.Win);      
                else if (matchResult.Team1.Score == matchResult.Team2.Score) //Draw
                {
                    AddScore(matchResult.Team1.TeamName, (int)Rule.Draw);
                    AddScore(matchResult.Team2.TeamName, (int)Rule.Draw);
                }
                //No need to worry about lose as there are no points
            }

            return FormatFinalRankings();
        }

        /// <summary>
        /// Private method to format the stdout for the final rankings
        /// </summary>
        /// <returns></returns>
        private string FormatFinalRankings()
        {
            string finalRankings = "";
            string pointsText = "";
            int prevScore = 0;
            int counter = 0;

            //Order by score and then by team name
            var sorted = teamResults.OrderByDescending(x => x.Score).ThenBy(x => x.TeamName).ToList();

            for (int i = 0; i < sorted.Count; i++)
            {
                pointsText = sorted[i].Score == 1 ? "pt" : "pts"; //Make sure we have the right points text

                if (i > 0)
                    counter = sorted[i].Score == prevScore ? i : i + 1;
                else counter = i + 1;

                //Create the final ranking text
                finalRankings += string.Concat(counter, ". ", sorted[i].TeamName, ", ", sorted[i].Score.ToString(), " ", pointsText, Environment.NewLine);

                //Set the previous score so that we can do the correct counter
                prevScore = sorted[i].Score;
            }

            return finalRankings;
        }

        /// <summary>
        /// Method to add all the teams to the teamResult list used to do the final ranking
        /// </summary>
        /// <param name="matchResult"></param>
        private void AddTeams(Models.ResultModel.MatchResult matchResult)
        {
            //First add all the teams to the final list - This is done to ensure that we have all the teams in the list
            if (!teamResults.Any(x => x.TeamName == matchResult.Team1.TeamName))
            {
                teamResults.Add(new Models.ResultModel.TeamResult
                {
                    TeamName = matchResult.Team1.TeamName,
                    Score = 0
                });
            }

            if (!teamResults.Any(x => x.TeamName == matchResult.Team2.TeamName))
            {
                teamResults.Add(new Models.ResultModel.TeamResult
                {
                    TeamName = matchResult.Team2.TeamName,
                    Score = 0
                });
            }
        }

        /// <summary>
        /// Add the score for each team
        /// </summary>
        /// <param name="TeamName"></param>
        /// <param name="Score"></param>
        private void AddScore(string TeamName, int Score)
        {
            var teamResult = teamResults.FirstOrDefault(x => x.TeamName == TeamName);

            if (teamResult != null) //Should always be the case
                teamResult.Score += Score;
        }
    }
}
