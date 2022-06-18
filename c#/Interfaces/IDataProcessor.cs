namespace LeagueRanking.Interfaces
{
    /// <summary>
    /// Basic interface used by the DataProcessorService class
    /// </summary>
    internal interface IDataProcessor
    {
        public List<Models.ResultModel.MatchResult> ProcessResult(string result);
        public List<Models.ResultModel.MatchResult> ProcessFile(string file);
    }
}
