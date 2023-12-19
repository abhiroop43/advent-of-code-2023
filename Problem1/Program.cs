using System.Text.RegularExpressions;

var lines = new List<string?>();
using var sr = new StreamReader("input.txt");
while (sr.ReadLine() is { } line) lines.Add(line);

var sum = (from line in lines 
    where line != null 
    let firstMatch = Regex.Match(line, @"\d|one|two|three|four|five|six|seven|eight|nine") 
    let lastMatch = Regex.Match(line, @"\d|one|two|three|four|five|six|seven|eight|nine", RegexOptions.RightToLeft) 
    select ParseNum(firstMatch.Value) * 10 + ParseNum(lastMatch.Value)).Sum();

Console.WriteLine(sum);
return;

int ParseNum(string st) => st switch {
    "one" => 1,
    "two" => 2,
    "three" => 3,
    "four" => 4,
    "five" => 5,
    "six" => 6,
    "seven" => 7,
    "eight" => 8,
    "nine" => 9,
    var d => int.Parse(d)
};

