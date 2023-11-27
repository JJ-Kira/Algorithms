namespace Algorithms.Chess
{
    internal class Knight
    {
        private int n, size; // n: Size of the chessboard (n x n), size: Total number of squares
        private int[,] chessboard; // 2D array representing the chessboard
        private int[] x, y; // Arrays to define the possible movements of the knight
        private List<int[,]> allSolutions; // List to store all found solutions

        public Knight()
        {
            // Initialize the 8 possible moves for a knight in chess
            x = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
            y = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
            allSolutions = new List<int[,]>();
        }

        public void PrintTour(int[,] board)
        {
            // Prints a single tour (solution)
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
            // Iterates through all solutions and prints each
            int count = 0;
            foreach (var solution in allSolutions)
            {
                Console.WriteLine($"--- Solution {count++} ---\n");
                PrintTour(solution);
            }
        }

        public void SolveKnightTour(int boardSize, int startX, int startY)
        {
            // Solves the Knight's Tour for a single solution
            InitializeBoard(boardSize, startX, startY);
            if (TryNextMove(2, startX, startY, false)) // false for single solution mode
                PrintTour(chessboard);
            else
                Console.WriteLine("No solution found!");
        }

        public void FindAllSolutions(int boardSize, int startX, int startY)
        {
            // Finds all solutions for the Knight's Tour from a given starting point
            InitializeBoard(boardSize, startX, startY);
            TryNextMove(2, startX, startY, true); // true for finding all solutions
            if (allSolutions.Count == 0)
                Console.WriteLine("No solutions found!");
            else
                PrintAllSolutions();
        }

        private void InitializeBoard(int boardSize, int startX, int startY)
        {
            // Initializes the chessboard and sets the starting position
            n = boardSize;
            size = n * n;
            chessboard = new int[n, n];
            allSolutions.Clear();

            if (startX < 0 || startX >= n || startY < 0 || startY >= n)
                throw new ArgumentException("Invalid starting position!");

            chessboard[startX, startY] = 1; // Mark the starting position
        }

        private bool TryNextMove(int moveNumber, int x, int y, bool findAll)
        {
            // Recursive method to try the next move of the Knight
            if (moveNumber > size)
            {
                // If all squares are visited, add to solutions (if findAll) and return true
                if (findAll)
                {
                    allSolutions.Add((int[,])chessboard.Clone());
                }
                return true;
            }

            bool foundAtLeastOne = false;

            for (int k = 0; k < 8; k++)
            {
                // Calculate the next move's coordinates
                int u = x + this.x[k];
                int v = y + this.y[k];

                // Check if the move is valid (within board and to an unvisited square)
                if (u >= 0 && u < n && v >= 0 && v < n && chessboard[u, v] == 0)
                {
                    chessboard[u, v] = moveNumber; // Mark the move
                    if (TryNextMove(moveNumber + 1, u, v, findAll))
                    {
                        foundAtLeastOne = true;
                        if (!findAll) // If not finding all solutions, return immediately after finding one
                            return true;
                    }

                    // Backtrack if no further move is possible
                    chessboard[u, v] = 0;
                }
            }

            return foundAtLeastOne;
        }
    }
}
