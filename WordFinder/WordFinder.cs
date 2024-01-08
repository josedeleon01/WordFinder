using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder
{
    public class WordFinder
    {
        private readonly char[,] matrix;

        // Constructor
        public WordFinder(IEnumerable<string> matrix)
        {
            // Validate input parameter
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));

            // Convert matrix IEnumerable to an array
            var matrixArray = matrix.ToArray();

            // Validate matrix structure
            if (matrixArray.Length == 0 || matrixArray.Any(row => row.Length != matrixArray[0].Length))
                throw new ArgumentException("Invalid matrix");

            // Create a character matrix from the input IEnumerable
            this.matrix = new char[matrixArray.Length, matrixArray[0].Length];

            // Populate the character matrix with the values from the input IEnumerable
            for (int i = 0; i < matrixArray.Length; i++)
            {
                for (int j = 0; j < matrixArray[0].Length; j++)
                {
                    this.matrix[i, j] = matrixArray[i][j];
                }
            }
        }

        // Find method to search for words in the matrix
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            // Validate input parameter
            if (wordstream == null)
                throw new ArgumentNullException(nameof(wordstream));

            // Dictionary to store word frequencies along with their locations
            var wordFrequency = new Dictionary<string, int>();

            // Iterate through each word in the wordstream
            foreach (var word in wordstream)
            {
                // Find locations where the word occurs in the matrix
                var foundLocations = SearchWord(word);

                // If word is found, update its frequency in the wordFrequency dictionary
                if (foundLocations.Any())
                {
                    var uniqueLocations = foundLocations.Distinct();

                    foreach (var location in uniqueLocations)
                    {
                        var key = $"{word}@{location.Item1},{location.Item2}";
                        if (!wordFrequency.ContainsKey(key))
                        {
                            wordFrequency[key] = 1;
                        }
                    }
                }
            }

            // Select the top 10 most repeated words from the wordFrequency dictionary
            var topWords = wordFrequency.OrderByDescending(kv => kv.Value)
                                        .Select(kv => kv.Key.Split('@')[0])
                                        .Take(10);

            return topWords;
        }

        // Method to search for a word in the matrix and return its locations
        private IEnumerable<(int, int)> SearchWord(string word)
        {
            var foundLocations = new List<(int, int)>();

            // Iterate through each position in the matrix
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    // Check if the word is found horizontally or vertically at the current position
                    if (SearchHorizontal(i, j, word) || SearchVertical(i, j, word))
                    {
                        foundLocations.Add((i, j));
                    }
                }
            }

            return foundLocations;
        }

        // Helper method to check if a word is found horizontally at a given position
        private bool SearchHorizontal(int row, int col, string word)
        {
            // Check if the word exceeds the matrix boundaries horizontally
            if (col + word.Length > matrix.GetLength(1))
            {
                return false;
            }

            // Check if the characters match horizontally
            for (int j = 0; j < word.Length; j++)
            {
                if (matrix[row, col + j] != word[j])
                {
                    return false;
                }
            }

            return true;
        }

        // Helper method to check if a word is found vertically at a given position
        private bool SearchVertical(int row, int col, string word)
        {
            // Check if the word exceeds the matrix boundaries vertically
            if (row + word.Length > matrix.GetLength(0))
            {
                return false;
            }

            // Check if the characters match vertically
            for (int i = 0; i < word.Length; i++)
            {
                if (matrix[row + i, col] != word[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
