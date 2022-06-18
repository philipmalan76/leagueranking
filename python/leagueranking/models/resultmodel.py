# Model for the objects used
class team_result(object):
    team_name = str()
    score = int()

class match_result(object):
    team1 = team_result()
    team2 = team_result()

