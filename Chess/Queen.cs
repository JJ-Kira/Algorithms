using System.Diagnostics;

namespace Algorithms.Chess
{
    internal class Queen
    {
        // Stopwatch to measure the time taken to find solutions
        private Stopwatch watch = new Stopwatch();

        // Size of the chessboard (N x N)
        private int boardSize;

        // Array to store the column position of a queen in each row
        private int[]? columnPositions;

        // Arrays to check if a row or diagonal is available for placing a queen
        private bool[]? rowAvailable, mainDiagonalAvailable, antiDiagonalAvailable;

        // List to store all possible solutions
        private List<int[]>? allSolutions;

        // Solves the N-Queens problem for a single solution
        public void SingleSolveQueens(int size)
        {
            InitializeBoard(size); // Initialize the board with the given size
            watch.Start(); // Start the stopwatch
            bool solutionFound = false;
            TryPlaceQueen(0, ref solutionFound); // Start placing queens from the first row
            watch.Stop(); // Stop the stopwatch after finding a solution or exhausting possibilities

            if (solutionFound)
            {
                PrintSingleSolution(); // Print the found solution
            }
            else
            {
                Console.WriteLine("No solution found.");
            }
            Console.WriteLine("Time for single solution: " + watch.ElapsedMilliseconds + "ms\n");
        }

        // Solves the N-Queens problem for all possible solutions
        public void SolveAllQueens(int size)
        {
            InitializeBoard(size); // Initialize the board with the given size
            allSolutions = new List<int[]>(); // Initialize the list to store all solutions
            watch.Start(); // Start the stopwatch
            TryPlaceQueenAllSolutions(0); // Start placing queens from the first row
            watch.Stop(); // Stop the stopwatch after finding all solutions

            //PrintAllSolutions(); // Print all found solutions
            Console.WriteLine("Time for all solutions: " + watch.ElapsedMilliseconds + "ms\n");
        }

        // Initializes the board and auxiliary arrays
        private void InitializeBoard(int size)
        {
            this.boardSize = size;
            columnPositions = new int[size]; // Initialize column positions
            // Initialize availability of rows and diagonals
            rowAvailable = Enumerable.Repeat(true, size).ToArray();
            mainDiagonalAvailable = Enumerable.Repeat(true, 2 * size - 1).ToArray();
            antiDiagonalAvailable = Enumerable.Repeat(true, 2 * size - 1).ToArray();
        }

        // Recursive method to place queens for a single solution
        private void TryPlaceQueen(int row, ref bool solutionFound)
        {
            for (int col = 0; col < boardSize && !solutionFound; col++)
            {
                if (IsSafe(row, col)) // Check if it's safe to place a queen at the current position
                {
                    PlaceQueen(row, col); // Place the queen
                    if (row < boardSize - 1)
                    {
                        TryPlaceQueen(row + 1, ref solutionFound); // Recursively try to place the next queen
                        if (!solutionFound)
                        {
                            RemoveQueen(row, col); // Backtrack: Remove the queen and try the next position
                        }
                    }
                    else
                    {
                        solutionFound = true; // All queens are placed successfully
                    }
                }
            }
        }

        // Recursive method to place queens for all solutions
        private void TryPlaceQueenAllSolutions(int row)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if (IsSafe(row, col)) // Check if it's safe to place a queen at the current position
                {
                    PlaceQueen(row, col); // Place the queen
                    if (row < boardSize - 1)
                    {
                        TryPlaceQueenAllSolutions(row + 1); // Recursively try to place the next queen
                    }
                    else
                    {
                        allSolutions.Add((int[])columnPositions.Clone()); // Add the current solution to the list
                    }
                    RemoveQueen(row, col); // Backtrack: Remove the queen and try the next position
                }
            }
        }

        // Checks if it's safe to place a queen at the given position
        private bool IsSafe(int row, int col)
        {
            // Check if the row, main diagonal, and anti-diagonal are available
            return rowAvailable[col] && mainDiagonalAvailable[row + col] && antiDiagonalAvailable[row - col + boardSize - 1];
        }

        // Places a queen at the given position
        private void PlaceQueen(int row, int col)
        {
            columnPositions[row] = col; // Mark the column position of the queen
            // Mark the row, main diagonal, and anti-diagonal as not available
            rowAvailable[col] = false;
            mainDiagonalAvailable[row + col] = false;
            antiDiagonalAvailable[row - col + boardSize - 1] = false;
        }

        // Removes a queen from the given position
        private void RemoveQueen(int row, int col)
        {
            // Mark the row, main diagonal, and anti-diagonal as available again
            rowAvailable[col] = true;
            mainDiagonalAvailable[row + col] = true;
            antiDiagonalAvailable[row - col + boardSize - 1] = true;
        }

        // Prints the single solution found
        private void PrintSingleSolution()
        {
            Console.WriteLine("Single Solution:");
            PrintSolution(columnPositions); // Print the solution
        }

        // Prints all solutions found
        private void PrintAllSolutions()
        {
            Console.WriteLine($"All Solutions (Total: {allSolutions.Count}):");
            foreach (var solution in allSolutions)
            {
                PrintSolution(solution); // Print each solution
                Console.WriteLine();
            }
        }

        // Helper method to print a solution
        private void PrintSolution(int[] solution)
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    // Print 'Q' for a queen and '.' for an empty space
                    Console.Write(solution[i] == j ? "Q " : ". ");
                }
                Console.WriteLine();
            }
        }
    }
}
