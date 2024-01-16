namespace Algorithms.Graphs
{
    internal class Tarjan
    {
        private Graph graph;
        private List<List<int>> stronglyConnectedComponents;
        private Stack<int> stack;
        private int[] ids;
        private int[] lows;
        private int id;
        private bool[] onStack;

        public Tarjan(Graph graph)
        {
            this.graph = graph;
            stronglyConnectedComponents = new List<List<int>>();
            stack = new Stack<int>();
            ids = new int[graph.n];
            lows = new int[graph.n];
            onStack = new bool[graph.n];
            id = 0;
        }

        public List<List<int>> FindStronglyConnectedComponents()
        {
            for (int i = 0; i < graph.n; i++)
            {
                if (ids[i] == 0)
                {
                    DFS(i);
                }
            }

            return stronglyConnectedComponents;
        }

        public int FindLowestCommonAncestor(int vertex1, int vertex2)
        {
            // Assuming FindStronglyConnectedComponents() has been called before this method
            int component1 = -1;
            int component2 = -1;

            foreach (var component in stronglyConnectedComponents)
            {
                if (component.Contains(vertex1))
                    component1 = component1 == -1 ? component[0] : component1;
                if (component.Contains(vertex2))
                    component2 = component2 == -1 ? component[0] : component2;
            }

            return component1 == component2 ? component1 : -1;
        }

        private void DFS(int at)
        {
            stack.Push(at);
            onStack[at] = true;
            ids[at] = lows[at] = ++id;

            foreach (var to in graph.vertices[at].neighbors)
            {
                if (ids[to] == 0)
                {
                    DFS(to);
                    lows[at] = Math.Min(lows[at], lows[to]);
                }
                else if (onStack[to])
                {
                    lows[at] = Math.Min(lows[at], ids[to]);
                }
            }

            if (ids[at] == lows[at])
            {
                List<int> component = new List<int>();
                int node;
                do
                {
                    node = stack.Pop();
                    onStack[node] = false;
                    component.Add(node);
                } while (node != at);

                stronglyConnectedComponents.Add(component);
            }
        }
    }
}
