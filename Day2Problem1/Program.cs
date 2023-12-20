// See https://aka.ms/new-console-template for more information

using System.Text.RegularExpressions;

var TOTAL_BLUE_BALLS = 14;
var TOTAL_RED_BALLS = 12;
var TOTAL_GREEN_BALLS = 13;

var lines = new List<string?>();
using var sr = new StreamReader("input.txt");
while (sr.ReadLine() is { } line) lines.Add(line);

Dictionary<int, bool> gamePossibilities = new Dictionary<int, bool>();
Dictionary<int, int> gamePowers = new Dictionary<int, int>();

foreach (var input in lines)
{
    string bluePattern = @"(\d+)\s*blue";
    string redPattern = @"(\d+)\s*red";
    string greenPattern = @"(\d+)\s*green";
    
    string gamePattern = @"Game (\d+):";
    bool gamePossible = true;

    if (input == null) continue;
    MatchCollection blueMatches = Regex.Matches(input, bluePattern);
    MatchCollection redMatches = Regex.Matches(input, redPattern);
    MatchCollection greenMatches = Regex.Matches(input, greenPattern);
    
    Match gameNumberMatch = Regex.Match(input, gamePattern);
    int gameNumber = Convert.ToInt32(gameNumberMatch.Groups[1].Value);

    var blueBalls = new List<int>();
    foreach (Match match in blueMatches)
    {
        if (!int.TryParse(match.Groups[1].Value, out int count)) continue;
        blueBalls.Add(count);
    }
    
    var redBalls = new List<int>();
    foreach (Match match in redMatches)
    {
        if (!int.TryParse(match.Groups[1].Value, out int count)) continue;
        redBalls.Add(count);
    }

    var greenBalls = new List<int>();
    foreach (Match match in greenMatches)
    {
        if (!int.TryParse(match.Groups[1].Value, out int count)) continue;
        greenBalls.Add(count);
    }

    var maxBlueBalls = blueBalls.Max();
    var maxGreenBalls = greenBalls.Max();
    var maxRedBalls = redBalls.Max();
    
    if(maxBlueBalls > TOTAL_BLUE_BALLS 
       || maxGreenBalls > TOTAL_RED_BALLS 
       || maxRedBalls > TOTAL_GREEN_BALLS)
    {
        gamePossible = false;
    }
    // Console.WriteLine($"Key: {gameNumber}, Value: {gamePossible}");
    gamePossibilities.Add(gameNumber, gamePossible);
    
    var gamePower = maxBlueBalls * maxGreenBalls * maxRedBalls;
    // Console.WriteLine($"Key: {gameNumber}, Value: {gamePower}");
    gamePowers.Add(gameNumber, gamePower);
}


// add the game numbers where the game is possible
var totalPossibleGames = gamePossibilities
    .Where(game => game.Value)
    .Sum(game => game.Key);

int totalGamePower = 0;
foreach (var game in gamePowers)
{
    if (gamePowers[game.Key] != 0)
    {
        totalGamePower += game.Value;
    }
}



Console.WriteLine(totalPossibleGames);
Console.WriteLine(totalGamePower);
