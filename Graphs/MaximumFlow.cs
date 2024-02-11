namespace Algorithms.Graphs
{
    internal class MaximumFlow
    {
        private Graph graph; // Reference to the original graph
        private List<Vertex> vertices; // List to store vertices with their properties for the algorithm
        private List<Edge> edges; // List of edges in the graph

        private Graph residualGraph;
        private int N;

        // Constructor
        public MaximumFlow(Graph graph)
        {
            this.graph = graph;
            this.vertices = new List<Vertex>();
            this.edges = new List<Edge>(graph.edges);
            InitializeVertices();
        }

        // Initializes vertices for the algorithm
        private void InitializeVertices()
        {
            foreach (var node in graph.nodes)
            {
                vertices.Add(new Vertex(node, 0));
            }
        }

        // Function to calculate maximum flow using Push-Relabel algorithm
        public int CalculateMaximumFlowPR(int source, int sink)
        {
            Preflow(source); // Initialize preflow

            // Core algorithm loop: Continue until there's no vertex with excess flow
            while (OverFlowVertex() != -1)
            {
                int u = OverFlowVertex();
                if (!Push(u)) // Attempt to push flow from u
                    Relabel(u); // If push is not successful, relabel the vertex
            }

            return vertices[sink].excessFlow; // Maximum flow is the excess flow at the sink
        }

        // Initializes preflow in the graph
        private void Preflow(int s)
        {
            vertices[s].h = vertices.Count; // Set the height of the source vertex

            List<Edge> newEdges = new List<Edge>(); // Temporary list for new edges

            // Initialize flow and reverse edges for edges from the source
            foreach (var edge in edges.Where(e => e.VertexStart == s).ToList())
            {
                edge.Flow = edge.Weight; // Set initial flow to capacity
                vertices[edge.VertexEnd].excessFlow += edge.Flow; // Update excess flow

                // Create reverse edge for the residual graph
                Edge reverseEdge = new Edge(edge.VertexEnd, edge.VertexStart, 0);
                reverseEdge.Flow = -edge.Flow;
                newEdges.Add(reverseEdge);
            }

            edges.AddRange(newEdges); // Add the new reverse edges to the edge list
        }

        // Finds a vertex with excess flow
        private int OverFlowVertex()
        {
            for (int i = 1; i < vertices.Count - 1; i++)
            {
                if (vertices[i].excessFlow > 0)
                    return i;
            }
            return -1; // Return -1 if no overflowing vertex is found
        }

        // Tries to push flow from vertex u
        private bool Push(int u)
        {
            foreach (var edge in edges.Where(e => e.VertexStart == u))
            {
                // Skip if flow is already at capacity
                if (edge.Flow == edge.Weight)
                    continue;

                // Push flow if height of u is greater than height of adjacent vertex
                if (vertices[u].h > vertices[edge.VertexEnd].h)
                {
                    int flow = Math.Min(edge.Weight - edge.Flow, vertices[u].excessFlow);
                    vertices[u].excessFlow -= flow;
                    vertices[edge.VertexEnd].excessFlow += flow;
                    edge.Flow += flow;

                    UpdateReverseEdgeFlow(edge, flow); // Update flow in the reverse edge

                    return true;
                }
            }
            return false; // Return false if no push operation was successful
        }

        // Relabels the vertex to increase its height
        private void Relabel(int u)
        {
            int minHeight = int.MaxValue;

            // Find the minimum height among adjacent vertices
            foreach (var edge in edges.Where(e => e.VertexStart == u))
            {
                if (edge.Flow == edge.Weight)
                    continue;

                if (vertices[edge.VertexEnd].h < minHeight)
                {
                    minHeight = vertices[edge.VertexEnd].h;
                    vertices[u].h = minHeight + 1; // Set new height
                }
            }
        }

        // Updates the reverse flow for an edge
        private void UpdateReverseEdgeFlow(Edge edge, int flow)
        {
            // Check for existing reverse edge and update it
            foreach (var e in edges)
            {
                if (e.VertexStart == edge.VertexEnd && e.VertexEnd == edge.VertexStart)
                {
                    e.Flow -= flow;
                    return;
                }
            }

            // If no reverse edge found, create a new one
            edges.Add(new Edge(edge.VertexEnd, edge.VertexStart, flow));
        }

        // Function to calculate maximum flow using Ford-Fulkerson algorithm
        public int CalculateMaximumFlowFF(int source, int sink)
        {
            // Initialize residual graph and set up for Ford-Fulkerson
            N = graph.vertices.Count;
            InitializeResidualGraph(N); // Initialize residual graph with reverse edges

            int maxFlow = 0; // Start with 0 flow
            int[] parent = new int[N]; // Array to store path from source to sink

            // Main loop of Ford-Fulkerson: Find augmenting paths and update flows
            while (BFS(source, sink, parent))
            {
                // Find minimum residual capacity of the edges along the path filled by BFS
                int pathFlow = int.MaxValue;
                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    int cost = residualGraph.edges.First(x => x.VertexStart == u && x.VertexEnd == v).Weight;
                    pathFlow = Math.Min(pathFlow, cost);
                }

                // Update residual capacities of the edges and reverse edges along the path
                for (int v = sink; v != source; v = parent[v])
                {
                    int u = parent[v];
                    residualGraph.edges.First(x => x.VertexStart == u && x.VertexEnd == v).Weight -= pathFlow;
                    residualGraph.edges.First(x => x.VertexStart == v && x.VertexEnd == u).Weight += pathFlow;
                }

                maxFlow += pathFlow; // Add path flow to overall flow
            }
            return maxFlow; // Return the total flow as maximum flow
        }

        // BFS to find path from source to sink with positive residual capacity; updates parent[] to store the path
        bool BFS(int s, int t, int[] parent)
        {
            bool[] visited = new bool[N]; // Keep track of visited vertices
            Queue<int> queue = new Queue<int>(); // Queue for BFS
            queue.Enqueue(s);
            visited[s] = true;
            parent[s] = -1; // Source has no parent

            // Standard BFS loop
            while (queue.Count != 0)
            {
                int u = queue.Dequeue();
                foreach (var v in residualGraph.vertices.First(x => x.num == u).neighbors)
                {
                    int edgeCost = residualGraph.edges.First(x => x.VertexStart == u && x.VertexEnd == v).Weight;
                    if (!visited[v] && edgeCost > 0)
                    {
                        queue.Enqueue(v);
                        parent[v] = u; // Store the path
                        visited[v] = true;
                        if (v == t) return true; // If sink is reached
                    }
                }
            }
            return false; // No augmenting path found
        }

        // Initializes the residual graph for Ford-Fulkerson algorithm
        private void InitializeResidualGraph(int N)
        {
            // Copy original graph to residual graph and add reverse edges with 0 capacity
            residualGraph = graph;
            foreach (var edge in graph.edges.ToArray())
            {
                if (!residualGraph.edges.Any(e => e.VertexStart == edge.VertexEnd && e.VertexEnd == edge.VertexStart))
                {
                    residualGraph.AddEdge(edge.VertexEnd, edge.VertexStart, 0); // Add reverse edge
                }
            }
        }
    }
}
