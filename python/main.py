from leagueranking.services.DataProcessorService import DataProcessorService
from leagueranking.services.CalculateRankingService import CalculateRankingService
import sys

results = []

print("Please enter the scores. Press q to quit and calculate the rankings")

for line in sys.stdin:
    if 'q' == line.rstrip():
        break
    results.append(line)

# Create an instance of the DataProcessorService and add the results from stdin to it
data_processor_service = DataProcessorService(results)

# Call the DataProcessorService to process the results from stdin
team_results = data_processor_service.process_result()

# Create an instance of the CalculateRankingService and add the team_results to it
calculate_ranking_service = CalculateRankingService(team_results)

# Call the CalculateRankingService to calculate the final rankings
final_ranking = calculate_ranking_service.calculate_ranking()

print("The final ranking is as follows:")

# Output the final rankings to the console
print(final_ranking)



