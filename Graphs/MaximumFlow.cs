namespace Algorithms.Graphs
{
    internal class MaximumFlow
    {
        private Graph graph; // Reference to the original graph
        private List<Vertex> vertices; // List to store vertices with their properties for the algorithm
        private List<Edge> edges; // List of edges in the graph

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

        // Calculates the maximum flow from source to sink
        public int CalculateMaximumFlow(int source, int sink)
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
    }
}
