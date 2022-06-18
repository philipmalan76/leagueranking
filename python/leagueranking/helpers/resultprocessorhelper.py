#Static helper functions to get the team name and score from the input string
class ResultProcessorHelper:

    @staticmethod
    def get_score(input: str):
        if input.split(' ')[-1].isnumeric():
            return input.split(' ')[-1]
        else:
            return None

    @staticmethod
    def get_team_name(input: str):
        return input.rsplit(' ', 1)[0].strip()

