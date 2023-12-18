namespace Algorithms.Graphs
{
    internal class KruskalUnionFind
    {
        private Graph graph; // Represents the graph where the algorithm operates
        private List<Edge> MST = new List<Edge>(); // Minimum Spanning Tree (MST)
        private readonly int N; // Number of vertices in the graph
        private int[] disjointSets; // Represents sets (disjoint sets), each element is a set representative
        private int pathCost = 0; // Total cost of the MST

        // Constructor initializing the UnionFind structure
        public KruskalUnionFind(Graph graph)
        {
            this.graph = graph;
            N = graph.nodes.Count;
            disjointSets = Enumerable.Repeat(-1, N).ToArray(); // Initialize each vertex as its own set representative
        }

        // Prints the edges in the MST and the set representatives
        public void PrintMST()
        {
            Console.WriteLine("MST edges:");
            foreach (var edge in MST)
                Console.Write($"{edge.VertexStart} - {edge.VertexEnd} \n");
            Console.WriteLine("T array:");
            for (int i = 0; i < N; i++)
                Console.Write($"T[{i}] = {disjointSets[i]} \n");

            Console.WriteLine($"Minimal cost of MST: {pathCost}");
        }

        // Combines two sets by their representatives
        private void Union(int x, int y)
        {
            int a = Find(x), b = Find(y); // Finding representatives of sets containing x and y
            if (a != b)
            {
                if (disjointSets[a] < disjointSets[b]) // Union by rank (size of sets)
                {
                    int tmp = disjointSets[b];
                    disjointSets[a] += tmp;
                    disjointSets[b] = a;
                }
                else
                {
                    int tmp = disjointSets[a];
                    disjointSets[a] = b;
                    disjointSets[b] += tmp;
                }
            }
        }

        // Finds the representative of the set containing x with path compression
        private int Find(int x)
        {
            int r = x;
            while (disjointSets[r] > 0) r = disjointSets[r]; // Finding the representative
            if (disjointSets[r] > 0)
            {
                while (disjointSets[x] != r) // Path compression: Update each node's representative to the final representative
                {
                    int tmp = disjointSets[x];
                    disjointSets[x] = r;
                    x = tmp;
                }
            }
            return r;
        }

        // Kruskal's Algorithm implementation
        public void KruskalAlgorithm()
        {
            // Sort the edges in ascending order by weight
            var sortedEdges = graph.edges.OrderBy(x => x.Weight);
            int counter = 0;

            // Choose the most optimal edges and add them to the solution until N-1 edges are selected
            while (counter < N - 1)
            {
                foreach (var edge in sortedEdges)
                {
                    int vertex1 = edge.VertexStart - 1;
                    int vertex2 = edge.VertexEnd - 1;

                    // Check if adding this edge forms a cycle in the MST
                    if (Find(vertex1) != Find(vertex2))
                    {
                        pathCost += edge.Weight;
                        Union(vertex1, vertex2);
                        counter++;
                        MST.Add(edge);
                    }
                }
            }
        }
    }
}
