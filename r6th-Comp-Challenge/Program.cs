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
            //TestA2(a);

            SectionB b = new SectionB();
            TestB1(b);
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

        private static void TestB1(SectionB b)
        {
            string testFile = File.ReadAllText("Input Data/input_b1.txt").Replace("\r\n", "\n");
            string[] testCases = testFile.Split(new string[] { "\n\n" }, StringSplitOptions.None);
            foreach (string test in testCases)
            {
                char[][] grid = test.Split('\n').Select(l => l.ToCharArray()).ToArray();
                Console.WriteLine("Test data:");
                foreach(char[] row in grid)
                {
                    foreach(char c in row)
                    {
                        if (c == 'U')
                            Console.ForegroundColor = ConsoleColor.Green;
                        else
                            Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(c);
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();
                Console.WriteLine($"Your Answer: {b.MaxChain(grid)}\n");
                //Console.WriteLine($"Test data:\n{test}\nYour Answer: {b.MaxChain(grid)}\n");
            }
        }
    }
}
