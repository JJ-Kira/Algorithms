using System.Text;

namespace Algorithms.Graphs
{
    internal class Graph
    {
        // Variables representing various graph components and properties
        public List<Edge> edges = new List<Edge>(); // List of edges
        public List<int> nodes = new List<int>(); // List of vertices
        public List<Vertex> vertices = new List<Vertex>(); // List of Tarjan vertices
        public bool isDirected = true; // Flag indicating if the graph is directed
        public int n; // Count of nodes
        public int mode = 1; // Mode for constructing edges

        // 2CNF
        public List<Edge> twoCNFEdges = new List<Edge>(); // List of edges
        public List<Vertex> twoCNFVertieces = new List<Vertex>(); // List of vertices as class objects
        public List<int> twoCNFNodes = new List<int>(); // List of vertices as a list
        public StringBuilder formula = new StringBuilder(); // String builder for the formula
        public List<Tuple<int, int>> logicFormula = new List<Tuple<int, int>>(); // List of alternatives for the 2CNF expression

        // Constructor to initialize the graph from a file
        public Graph(string path, bool isDirected)
        {
            this.isDirected = isDirected;
            readFile(path);
        }

        public Graph(string path, bool isDirected, int mode)
        {
            this.isDirected = isDirected;
            this.mode = mode;
            readFile(path);
        }

        public Graph(string path)
        {
            ReadFile2CNF(path);
        }

        public void TransposeGraph()
        {

            List<Vertex> verticesTranspose = new List<Vertex>();
            List<int> nodesTranspose = new List<int>();
            foreach (var vertex in vertices)
            {
                foreach (var neighbour in vertex.neighbors)
                {
                    Vertex v = new Vertex(neighbour);

                    if (!nodesTranspose.Contains(neighbour))
                    {
                        nodesTranspose.Add(neighbour);
                        verticesTranspose.Add(v);
                    }
                    if (!nodesTranspose.Contains(vertex.num)) // adds empty vertices
                    {
                        Vertex v1 = new Vertex(vertex.num);
                        nodesTranspose.Add(vertex.num);
                        verticesTranspose.Add(v1);
                    }
                    verticesTranspose.First(x => x.num == v.num).AddNeighborToVertex(vertex.num);
                }
            }

            vertices = new List<Vertex>(verticesTranspose);
        }

        public void PrintGraph()
        {
            foreach (var edge in edges)
            {
                Console.WriteLine($"{edge.VertexStart} -> {edge.VertexEnd} : {edge.Weight}");
            }

            Console.WriteLine();

            Console.WriteLine($"Nodes number: {nodes.Count}");
        }

        // Display the logical formula representing the graph (for 2CNF)
        public void PrintLogicalFormula()
        {
            int len = formula.Length;
            formula.Replace("&&", "", len - 3, 2);
            Console.WriteLine("Logical Formula:");
            Console.WriteLine(formula.ToString() + "\n");
        }

        private void ReadFile2CNF(string path)
        {
            string s;
            StreamReader sr = File.OpenText(path);

            n = int.Parse(sr.ReadLine());

            while ((s = sr.ReadLine()) != null)
            {
                string[] subs = s.Split(' ');
                int a = int.Parse(subs[0]);
                int b = int.Parse(subs[1]);

                logicFormula.Add(new Tuple<int, int>(a, b));
                formula.Append($"({a} || {b}) && ");
                AddEdge2CNF(-a, b, 0);
                AddEdge2CNF(-b, a, 0);

                // x => x     a -x => -x+n = n-(-x)
                if (a > 0 && b > 0)
                {
                    AddEdge(a + n, b, 0);
                    AddEdge(b + n, a, 0);
                }
                else if (a > 0 && b < 0)
                {
                    AddEdge(a + n, n - b, 0);
                    AddEdge(-b, a, 0);
                }
                else if (a < 0 && b > 0)
                {
                    AddEdge(-a, b, 0);
                    AddEdge(b, n - a, 0);
                }
                else
                {
                    AddEdge(-a, n - b, 0);
                    AddEdge(-b, n - a, 0);
                }
            }
            sr.Close();
            AddNodes();
            AddNeighbors();
            AddNodes2CNF();
            AddNeighbours2CNF();
        }


        private void readFile(string path)
        {
            string s;
            StreamReader sr = File.OpenText(path);

            while ((s = sr.ReadLine()) != null)
            {
                string[] subs = s.Split(' ');
                if (isDirected)
                    AddEdge(int.Parse(subs[0]), int.Parse(subs[1]), int.Parse(subs[2]));
                else
                {
                    AddEdge(int.Parse(subs[0]), int.Parse(subs[1]), int.Parse(subs[2]));
                    if (mode == 1)
                        AddEdge(int.Parse(subs[1]), int.Parse(subs[0]), int.Parse(subs[2]));
                    else if (mode == 2)
                        AddEdge(int.Parse(subs[1]), int.Parse(subs[0]), 0);
                }
            }
            sr.Close();
            AddNodes();
            AddNeighbors();
        }

        private void AddNodes()
        {
            foreach (var item in edges)
            {
                if (!nodes.Contains(item.VertexStart))
                    nodes.Add(item.VertexStart);
                if (!nodes.Contains(item.VertexEnd))
                    nodes.Add(item.VertexEnd);
            }
        }

        private void AddNeighbors()
        {

            foreach (var n in nodes)
            {
                Vertex v = new Vertex(n);
                vertices.Add(v);

                var sortedVertex = edges.Where(x => x.VertexStart == n).Select(x => x.VertexEnd);

                foreach (var u in sortedVertex)
                    v.AddNeighborToVertex(u);
            }
        }

        public void AddEdge(int vertex1, int vertex2, int weight)
        {
            Edge edge = new Edge(vertex1, vertex2, weight);

            if (!edges.Contains(edge))
                edges.Add(edge);
        }


        private void AddNodes2CNF()
        {
            foreach (var item in twoCNFEdges)
            {
                if (!twoCNFNodes.Contains(item.VertexStart))
                    twoCNFNodes.Add(item.VertexStart);
                if (!twoCNFNodes.Contains(item.VertexEnd))
                    twoCNFNodes.Add(item.VertexEnd);
            }
        }

        private void AddNeighbours2CNF()
        {
            foreach (var n in twoCNFNodes)
            {
                Vertex v = new Vertex(n);
                twoCNFVertieces.Add(v);

                var sortedVertex2CNF = twoCNFEdges.Where(x => x.VertexStart == n).Select(x => x.VertexEnd);

                foreach (var u in sortedVertex2CNF)
                    v.AddNeighborToVertex(u);
            }
        }

        public void AddEdge2CNF(int vertex1, int vertex2, int weight)
        {
            Edge edge = new Edge(vertex1, vertex2, weight);

            if (!twoCNFEdges.Contains(edge))
                twoCNFEdges.Add(edge);
        }

        public void RemoveEdge(int vertex1, int vertex2)
        {
            var edge = edges.Single(x => x.VertexStart == vertex1 && x.VertexEnd == vertex2);

            if (edges.Contains(edge))
                edges.Remove(edge);
        }
    }
    internal class Vertex
    {
        // Represents a vertex in the graph
        public bool state; // State of the vertex
        public int sccID; // Strongly Connected Component (SCC) identifier
        public int num; // Number assigned to the vertex
        public List<int> neighbors = new List<int>(); // List of neighboring vertices

        public int h; // Height of the vertex
        public int excessFlow; // Excess flow at the vertex

        public Vertex(int number) => this.num = number;

        public Vertex(int number, int excessFlow)
        {
            this.num = number;
            this.excessFlow = excessFlow;
            this.h = 0; // Default height is 0
        }

        public void AddNeighborToVertex(int u)
        {
            neighbors.Add(u);
        }

        public void PrintNeighbors()
        {
            Console.Write($"{num}: ");
            foreach (int i in neighbors)
                Console.Write($"{i} ");
            Console.WriteLine();
        }
    }

    internal class Edge
    {
        // Represents an edge in the graph
        public int VertexStart; // Start vertex of the edge
        public int VertexEnd; // End vertex of the edge
        public int Weight; // Weight of the edge
        public int Flow; // Flow of the edge (if applicable)

        public Edge(int vertex1, int vertex2, int weight)
        {
            VertexEnd = vertex2;
            VertexStart = vertex1;
            Weight = weight;
            Flow = 0;
        }
    }
}
