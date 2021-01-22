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
            int n = grid.Length;

            // Check rows
            foreach (char[] row in grid)
            {
                int t = MaxChain(row);
                if (t > maxChain) maxChain = t;
            }

            // Check columns
            for (int i = 0; i < n; i++)
            {
                var column = Enumerable.Range(0, n).Select(r => grid[r][i]);
                int t = MaxChain(column.ToArray());
                if (t > maxChain) maxChain = t;
            }

            // Check / diagonals
            for (int i = 0; i < 2 * n - 1; i++)
            {
                List<char> diag = new List<char>();
                for (int j = Math.Max(i - n + 1, 0); j < Math.Min(i + 1, n); j++)
                    diag.Add(grid[i - j][j]);
                int t = MaxChain(diag.ToArray());
                if (t > maxChain) maxChain = t;
            }

            // Check \ diagonals
            for (int i = 0; i < 2 * n - 1; i++)
            {
                List<char> diag = new List<char>();
                for (int j = Math.Max(i - n + 1, 0); j < Math.Min(i + 1, n); j++)
                    diag.Add(grid[n - i + j - 1][j]);
                int t = MaxChain(diag.ToArray());
                if (t > maxChain) maxChain = t;
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

        public int TicTacNo(int n)
        {
            if (n == 2)
                return 3;
            /*
             * Going down the leading diagonal works for odd n
             * U....
             * .U...
             * ..U..
             * ...U.
             * ....U
             * Rotating the centre 2x2 makes it work for even n
             * U.....
             * .U....
             * ...U..
             * ..U...
             * ....U.
             * .....U
             * which is n blocks
             */
            return n;
        }

        public char[][] ChainBlocker(int n, int k)
        {
            // Start with a fully unblocked grid
            char[][] grid = new char[n][];
            for (int i = 0; i < n; i++)
                grid[i] = Enumerable.Repeat('U', n).ToArray();

            // Fully blocked grid
            if (k == 1)
            {
                for (int i = 0; i < n; i++)
                    grid[i] = Enumerable.Repeat('B', n).ToArray();
                return grid;
            }

            // Impossible to have k-chains
            if (k > n)
                // Return the fully unblocked grid
                return grid;

            // Special case from b-2
            if (n == k)
            {
                // Block diagonal
                for (int i = 0; i < n; i++)
                    grid[i][i] = 'B';
                // Fix centre for even n
                if (n % 2 == 0)
                {
                    grid[n / 2 - 1][n / 2 - 1] = 'U';
                    grid[n / 2 - 1][n / 2] = 'B';
                    grid[n / 2][n / 2 - 1] = 'B';
                    grid[n / 2][n / 2] = 'U';
                }
                return grid;
            }

            // Run greedy algorithm
            GreedyChainBlocker(n, k, grid);

            return grid;
        }

        private void GreedyChainBlocker(int n, int k, char[][] grid)
        {
            // Just split it into (k-1)x(k-1) squares since that is what
            // the greedy algorithm was doing anyway
            for (int r = 0; r < n; r++)
            {
                char[] row = grid[r];
                for (int c = k - 1; c < n; c += k)
                    row[c] = 'B';
                if ((r + 1) % k == 0)
                {
                    for (int c = 0; c < n; c++)
                        row[c] = 'B';
                }
            }
        }
    }
}
