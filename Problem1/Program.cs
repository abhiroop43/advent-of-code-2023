using System.Text.RegularExpressions;

var lines = new List<string?>();
using var sr = new StreamReader("input.txt");
while (sr.ReadLine() is { } line) lines.Add(line);

var numbers = new List<int>();
var index = 0;
foreach (var line in lines)
{
    if (line == null) continue;
    var matches = Regex.Matches(line, @"\d+");
    var digits = new List<char>();
    foreach (Match match in matches)
    {
        var num = int.Parse(match.Value);
        digits.AddRange(num.ToString().ToCharArray());
    }

    // if the number of digits in a line is less than 2, then duplicate the first digit
    if (digits.Count < 2) digits.Add(digits[0]);

    var numberInLine = $"{digits[0]}{digits[^1]}";
    // Console.WriteLine($"{index}: Number: {numberInLine}");
    try
    {
        numbers.Add(int.Parse(numberInLine));
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"{index}: Tried the number {numberInLine} but it failed with the following exception: {ex.Message}");
        throw;
    }

    index++;
}

var sum = numbers.Sum();

Console.WriteLine($"The output is: {sum}");