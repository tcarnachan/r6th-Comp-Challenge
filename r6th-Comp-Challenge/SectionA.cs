using System;
using System.Linq;
using System.Collections.Generic;

namespace r6thCompChallenge
{
    public class SectionA
    {
        public bool Chain(char[][] grid)
        {
            for (int i = 0; i < 3; i++)
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

        // Check if p has won
        private bool Chain(char[][] grid, char p)
        {
            for (int i = 0; i < 3; i++)
            {
                // Check row
                if (grid[i].All(c => c == p))
                    return true;
                // Check column
                if (Enumerable.Range(0, 3).All(c => grid[c][i] == p))
                    return true;
            }
            // Check leading diagonal
            if (Enumerable.Range(0, 3).All(i => grid[i][i] == p))
                return true;
            // Check other diagonal
            if (Enumerable.Range(0, 3).All(i => grid[i][2 - i] == p))
                return true;
            // No more possible 3-chains
            return false;
        }

        public bool TicTacToe(char[][] grid)
        {
            int diff = 0, other = 0;
            foreach (char[] row in grid)
            {
                foreach (char c in row)
                {
                    if (c == 'X') diff++;
                    else if (c == 'O') diff--;
                    else if (c != '-') other++;
                }
            }

            bool xWin = Chain(grid, 'X'), oWin = Chain(grid, 'O');

            return other == 0 && // No other characters
                diff >= -1 && diff <= 1 && // Difference between number of moves is between -1 and 1
                !(xWin && oWin) && // Cannot have two winners
                !(xWin && diff == -1) && // If X wins, then there cannot be more Os than Xs
                !(oWin && diff == 1); // If O wins, then there cannot be more Xs than Os
        }

        #region Tic Tac Toe testing
        /*
         * Just generate every possible valid board state
         * inefficient, but nice for testing since it is easy to verify that it works
         */
        public HashSet<string> GetAllPossibleBoards()
        {
            HashSet<string> possible = new HashSet<string>();

            // Initialise clean grid
            char[][] grid = new char[3][];
            for (int r = 0; r < 3; r++)
                grid[r] = Enumerable.Repeat('-', 3).ToArray();

            // Populate possible
            GetAllPossibleBoards(grid, 0, ref possible);
            GetAllPossibleBoards(grid, 1, ref possible);

            return possible;
        }

        private void GetAllPossibleBoards(char[][] grid, int nextPlayer, ref HashSet<string> possible)
        {
            // Add this board
            possible.Add(string.Concat(grid.Select(r => new string(r))));
            // Is the game over
            if (Chain(grid, 'X') || Chain(grid, 'O') || !grid.Any(r => r.Any(c => c == '-'))) return;
            // Continue recursing
            foreach (char[] row in grid)
            {
                for (int c = 0; c < 3; c++)
                {
                    if (row[c] != '-') continue;
                    row[c] = "XO"[nextPlayer];
                    GetAllPossibleBoards(grid, 1 - nextPlayer, ref possible);
                    row[c] = '-';
                }
            }
        }
        #endregion
    }
}
