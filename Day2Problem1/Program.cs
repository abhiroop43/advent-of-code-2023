using System.Text.RegularExpressions;

const int totalBlueBalls = 14;
const int totalRedBalls = 12;
const int totalGreenBalls = 13;

var lines = new List<string?>();
using var sr = new StreamReader("input.txt");
while (sr.ReadLine() is { } line) lines.Add(line);

var gamePossibilities = new Dictionary<int, bool>();
var gamePowers = new Dictionary<int, int>();

foreach (var input in lines)
{
    const string bluePattern = @"(\d+)\s*blue";
    const string redPattern = @"(\d+)\s*red";
    const string greenPattern = @"(\d+)\s*green";
    
    const string gamePattern = @"Game (\d+):";
    var gamePossible = true;

    if (input == null) continue;
    var blueMatches = Regex.Matches(input, bluePattern);
    var redMatches = Regex.Matches(input, redPattern);
    var greenMatches = Regex.Matches(input, greenPattern);
    
    var gameNumberMatch = Regex.Match(input, gamePattern);
    var gameNumber = Convert.ToInt32(gameNumberMatch.Groups[1].Value);

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
    
    if(maxBlueBalls > totalBlueBalls 
       || maxRedBalls > totalRedBalls 
       || maxGreenBalls > totalGreenBalls)
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

var totalGamePower = gamePowers
    .Where(game => gamePowers[game.Key] != 0)
    .Sum(game => game.Value);


Console.WriteLine(totalPossibleGames);
Console.WriteLine(totalGamePower);