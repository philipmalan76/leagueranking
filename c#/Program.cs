using LeagueRanking.Services;

ConsoleKey selectedOption;
bool optionSelected;
bool calculateRanking = false;
bool consoleInput = false;
List<LeagueRanking.Models.ResultModel.MatchResult> matchResults = new();


DataProcessorService dataProcessorService = new();
CalculateRankingService calculateRankingService = new();

string? line;

Console.WriteLine("Welcome to the League Ranking Calculator. Please select an option for inputting the scores.");
Console.WriteLine("1. Enter the scores manually using the console");
Console.WriteLine("2. Specify a file path to import it from");

do
{
    selectedOption = Console.ReadKey(false).Key;

    switch (selectedOption)
    {
        case ConsoleKey.D1:
            optionSelected = true;
            consoleInput = true;
            break;
        case ConsoleKey.D2:
            optionSelected= true;
            break;
        default:
            Console.WriteLine("Invalid Option");
            optionSelected = false;
            break;
    }

} while (!optionSelected);

//Once we have made a choice then we start with the processing
do
{
    Console.Clear();
    
    if (consoleInput)
    {
        Console.WriteLine("Please enter the result. You can either enter one result or multiple results separated by a new line. Press q and enter to quit and process the rankings");
        
        //Read all the lines
        while (!String.IsNullOrWhiteSpace(line = Console.ReadLine()) && line.ToLower() != "q")
        {
            matchResults = dataProcessorService.ProcessResult(line);
            Console.WriteLine(String.Concat("Result ", line, " added successfully"));
        }

        calculateRanking = true;
    }
    else //File input
    {
        Console.WriteLine("Please enter the full file path");

        line = Console.ReadLine();

        matchResults = dataProcessorService.ProcessFile(line);

        calculateRanking = true;
    }
} while (!calculateRanking);

Console.Clear();

Console.WriteLine("The final ranking is as follows:");

//Now we calcuate the rankings
Console.WriteLine(calculateRankingService.CalculateRanking(matchResults));


//TODO - LOGGING

