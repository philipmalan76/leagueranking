import leagueranking.helpers.logginghelper
from leagueranking.models.resultmodel import team_result
from leagueranking.models.resultmodel import match_result
from leagueranking.rules import Rules
from leagueranking.helpers.logginghelper import LoggingHelper

# Service to calculate the rankings and add format the final ranking stdout
class CalculateRankingService:
    _team_results: team_result
    _match_results: match_result

    def __init__(self, match_results):
        self._team_results = [] # create an instance of the team_result object list
        self._match_results = match_results # assign the match_results

    # Function to calculate the ranking
    def calculate_ranking(self):
        for q in self._match_results: # Iterate through all the match results
            #Add the Teams - This is done to ensure we have all the teams in the team_results list
            try:
                self.add_teams(q)
            except Exception as Argument:
                LoggingHelper.log_console("An error occured trying to add a team to the ranking table")
                LoggingHelper.log_file(Argument)

            #Add the scores
            try:
                if q.team1.score > q.team2.score:
                    self.add_score(q.team1.team_name, Rules.win.value) # Team 1 Won
                elif q.team2.score > q.team1.score:
                    self.add_score(q.team2.team_name, Rules.win.value)  # Team 2 Won
                elif q.team1.score == q.team2.score:
                    self.add_score(q.team1.team_name, Rules.draw.value) # Each get a point
                    self.add_score(q.team2.team_name, Rules.draw.value) # Each get a point
            except Exception as Argument:
                LoggingHelper.log_console("An error occured trying to add a score to a team")
                LoggingHelper.log_file(Argument)

        # Call the function to format the final rankings
        try:
            return self.format_final_rankings()
        except Exception as Argument:
            LoggingHelper.log_console("An error occured trying to format the final rankings")
            LoggingHelper.log_file(Argument)
            return

    # Add the teams to the match_results list so that we can rank them
    def add_teams(self, result: match_result):
        #for team 1
        found = next((t for t in self._team_results if t.team_name == result.team1.team_name), None) # See if we already have the team in the list
        if not found: # If not found then add to the list
            q = team_result()
            q.team_name = result.team1.team_name
            q.score = 0
            self._team_results.append(q)

        #for team 2
        found = next((t for t in self._team_results if t.team_name == result.team2.team_name), None) # See if we already have the team in the list
        if not found: # If not found then add to the list
            q = team_result()
            q.team_name = result.team2.team_name
            q.score = 0
            self._team_results.append(q)

    # Function to add the scores to each team in the final list
    def add_score(self, team_name: str, score: int):
        for x in self._team_results:
            if x.team_name == team_name:
                index = self._team_results.index(x)
                old_score = self._team_results[index].score # Get the old score from the list
                self._team_results[index].score = int(old_score) + int(score) # Add the old an new score
                return

    # Function to format the final rankings stdout
    def format_final_rankings(self):
        final_rankings = ""
        points_text = ""
        prev_score = 0
        counter = 0

        # Sort the list in place (first by score descending and the by team name)
        self._team_results.sort(key=lambda x: (-x.score, x.team_name))

        # Iterate through the team results
        for i in range(len(self._team_results)):
            points_text = 'pt' if self._team_results[i].score == 1 else "pts" # Make sure the text for points are correct

            if i > 0:
                counter = i if self._team_results[i].score == prev_score else i + 1 # If we have the same score for 2 teams then keep the ranking number the same
            else: counter = i + 1

            # Create the final rankings string and append to the current one
            final_rankings += ''.join([str(counter), '. ', self._team_results[i].team_name, ', ', str(self._team_results[i].score), ' ', points_text, '\n'])

            # Keep track of the previous score
            prev_score = self._team_results[i].score

        return final_rankings





