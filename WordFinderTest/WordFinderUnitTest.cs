using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;

namespace WordFinderTest
{

    [TestClass]
    public class WordFinderTests
    {
        [TestMethod]
        // Test whether the Find method correctly identifies and returns the top 10 most repeated words
        // from the word stream that are present in the provided matrix, including a duplicate word.
        public void Find_TopWordsInMatrix_ReturnsCorrectResults()
        {
            // Arrange
            var matrix = new List<string>
            {
                "chill",
                "owind",
                "lchar",
                "coldw",
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

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            var expectedResult = new List<string> { "chill", "cold", "wind" };
            CollectionAssert.AreEqual(expectedResult.ToList(), result.ToList());
        }

        [TestMethod]
        // Test whether the Find method returns an empty result when none of the words from the
        // word stream are present in the provided matrix.
        public void Find_NoWordsInMatrix_ReturnsEmptyResult()
        {
            // Arrange
            var matrix = new List<string>
            {
                "abcde",
                "fghij",
                "klmno",
                "pqrst"
            };

            var wordFinder = new WordFinder.WordFinder(matrix);

            var wordStream = new List<string>
            {
                "apple",
                "banana",
                "orange"
            };

            // Act
            var result = wordFinder.Find(wordStream);

            // Assert
            CollectionAssert.AreEqual(new List<string>(), result.ToList());
        }

        [TestMethod]
        // Performance test to measure the execution time of the Find method when processing
        // a large matrix and word stream. Prints the time taken to the console.
        [Timeout(5000)] // 5 seconds timeout for performance test
        public void Find_PerformanceTest()
        {
            // Arrange
            const int matrixSize = 64;
            const int wordStreamSize = 1000;

            var random = new Random();
            var matrix = GenerateRandomMatrix(matrixSize, random);
            var wordStream = GenerateRandomWordStream(wordStreamSize, random);

            var wordFinder = new WordFinder.WordFinder(matrix);

            // Act
            var stopwatch = Stopwatch.StartNew();
            var result = wordFinder.Find(wordStream);
            stopwatch.Stop();

            // Assert
            Console.WriteLine($"Performance test completed in {stopwatch.ElapsedMilliseconds} ms");
            // No specific assertion for performance; this is more of a timing check
        }

        private static List<string> GenerateRandomMatrix(int size, Random random)
        {
            return Enumerable.Range(0, size)
                .Select(_ => new string(Enumerable.Range(0, size)
                    .Select(__ => (char)('A' + random.Next(26)))
                    .ToArray()))
                .ToList();
        }

        private static List<string> GenerateRandomWordStream(int size, Random random)
        {
            return Enumerable.Range(0, size)
                .Select(_ => new string(Enumerable.Range(0, 5) // Word length set to 5 for simplicity
                    .Select(__ => (char)('A' + random.Next(26)))
                    .ToArray()))
                .ToList();
        }
    }
}