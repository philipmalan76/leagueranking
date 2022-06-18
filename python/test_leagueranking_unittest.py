import unittest
from leagueranking.services.DataProcessorService import DataProcessorService
from leagueranking.services.CalculateRankingService import CalculateRankingService
from leagueranking.models.resultmodel import match_result

class TestLeagueRanking(unittest.TestCase):
    # Test the data processor service
    def test_data_processor_service(self):
        input = ['Lions 3, Snakes 3']

        data_processor_service = DataProcessorService(input)

        team_results = data_processor_service.process_result()

        # Create the output to test
        output = match_result()
        output.team1.team_name = 'Lions'
        output.team2.team_name = 'Snakes'
        output.team1.score = '3'
        output.team2.score = '3'

        self.assertEqual(team_results[0].team1.team_name, output.team1.team_name)
        self.assertEqual(team_results[0].team2.team_name, output.team2.team_name)
        self.assertEqual(team_results[0].team1.score, output.team1.score)
        self.assertEqual(team_results[0].team2.score, output.team2.score)

    # Test the final output
    def test_final_ranking(self):
        input = ['Lions 3, Snakes 3', 'Tarantulas 1, FC Awesome 0', 'Lions 1, FC Awesome 1', 'Tarantulas 3, Snakes 1', 'Lions 4, Grouches 0']

        data_processor_service = DataProcessorService(input)

        team_results = data_processor_service.process_result()

        calculate_ranking_service = CalculateRankingService(team_results)

        final_ranking = calculate_ranking_service.calculate_ranking()

        output = '1. Tarantulas, 6 pts\n2. Lions, 5 pts\n3. FC Awesome, 1 pt\n3. Snakes, 1 pt\n5. Grouches, 0 pts\n'

        self.assertEqual(final_ranking, output)

if __name__ == '__main__':
    unittest.main()