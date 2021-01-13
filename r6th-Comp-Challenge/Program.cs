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
            TestB2(b);
        }

        private static void TestA1(SectionA a)
        {
            bool[] expectedResults = new bool[] { true, true, true, true, false, true, true, false, true, true };
            Console.WriteLine(string.Join("\n", GetTestCasesGrid("a2").Zip(expectedResults, (test, result) =>
            {
                string formatted = string.Join("\n-----\n", test.Select(r => string.Join("|", r)));
                return $"Test data:\n{formatted}\nExpected Answer: {result}\nYour Answer: {a.Chain(test)}\n";
            })));
        }

        private static void TestA2(SectionA a)
        {
            HashSet<string> possible = a.GetAllPossibleBoards();

            foreach (char[][] grid in GetTestCasesGrid("a2"))
            {
                bool result = possible.Contains(string.Concat(grid.Select(r => new string(r))));
                string test = string.Join("\n-----\n", grid.Select(r => string.Join("|", r)));
                Console.WriteLine($"Test data:\n{test}\nExpected Answer: {result}\nYour Answer: {a.TicTacToe(grid)}\n");
            }
        }

        private static void TestB1(SectionB b)
        {
            foreach (char[][] grid in GetTestCasesGrid("b1"))
            {
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
            }
        }

        private static void TestB2(SectionB b)
        {
            foreach (int test in GetTestCasesIntArray("b2"))
            {
                Console.WriteLine($"Test: {test}\nYour Answer: {b.TicTacNo(test)}\n");
            }
        }

        private static List<char[][]> GetTestCasesGrid(string challenge)
        {
            string file = File.ReadAllText($"Input Data/input_{challenge}.txt").Replace("\r\n", "\n");
            string[] testCases = file.Split(new string[] { "\n\n" }, StringSplitOptions.None);
            List<char[][]> grids = new List<char[][]>();
            foreach (string test in testCases)
                grids.Add(test.Split('\n').Select(l => l.ToCharArray()).ToArray());
            return grids;
        }

        private static int[] GetTestCasesIntArray(string challenge)
        {
            string file = File.ReadAllText($"Input Data/input_{challenge}.txt").Replace("\r\n", "\n");
            return file.Split().Select(int.Parse).ToArray();
        }
    }
}
