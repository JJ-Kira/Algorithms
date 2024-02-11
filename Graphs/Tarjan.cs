using System.Collections.Generic;

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

        private readonly int vertCount; // Number of vertices in the graph
        private int[] T; // Array T where T[i] represents the parent of the ith set in the Union-Find structure
        private int[] ancestors; // An array to store the ancestor of each vertex
        private bool[] processed; // Array to mark if a vertex has been processed

        List<Tuple<int, int>> list; // List of queries for finding the nearest common ancestor

        public Tarjan(Graph graph)
        {
            this.graph = graph;

            vertCount = graph.vertices.Count;
            T = new int[vertCount];
            processed = new bool[vertCount];
            ancestors = new int[vertCount];

            stack = new Stack<Vertex>();
            discoveryTimeIndex = 0;
            sccs = new List<List<int>>();
            indices = new int[graph.nodes.Count];
            lowLink = new int[graph.nodes.Count];
            onStack = new bool[graph.nodes.Count];
            Array.Fill(indices, -1); // Initialize all indices to -1
        }

        // Merges two sets containing x and y into one set
        private void Union(int x, int y)
        {
            int a = Find(x), b = Find(y); // Find the representatives of the sets containing x and y
            if (a != b)
            {
                // If tree height of a is less than b, merge a into b
                if (T[a] < T[b]) // Note: T stores negative values to represent the size of the set
                {
                    int tmp = T[b];
                    T[a] += tmp; // Merge b into a
                    T[b] = a; // Set a as the new root
                }
                else // If T[a] == T[b] or T[a] > T[b], merge b into a
                {
                    int tmp = T[a];
                    T[a] = b;
                    T[b] += tmp; // Update the size of the set after merge
                }
            }
        }

        // Finds the representative of the set containing x with path compression
        private int Find(int x)
        {
            int rep = x;
            while (T[rep] > 0) rep = T[rep]; // Find the root of the set
            if (T[rep] > 0)
            {
                // Path compression: make all nodes along the path point directly to the root
                while (T[x] != rep)
                {
                    int tmp = T[x];
                    T[x] = rep;
                    x = tmp;
                }
            }
            return rep;
        }

        // Initializes the algorithm to find the nearest common ancestor
        public void FindNearestCommonAncestor(List<Tuple<int, int>> list)
        {
            this.list = new List<Tuple<int, int>>(list);
            Console.WriteLine(list.Count);
            TarjanLCA(0); // Start the algorithm from the first vertex of the tree
        }

        // Initializes a vertex as its own set representative
        private void MakeSet(int u)
        {
            T[u] = -1; // Initialize the vertex as its own set representative
        }

        // Main function of the Tarjan's offline LCA algorithm
        private void TarjanLCA(int u)
        {
            MakeSet(u);
            ancestors[u] = u; // Initially, the ancestor of u is itself

            // Iterate through the neighbors of vertex u
            var vertices = graph.vertices.First(x => x.num == u + 1).neighbors;
            foreach (var v in vertices)
            {
                TarjanLCA(v - 1); // Recurse for each neighbor
                Union(u, v - 1); // Union the sets of u and v
                ancestors[Find(u)] = u; // Set the ancestor of the merged set
            }

            processed[u] = true; // Mark u as processed

            // Check the list of queries for common ancestors
            for (int i = 0; i < list.Count; i++)
            {
                // If u is one of the vertices in the query and the other vertex is processed
                if (u == list[i].Item1 - 1 && processed[list[i].Item2 - 1])
                    Console.WriteLine($"Nearest Common Ancestor for {list[i].Item1} and {list[i].Item2}: {ancestors[Find(list[i].Item2 - 1)] + 1}");
                else if (u == list[i].Item2 - 1 && processed[list[i].Item1 - 1])
                    Console.WriteLine($"Nearest Common Ancestor for {list[i].Item1} and {list[i].Item2}: {ancestors[Find(list[i].Item1 - 1)] + 1}");
            }
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
