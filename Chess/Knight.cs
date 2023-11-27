using System;
using System.Collections.Generic;

namespace Algorithms.Chess
{
    internal class Knight
    {
        private int n, size;
        private int[,] chessboard;
        private int[] x, y;
        private List<int[,]> allSolutions;

        public Knight()
        {
            x = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
            y = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
            allSolutions = new List<int[,]>();
        }

        public void PrintTour(int[,] board)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(board[i, j] + "\t");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintAllSolutions()
        {
            int count = 0;
            foreach (var solution in allSolutions)
            {
                Console.WriteLine($"--- Solution {count++} ---\n");
                PrintTour(solution);
            }
        }

        public void SolveKnightTour(int boardSize, int startX, int startY)
        {
            InitializeBoard(boardSize, startX, startY);
            if (TryNextMove(2, startX, startY, false)) // false indicates single solution mode
                PrintTour(chessboard);
            else
                Console.WriteLine("No solution found!");
        }

        public void FindAllSolutions(int boardSize, int startX, int startY)
        {
            InitializeBoard(boardSize, startX, startY);
            TryNextMove(2, startX, startY, true); // true indicates all solutions mode
            if (allSolutions.Count == 0)
                Console.WriteLine("No solutions found!");
            else
                PrintAllSolutions();
        }

        private void InitializeBoard(int boardSize, int startX, int startY)
        {
            n = boardSize;
            size = n * n;
            chessboard = new int[n, n];
            allSolutions.Clear();

            if (startX < 0 || startX >= n || startY < 0 || startY >= n)
                throw new ArgumentException("Invalid starting position!");

            chessboard[startX, startY] = 1;
        }

        private bool TryNextMove(int moveNumber, int x, int y, bool findAll)
        {
            if (moveNumber > size)
            {
                if (findAll)
                {
                    allSolutions.Add((int[,])chessboard.Clone());
                }
                return true;
            }

            bool foundAtLeastOne = false;

            for (int k = 0; k < 8; k++)
            {
                int u = x + this.x[k];
                int v = y + this.y[k];

                if (u >= 0 && u < n && v >= 0 && v < n && chessboard[u, v] == 0)
                {
                    chessboard[u, v] = moveNumber;
                    if (TryNextMove(moveNumber + 1, u, v, findAll))
                    {
                        foundAtLeastOne = true;
                        if (!findAll) // If not finding all solutions, return immediately after finding one
                            return true;
                    }

                    // Backtrack
                    chessboard[u, v] = 0;
                }
            }

            return foundAtLeastOne;
        }
    }
}
