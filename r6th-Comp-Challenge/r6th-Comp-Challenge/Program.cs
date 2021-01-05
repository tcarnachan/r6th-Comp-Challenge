using System;
using System.IO;
using System.Linq;

namespace r6thCompChallenge
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            bool[] expectedResults = new bool[] { true, true, true, true, false, true, true, false, true, true };
            string testFile = File.ReadAllText("Input Data/input_a1.txt").Replace("\r\n", "\n");
            string[] testCases = testFile.Split(new string[] { "\n\n" }, StringSplitOptions.None);
            Console.WriteLine(string.Join("\n", testCases.Zip(expectedResults, (test, result) =>
            {
                char[][] grid = test.Split('\n').Select(l => l.ToCharArray()).ToArray();
                return $"Test data:\n{test}\nExpected Answer: {result}\nYour Answer: {Chain(grid)}\n";
            })));
        }

        private static bool Chain(char[][] grid)
        {
            for(int i = 0; i < 3; i++)
            {
                // Check row
                if (grid[i].Distinct().Count() == 1)
                        return true;
                // Check column
                if (grid[0][i] == grid[1][i] && grid[1][i] == grid[2][i])
                    return true;
            }
            // Check leading diagonal
            if (grid[0][0] == grid[1][1] && grid[1][1] == grid[2][2])
                return true;
            // Check other diagonal
            if (grid[0][2] == grid[1][1] && grid[1][1] == grid[2][0])
                return true;
            // No more possible 3-chains
            return false;
        }
    }
}
