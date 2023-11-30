using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Chess
{
    internal class GradientQueen
    {
        private int[,] chessBoard;
        private int N;
        private int[] column; // pozycja w i-tej kolumnie


        private void PrintQueensPositions()
        {
            for (int i = 0, j = 1; i < N; j++, i++)
            {
                chessBoard[i, column[i]] = j;
            }

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    Console.Write(chessBoard[i, j] + "\t");
                Console.WriteLine();
            }
        }

        //permution ensures no conflicts on rows and columns
        private void Permutation()
        {
            column = Enumerable.Repeat(-1, N).ToArray();
            int tmp;
            int ind = 0;
            Random rnd = new Random();
            while (ind < N)
            {
                tmp = rnd.Next(0, N);

                if (!column.Contains(tmp)) // if tmp is not in x
                {
                    column[ind] = tmp;
                    ind++;
                }
            }
        }

        private bool IsUnderAttack(int pos)
        {

            for (int i = 0; i < N; i++)
            {
                if (i != pos)
                {
                    if (i + column[i] == pos + column[pos] || i - column[i] == pos - column[pos])
                        return true;
                }
            }
            return false;
        }

        private void SwapQueens(int i, int j)
        {
            int iQueen = column[i];
            int jQueen = column[j];
            column[i] = jQueen;
            column[j] = iQueen;
        }
        private bool ReductionOfCollisionsCheck(int i, int j)
        {
            int countOldConf = CollisionNumber();

            SwapQueens(i, j);

            int countNewConf = CollisionNumber();

            SwapQueens(i, j);

            return countNewConf < countOldConf;
        }

        private int CollisionNumber()
        {
            int countConflicts = 0;
            for (int i = 0; i < N; i++)
            {
                if (IsUnderAttack(i))
                    countConflicts++;
            }
            return countConflicts;
        }

        private void QueenSearch()
        {
            int swapsPerformed;

            while (CollisionNumber() != 0) // stop when there are no collisions
            {
                Permutation();
                do
                {
                    swapsPerformed = 0;
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = i + 1; j < N; j++)
                        {
                            if (IsUnderAttack(i) || IsUnderAttack(j))
                            {
                                if (ReductionOfCollisionsCheck(i, j))
                                {
                                    SwapQueens(i, j);
                                    swapsPerformed++;
                                }
                            }
                        }
                    }
                } while (swapsPerformed != 0);
            }
        }

        public void SolveNQueens(int n)
        {
            N = n;
            chessBoard = new int[N, N];

            Permutation();
            QueenSearch();

            PrintQueensPositions();

        }
    }
}
