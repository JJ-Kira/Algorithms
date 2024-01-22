namespace Algorithms.Graphs
{
    internal class Tarjan
    {
        private Graph graph; // The graph on which Tarjan's algorithm is applied
        private Stack<Vertex> stack; // Stack used for Tarjan's DFS
        private int discoveryTimeIndex; // Index used for assigning discovery times to vertices
        private List<List<int>> sccs; // List of strongly connected components
        private int[] indices; // Array storing the discovery index of each vertex
        private int[] lowLink; // Array storing the low-link value of each vertex
        private bool[] onStack; // Boolean array to check if a vertex is in the stack

        public Tarjan(Graph graph)
        {
            this.graph = graph;
            stack = new Stack<Vertex>();
            discoveryTimeIndex = 0;
            sccs = new List<List<int>>();
            indices = new int[graph.nodes.Count];
            lowLink = new int[graph.nodes.Count];
            onStack = new bool[graph.nodes.Count];
            Array.Fill(indices, -1); // Initialize all indices to -1
        }

        // Main method to start the Tarjan's algorithm
        public void FindStronglyConnectedComponents()
        {
            foreach (var v in graph.vertices)
            {
                if (indices[v.num] == -1)
                {
                    DepthFirstSearch(v);
                }
            }
        }

        // DFS method for Tarjan's algorithm
        private void DepthFirstSearch(Vertex v)
        {
            // Step K01-K05: Initialize vertex parameters and add it to stack
            indices[v.num] = discoveryTimeIndex;
            lowLink[v.num] = discoveryTimeIndex;
            discoveryTimeIndex++;
            stack.Push(v);
            onStack[v.num] = true;

            // Step K06: Explore all neighbors
            foreach (var wNum in v.neighbors)
            {
                Vertex w = graph.vertices.Find(vertex => vertex.num == wNum);
                // Step K07-K12: Update low-link value based on neighbor exploration
                if (indices[w.num] == -1)
                {
                    DepthFirstSearch(w);
                    lowLink[v.num] = Math.Min(lowLink[v.num], lowLink[w.num]);
                }
                else if (onStack[w.num])
                {
                    lowLink[v.num] = Math.Min(lowLink[v.num], indices[w.num]);
                }
            }

            // Step K13-K29: Pop the stack and form SCCs
            if (lowLink[v.num] == indices[v.num])
            {
                List<int> component = new List<int>();
                Vertex w;
                do
                {
                    w = stack.Pop();
                    onStack[w.num] = false;
                    component.Add(w.num);
                } while (w != v);
                sccs.Add(component);
            }
        }

        // Method to print the SCCs found
        public void PrintStronglyConnectedComponents()
        {
            int count = 1;
            foreach (var scc in sccs)
            {
                Console.Write($"SCC {count} : ");
                foreach (var node in scc)
                {
                    Console.Write($"{node} ");
                }
                Console.WriteLine();
                count++;
            }
        }
    }
}
