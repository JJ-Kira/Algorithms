namespace Algorithms.Graphs
{
    internal class Bridges
    {
        private int[] visited; // Array to track visited vertices
        private int[] low; // Array to store lowest discovery time reachable from a vertex
        private Graph graph; // Graph object
        private int size; // Number of vertices in the graph
        private int count; // Counter to assign a discovery time to each vertex

        public Bridges(Graph graph)
        {
            this.graph = graph;
            size = graph.vertices.Count;
            visited = new int[size];
            low = new int[size];
            count = 0;
        }

        // Method to find bridges starting from a random vertex
        public void FindBridges()
        {
            Random rnd = new Random();
            int root = rnd.Next(0, size);
            Console.WriteLine($"Root: {root + 1}");

            // Traversal Start: Choose a random/root vertex or use a specified root to start the DFS traversal.
            BridgesDFS(root, root);
        }

        // Method to find bridges starting from a specified root vertex
        public void FindBridges(int root)
        {
            BridgesDFS(root, root);
        }

        // DFS-based algorithm to find bridges
        private void BridgesDFS(int u, int parentVertex)
        {
            visited[u] = low[u] = ++count;

            var vertices = graph.vertices.First(x => x.num - 1 == u).neighbors;

            foreach (var v in vertices)
            {
                if (v - 1 != parentVertex)
                {
                    if (visited[v - 1] == 0)
                    {
                        // DFS Recursive Step:
                        // - Visit the current vertex (u).
                        // - Update visited and low arrays for the current vertex.
                        BridgesDFS(v - 1, u);
                        low[u] = low[u] < low[v - 1] ? low[u] : low[v - 1];
                    }
                    else
                        low[u] = low[u] < visited[v - 1] ? low[u] : visited[v - 1];
                }
            }

            // Handling Conditions:
            // - For bridges: Check if the current edge is a bridge based on the condition met.
            if (low[u] == visited[u] && u != parentVertex)
                Console.WriteLine($"Bridge: {u + 1} {parentVertex + 1}");
        }
    }
}
