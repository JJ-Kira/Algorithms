namespace Algorithms.Chess
{
    internal class GradientQueen
    {
        private int[,] chessboard; // 2D array representing the chessboard. Values represent queen's positions.
        private int n; // Size of the chessboard (n x n) and number of queens.
        private int[] column; // Array representing queen's position in each column.

        // Prints the final positions of the queens on the chessboard.
        private void PrintQueensPositions()
        {
            // Assign a unique number to each queen's position for printing.
            for (int i = 0; i < n; i++)
            {
                chessboard[i, column[i]] = i + 1;
            }

            // Print the chessboard with queens' positions.
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(chessboard[i, j] + "\t");
                Console.WriteLine();
            }
        }

        // Initializes the queens' positions randomly.
        private void Permutation()
        {
            column = Enumerable.Repeat(-1, n).ToArray();
            int tmp;
            int ind = 0;
            Random rnd = new Random();
            while (ind < n)
            {
                tmp = rnd.Next(0, n);

                // Ensure each queen is in a unique column.
                if (!column.Contains(tmp))
                {
                    column[ind] = tmp;
                    ind++;
                }
            }
        }

        // Checks if a queen at position 'pos' is under attack.
        private bool IsUnderAttack(int pos)
        {
            for (int i = 0; i < n; i++)
            {
                if (i != pos)
                {
                    // Check if any queen is on the same diagonal.
                    if (i + column[i] == pos + column[pos] || i - column[i] == pos - column[pos])
                        return true;
                }
            }
            return false;
        }

        // Swaps the positions of two queens.
        private void SwapQueens(int i, int j)
        {
            int temp = column[i];
            column[i] = column[j];
            column[j] = temp;
        }

        // Checks if swapping two queens reduces the number of conflicts.
        private bool ReductionOfCollisionsCheck(int i, int j)
        {
            int countOldConf = CollisionNumber();

            SwapQueens(i, j);
            int countNewConf = CollisionNumber();

            // Revert the swap.
            SwapQueens(i, j);

            return countNewConf < countOldConf;
        }

        // Counts the total number of conflicts (queens under attack).
        private int CollisionNumber()
        {
            int countConflicts = 0;
            for (int i = 0; i < n; i++)
            {
                if (IsUnderAttack(i))
                    countConflicts++;
            }
            return countConflicts;
        }

        // Main method to find a solution using a gradient descent-like approach.
        private void QueenSearch()
        {
            int swapsPerformed;

            // Continue until a solution is found (no conflicts).
            while (CollisionNumber() != 0)
            {
                Permutation();
                do
                {
                    swapsPerformed = 0;
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = i + 1; j < n; j++)
                        {
                            // Check if swapping reduces conflicts.
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
                } while (swapsPerformed != 0); // Repeat until no beneficial swaps can be made.
            }
        }

        // Public method to solve the N Queens problem.
        public void SolveNQueens(int n)
        {
            this.n = n;
            chessboard = new int[this.n, this.n];

            Permutation();
            QueenSearch();

            PrintQueensPositions();
        }
    }
}