using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace r6thCompChallenge
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            SectionA a = new SectionA();

            //TestA1(a);

            TestA2(a);
        }

        private static void TestA1(SectionA a)
        {
            bool[] expectedResults = new bool[] { true, true, true, true, false, true, true, false, true, true };
            string testFile = File.ReadAllText("Input Data/input_a1.txt").Replace("\r\n", "\n");
            string[] testCases = testFile.Split(new string[] { "\n\n" }, StringSplitOptions.None);
            Console.WriteLine(string.Join("\n", testCases.Zip(expectedResults, (test, result) =>
            {
                char[][] grid = test.Split('\n').Select(l => l.ToCharArray()).ToArray();
                return $"Test data:\n{test}\nExpected Answer: {result}\nYour Answer: {a.Chain(grid)}\n";
            })));
        }

        private static void TestA2(SectionA a)
        {
            HashSet<string> possible = a.GetAllPossibleBoards();

            string testFile = File.ReadAllText("Input Data/input_a2.txt").Replace("\r\n", "\n");
            string[] testCases = testFile.Split(new string[] { "\n\n" }, StringSplitOptions.None);
            foreach(string test in testCases)
            {
                char[][] grid = test.Split('\n').Select(l => l.ToCharArray()).ToArray();
                bool result = possible.Contains(string.Concat(grid.Select(r => new string(r))));
                Console.WriteLine($"Test data:\n{test}\nExpected Answer: {result}\nYour Answer: {a.TicTacToe(grid)}\n");
            }
        }
    }
}
