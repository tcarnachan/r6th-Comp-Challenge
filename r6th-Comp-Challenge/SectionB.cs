using System;
using System.Linq;
using System.Collections.Generic;

namespace r6thCompChallenge
{
    public class SectionB
    {
        public int MaxChain(char[][] grid)
        {
            int maxChain = 0;

            // Check rows
            foreach(char[] row in grid)
            {
                int t = MaxChain(row);
                if (t > maxChain)
                    maxChain = t;
            }

            for(int i = 0; i < grid.Length; i++)
            {
                // Check column
                var column = Enumerable.Range(0, grid.Length).Select(r => grid[r][i]);
                int t = MaxChain(column.ToArray());
                if (t > maxChain)
                    maxChain = t;

                // Check / diagonal
                List<char> diagonal = new List<char>();
                for (int offset = 0; i - offset >= 0; offset++)
                    diagonal.Add(grid[offset][i - offset]);
                t = MaxChain(diagonal.ToArray());
                if (t > maxChain)
                    maxChain = t;

                // Check \ diagonal
                diagonal.Clear();
                for (int offset = 0; offset <= i; offset++)
                    diagonal.Add(grid[i - offset][offset]);
                t = MaxChain(diagonal.ToArray());
                if (t > maxChain)
                    maxChain = t;
            }

            return maxChain;
        }

        private int MaxChain(char[] row)
        {
            int start = -1, max = 0, end;
            for (end = 0; end < row.Length; end++)
            {
                if (row[end] == 'U')
                {
                    if (start == -1)
                        start = end;
                }
                else if (start != -1)
                {
                    int t = end - start;
                    if (t > max)
                        max = t;
                    start = -1;
                }
            }
            if (start != -1)
                return Math.Max(end - start, max);
            return max;
        }
    }
}
