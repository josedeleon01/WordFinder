# WordFinder
WordFinder is a C# class that searches for words in a character matrix. It provides functionality to find the top 10 most repeated words from a word stream in the matrix, supporting horizontal and vertical searches.

## Table of Contents
- [Installation](#installation)
- [Usage](#usage)
- [Testing](#testing)

## Installation
Clone the repository or download the source code.
git clone https://github.com/josedeleon01/WordFinder.git
## Usage
```// Create a WordFinder instance by providing a matrix
var matrix = new List<string>
{
    "chill",
    "owind",
    "lchar",
    "coldw"
};

var wordFinder = new WordFinder(matrix);

var wordStream = new List<string>
{
    "chill",
    "cold",
    "wind",
    "snow",
    "rain",
    "chill"
};

var result = wordFinder.Find(wordStream);
```

## Testing
The project includes unit tests to verify the functionality of the WordFinder class. You can run the tests using MSTest.
