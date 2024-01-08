using System.Diagnostics;

var matrix = new List<string>
{
    "chill",
    "owind",
    "lchar",
    "coldw"
};

var wordFinder = new WordFinder.WordFinder(matrix);

var wordStream = new List<string>
{
    "chill",
    "cold",
    "wind",
    "snow",
    "rain",
    "chill" // Duplicate word

};

Console.WriteLine($"The top 10 most repeated words: { String.Join(",", wordFinder.Find(wordStream))}");
