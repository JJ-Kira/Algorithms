namespace Algorithms.Graphs
{
    internal class ArticulationPoints
    {
        private int[] visited; // Array to track visited vertices
        private int[] ldt; // Array to store lowest discovery time reachable from a vertex
        private Graph graph; // Graph object
        private int size; // Number of vertices in the graph
        private int count; // Counter to assign a discovery time to each vertex

        private List<int> articulationPoints = new List<int>(); // List to store identified articulation points

        public ArticulationPoints(Graph graph)
        {
            this.graph = graph;
            size = graph.vertices.Count;
            visited = new int[size];
            ldt = new int[size];
            count = 0;
        }

        // Method to find articulation points starting from a random vertex
        public void FindArticulationPoints()
        {
            Random rnd = new Random();
            int root = rnd.Next(0, size);
            Console.WriteLine($"Root: {root + 1}");

            // Traversal Start: Choose a random/root vertex or use a specified root to start the DFS traversal.
            ArticulationPointsDFS(root, root);

            foreach (var art in articulationPoints)
                Console.WriteLine($"Articulation Point: {art + 1}");
        }

        // Method to find articulation points starting from a specified root vertex
        public void FindArticulationPoints(int root)
        {
            ArticulationPointsDFS(root, root);
        }

        // DFS-based algorithm to find articulation points
        private void ArticulationPointsDFS(int u, int parentVertex)
        {
            int countChildren = 0;

            // Initialization: Initialize visited, low, count, and other necessary variables.
            visited[u] = ldt[u] = ++count;

            var vertices = graph.vertices.First(x => x.num - 1 == u).neighbors;

            foreach (var v in vertices)
            {
                if (v - 1 != parentVertex)
                {
                    if (visited[v - 1] == 0)
                    {
                        countChildren++;

                        // DFS Recursive Step:
                        // - Visit the current vertex (u).
                        // - Update visited and low arrays for the current vertex.
                        ArticulationPointsDFS(v - 1, u);
                        ldt[u] = ldt[u] < ldt[v - 1] ? ldt[u] : ldt[v - 1];
                    }
                    else
                        ldt[u] = ldt[u] < visited[v - 1] ? ldt[u] : visited[v - 1];
                }

                // Handling Conditions:
                // - For articulation points: Check if the current vertex satisfies the condition for being an articulation point.
                if (ldt[v - 1] >= visited[u] && u != parentVertex)
                {
                    if (!articulationPoints.Contains(u))
                        articulationPoints.Add(u);
                }
                if (parentVertex == u && countChildren > 1)
                {
                    if (!articulationPoints.Contains(u))
                        articulationPoints.Add(u);
                }
            }
        }
    }
}
