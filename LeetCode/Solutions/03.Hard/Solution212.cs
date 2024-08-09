using BenchmarkDotNet.Attributes;
using LeetCode.Helper;

namespace LeetCode;

// | Method        | Mean     | Error     | StdDev    |
// |-------------- |---------:|----------:|----------:|
// | FindWordsSlow | 1.728 us | 0.0320 us | 0.0328 us |
public class Solution212
{
    [Benchmark]
    public IList<string> FindWordsSlow()
    {
        return FindWordsSlow(
            [['o', 'a', 'a', 'n'], ['e', 't', 'a', 'e'], ['i', 'h', 'k', 'r'], ['i', 'f', 'l', 'v']],
             ["oath", "pea", "eat", "rain"]);
    }

    public static IList<string> FindWordsSlow(char[][] board, string[] words)
    {
        var result = new List<string>();

        // ConsoleHelper.Print(board);

        var boardDimensionX = board.Length;
        var boardDimensionY = board[0].Length;

        List<KeyValuePair<int, int>> GetPossibleStartLocations(char wordChar)
        {
            var possibleStartLocations = new List<KeyValuePair<int, int>>();

            for (var row = 0; row < boardDimensionX; row++)
            {
                for (var col = 0; col < boardDimensionY; col++)
                {
                    if (board[row][col] == wordChar)
                    {
                        possibleStartLocations.Add(new KeyValuePair<int, int>(row, col));
                    }
                }
            }

            return possibleStartLocations;
        }

        KeyValuePair<int, int> GetNextCell(char wordChar, int currentX, int currentY, List<KeyValuePair<int, int>> excludedCells)
        {
            var possibleStartLocations = new List<KeyValuePair<int, int>>
            {
                new(Math.Abs(currentX - 1), Math.Abs(currentY)),
                new(Math.Abs(Math.Min(currentX + 1, boardDimensionX - 1)), Math.Abs(currentY)),
                new(Math.Abs(currentX), Math.Abs(currentY - 1)),
                new(Math.Abs(currentX), Math.Abs(Math.Min(currentY + 1, boardDimensionX - 1)))
            };

            possibleStartLocations = possibleStartLocations.DistinctBy(kvp => new { kvp.Key, kvp.Value }).ToList();

            foreach (var possibleStartLocation in possibleStartLocations)
            {
                if (excludedCells.Any(excludedCell => excludedCell.Key == boardDimensionX && excludedCell.Value == boardDimensionY))
                {
                    continue;
                }

                if (board[possibleStartLocation.Key][possibleStartLocation.Value] == wordChar)
                {
                    return possibleStartLocation;
                }
            }

            return default;
        }

        for (var wordsIndex = 0; wordsIndex < words.Length; wordsIndex++)
        {
            var word = words[wordsIndex];
            // Console.WriteLine($"Processing word: {word}");
            var wordFound = false;

            //Get all possible start locations
            var possibleStartLocations = GetPossibleStartLocations(word[0]);
            // Console.WriteLine($"Possible start locations for: {word[0]} is {string.Join(',', possibleStartLocations)}");

            foreach (var possibleStartLocation in possibleStartLocations)
            {
                if (wordFound)
                {
                    break;
                }

                var wordExcludedCells = new List<KeyValuePair<int, int>>();

                KeyValuePair<int, int> currentCell = possibleStartLocation;
                wordExcludedCells.Add(currentCell);
                for (var wordCharIndex = 1; wordCharIndex < word.Length; wordCharIndex++)
                {
                    // Find all char occurances in board, then search for closest cells for the next char
                    var wordChar = word[wordCharIndex];
                    currentCell = GetNextCell(wordChar, currentCell.Key, currentCell.Value, wordExcludedCells);

                    if (currentCell.Equals(default(KeyValuePair<int, int>)))
                    {
                        wordExcludedCells = [];
                        break;
                    }

                    wordExcludedCells.Add(currentCell);
                    // Console.WriteLine($"Cell location for: {wordChar} is [{currentCell.Key}][{currentCell.Value}]");

                    if (wordCharIndex == word.Length - 1)
                    {
                        wordFound = true;
                        result.Add(word);
                        // Console.WriteLine($"Word was found: {word}");
                    }
                }
            }
        }

        // Console.WriteLine($"Result: {string.Join(',', result)}");

        return result;
    }
}