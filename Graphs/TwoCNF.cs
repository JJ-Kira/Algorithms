namespace Algorithms.Graphs
{
    internal class TwoCNF
    {
        private Graph graph;
        private Graph graphSpare;

        private int n; // Number of variables
        private int vertCount; // Number of vertices in the graph

        private Stack<int> processed; // Stack to track processed vertices
        private int[] visited; // Array to mark visited vertices during DFS
        private int[] scc; // Array to store Strongly Connected Component IDs


        public TwoCNF(Graph graph)
        {
            this.graph = graph;
            graphSpare = graph; // Copy of the original graph
            vertCount = graph.vertices.Count;
            visited = new int[vertCount + 1];
            scc = new int[vertCount + 1];
            processed = new Stack<int>(vertCount);
            n = graph.n; // Number of variables in the 2-CNF expression
        }

        // Depth First Search (DFS) algorithm
        void DFS(int u, int k, bool flag)
        {
            var vertices = graph.vertices.First(x => x.num == u).neighbors;
            visited[u] = k;
            foreach (var v in vertices)
                if (visited[v] == 0) DFS(v, k, flag);
            if (flag)
                processed.Push(u);
            else
            {
                scc[u] = k;
                graph.vertices.First(x => x.num == u).sccID = k;
            }
        }

        // Print Strongly Connected Components
        public void Print2CNF()
        {
            var vertList = graphSpare.vertices.OrderBy(x => x.num).ToList();

            Console.WriteLine("SSS:");
            for (int i = 1; i <= vertCount; i++)
                if (vertList[i - 1].num > n)
                {
                    int p = vertList[i - 1].num - n;
                    Console.WriteLine(-p + ":" + scc[i]);
                }
                else
                    Console.WriteLine(vertList[i - 1].num + ":" + scc[i]);
        }

        // Checks Logic Formula based on Strongly Connected Components
        public void CheckLogicFormula()
        {
            var vertList = graphSpare.vertices.OrderBy(x => x.num).ToList();

            int k = scc.Min() + 1;

            while (k <= scc.Max())
            {
                var sccList = graph.vertices.FindAll(x => x.sccID == k);
                foreach (var vertex in sccList)
                {
                    if (vertex.state == false)
                    {
                        vertex.state = true;

                        if (vertex.num <= n)
                            graphSpare.vertices.First(x => x.num == vertex.num + n).state = false;
                        else
                            graphSpare.vertices.First(x => x.num == vertex.num - n).state = false;
                    }
                }
                k++;
            }
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Logic formula check");

            foreach (var vert in vertList)
            {
                if (vert.num > n)
                    Console.WriteLine($"{-(vert.num - n)}:{vert.state} scc:{vert.sccID}");
                else
                    Console.WriteLine($"{vert.num}:{vert.state} scc:{vert.sccID}");
            }

            bool result = true;
            bool val1, val2;
            foreach (var form in graphSpare.logicFormula)
            {
                // Logic to evaluate the 2-CNF expression based on SCC
                if (form.Item1 < 0)
                    val1 = graphSpare.vertices.First(x => x.num == -form.Item1 + n).state;
                else
                    val1 = graphSpare.vertices.First(x => x.num == form.Item1).state;
                if (form.Item2 < 0)
                    val2 = graphSpare.vertices.First(x => x.num == -form.Item2 + n).state;
                else
                    val2 = graphSpare.vertices.First(x => x.num == form.Item2).state;

                result &= (val1 || val2);
            }
            graph.PrintLogicalFormula();
            Console.WriteLine("result is " + result);
        }

        // Algorithm to verify 2-CNF expression satisfiability using SCC
        public bool Algorithm2CNF()
        {
            // Phase 1: DFS on the graph
            for (int u = 1; u <= vertCount; u++)
                if (visited[u] == 0)
                    DFS(u, 1, true);

            graph.TransposeGraph(); // Transpose the graph

            // Phase 2: DFS on the transposed graph
            visited = new int[vertCount + 1];
            int k = 0;
            while (processed.Count != 0)
            {
                int u = processed.Peek();
                processed.Pop();
                if (visited[u] == 0)
                {
                    DFS(u, ++k, false);
                }
            }

            // Check for contradictions in the SCC IDs
            for (int i = 1; i <= n; i++)
            {
                if (scc[i] == scc[i + n]) // If a variable and its negation share the same SCC ID
                    return false; // The 2-CNF expression is not satisfiable
            }

            return true; // The 2-CNF expression is satisfiable
        }
    }
}
