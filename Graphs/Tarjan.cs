namespace Algorithms.Graphs
{
    internal class TarjanSCC
    {
        // The graph on which Tarjan's algorithm is to be applied.
        private Graph graph;

        // Array 'low' keeps track of the lowest vertex id reachable from each vertex.
        private int[] low;

        // Array 'ids' stores the ids of each vertex, which are assigned during the DFS.
        // It's used to check if a vertex has already been visited. In other words, it stores discovery times of visited vertices.
        private int[] ids;

        // Array 'onStack' is a boolean array to check if a vertex is in the current stack.
        private bool[] onStack;

        // Stack to keep track of the vertices in the current DFS path.
        private Stack<int> stack;

        // List to store all the strongly connected components found.
        private List<List<int>> stronglyConnectedComponents;

        // Variable 'id' is used to assign unique ids to each vertex during DFS.
        private int id;

        public TarjanSCC(Graph graph)
        {
            this.graph = graph;
            low = new int[graph.nodes.Count];
            ids = new int[graph.nodes.Count];
            onStack = new bool[graph.nodes.Count];
            stack = new Stack<int>();
            stronglyConnectedComponents = new List<List<int>>();
            id = 0;

            // Initialize all ids to -1 indicating that no vertex has been visited yet.
            Array.Fill(ids, -1);

            // Start DFS from each vertex.
            for (int i = 0; i < graph.nodes.Count; i++)
            {
                if (ids[i] == -1)
                {
                    DFS(i);
                }
            }
        }

        private void DFS(int at)
        {
            // Start DFS from vertex 'at'.
            stack.Push(at);
            onStack[at] = true;
            ids[at] = low[at] = id++;

            // Visit all neighbors of 'at'.
            foreach (int to in graph.vertices[at].neighbors)
            {
                // If 'to' has not been visited, perform DFS on 'to'.
                if (ids[to] == -1) DFS(to);

                // Update the low-link value of 'at' if 'to' is in the current DFS stack.
                if (onStack[to]) low[at] = Math.Min(low[at], low[to]);
            }

            // If 'at' is a root node, pop all nodes from the stack to form an SCC.
            if (ids[at] == low[at])
            {
                var component = new List<int>();

                for (int node = stack.Pop(); ; node = stack.Pop())
                {
                    onStack[node] = false;
                    component.Add(node);
                    if (node == at) break;
                }

                stronglyConnectedComponents.Add(component);
            }
        }

        // Method to get the list of all strongly connected components.
        public List<List<int>> GetStronglyConnectedComponents()
        {
            return stronglyConnectedComponents;
        }
    }
}
