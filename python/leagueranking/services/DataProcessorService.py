from leagueranking.models.resultmodel import match_result
from leagueranking.models.resultmodel import team_result
from leagueranking.helpers.resultprocessorhelper import ResultProcessorHelper
from leagueranking.helpers.logginghelper import LoggingHelper

# Service to process the data from the command line
class DataProcessorService:
    _match_results: match_result
    _results = []

    def __init__(self, results: str):
        self._match_results = []
        self._results = results

    # Main function to process the results
    def process_result(self):
        for result in self._results: # iterate through all the command line scores and add them to the match_results object
            try:
                self.add_match_result(result.strip())
            except Exception as Argument:
                LoggingHelper.log_console("An error occured trying to add the match result")
                LoggingHelper.log_file(Argument)

        return self._match_results

    # Function to add all the match results
    def add_match_result(self, result: str):
        # Split into team name and score
        scores = result.split(',')

        # Add team 1 results
        team1 = team_result()
        team1.team_name = ResultProcessorHelper.get_team_name(scores[0]) #call to helpers.resultprocessorhelper.py
        team1.score = ResultProcessorHelper.get_score(scores[0]) #call to helpers.resultprocessorhelper.py

        # Add team 2 results
        team2 = team_result()
        team2.team_name = ResultProcessorHelper.get_team_name((scores[1])) #call to helpers.resultprocessorhelper.py
        team2.score = ResultProcessorHelper.get_score(scores[1]) #call to helpers.resultprocessorhelper.py

        result = match_result()
        result.team1 = team1
        result.team2 = team2

        # Add to the match_results object to be used to process the rankings
        self._match_results.append(result)

