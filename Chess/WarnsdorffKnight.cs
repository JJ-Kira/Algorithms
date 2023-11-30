namespace Algorithms.Chess
{
    internal class WarnsdorffKnight
    {
        // n: Size of the chessboard (n x n)
        private int n;

        // size: Total number of squares on the chessboard
        private int size;

        // chessboard: 2D array representing the chessboard, each cell contains the step number of the knight's tour
        private int[,] chessboard;

        // x, y: Arrays defining the 8 possible movements of a knight in chess
        private int[] x, y;

        public WarnsdorffKnight()
        {
            // Initialize the 8 possible moves for a knight in chess
            x = new int[] { 2, 1, -1, -2, -2, -1, 1, 2 };
            y = new int[] { 1, 2, 2, 1, -1, -2, -2, -1 };
        }

        // Prints the current state of the chessboard
        public void PrintTour()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    Console.Write(chessboard[i, j] + "\t");
                Console.WriteLine("\n");
            }
            Console.WriteLine("\n");
        }

        // Initializes the chessboard and starts the knight's tour using the Warnsdorf heuristic
        public void SolveKnightTour(int n, int startX, int startY)
        {
            size = n * n;
            this.n = n;
            chessboard = new int[n, n];

            if (startX < 0 || startX >= n || startY < 0 || startY >= n)
                throw new ArgumentException("Invalid starting position!");

            bool solutionFound = false;

            Warnsdorff(1, startX, startY, ref solutionFound);

            if (solutionFound)
                PrintTour();
            else
                Console.WriteLine("No solutions!");
        }

        // Checks if the given coordinates are within the bounds of the chessboard and the cell is not yet visited
        private bool CheckLimits(int x, int y)
        {
            return ((x >= 0 && y >= 0) && (x < n && y < n) && (chessboard[x, y] == 0));
        }

        // Determines the next move of the knight based on Warnsdorf's rule
        private int CheckDegree(int x, int y)
        {
            int minMoves = 9, currentIndex, minIndex = 0, nextX, nextY;

            for (int i = 0; i < 8; i++)
            {
                currentIndex = 0;
                nextX = x + this.x[i];
                nextY = y + this.y[i];
                if (CheckLimits(nextX, nextY))
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (CheckLimits(nextX + this.x[j], nextY + this.y[j]))
                            currentIndex++;
                    }
                    if (currentIndex < minMoves)
                    {
                        minMoves = currentIndex;
                        minIndex = i;
                    }
                }
            }
            return minIndex;
        }

        // Recursively attempts to solve the Knight's Tour using Warnsdorff's heuristic
        public void Warnsdorff(int i, int x, int y, ref bool solutionFound)
        {
            int u, v, ind;
            chessboard[x, y] = i; // Mark the current cell with the step number

            if (i < size)
            {
                ind = CheckDegree(x, y); // Find the next move based on Warnsdorf's rule

                u = x + this.x[ind];
                v = y + this.y[ind]; // Calculate the coordinates for the next move

                do
                {
                    Warnsdorff(i + 1, u, v, ref solutionFound);
                    if (!solutionFound)
                        chessboard[u, v] = 0; // Backtrack if no further moves are possible
                }
                while (!solutionFound && CheckLimits(u, v)); // Continue while solution not found and move is valid
            }
            else
                solutionFound = true; // Solution found, all squares are visited
        }
    }
}
