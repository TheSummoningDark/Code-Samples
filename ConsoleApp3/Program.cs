using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Graph
    {
        Dictionary<string, int> nameToInt;
        Dictionary<int,int> vertexWeight;
        Dictionary<int, HashSet<int>> adjList;


        Graph() {
            nameToInt = new Dictionary<string, int>();
            adjList = new Dictionary<int, HashSet<int>>();
            vertexWeight = new Dictionary<int, int>();
        }

        void AddAVertex(string name, int weight) {
            int index = nameToInt.Count;
            nameToInt.Add(name,index);
            vertexWeight.Add(index, weight);
            
        }

        void AddAEdge(string source, string desitnation) {
            HashSet<int> edges;
            int index;
            int destIndex;
            nameToInt.TryGetValue(desitnation, out destIndex);
            nameToInt.TryGetValue(source, out index);
            if (adjList.TryGetValue(index, out edges))
            {
                edges.Add(destIndex);
            }
            else {
                edges = new HashSet<int>() { destIndex };
                adjList.Add(index, edges);
            }

        }

        void topoSortHelp(int v, bool[] visited, ref Stack<int> stack) {
            visited[v] = true;
            HashSet<int> adjVerticies;
            if (adjList.TryGetValue(v, out adjVerticies)) {
                foreach (int vertex in adjVerticies) {
                    if (!visited[vertex]) {
                        topoSortHelp(vertex, visited,ref stack);
                    }
                }
                    }
            stack.Push(v);
        }

        void FindShortestPath(string name, string destName) {
            int sourceVertex;
            int destVertex;
            nameToInt.TryGetValue(destName,out destVertex);
            nameToInt.TryGetValue(name,out sourceVertex);
            if (destVertex == sourceVertex) {
                Console.WriteLine(0);
                return;
            }
            bool[] visted = new bool[nameToInt.Count];
            int[] distance = new int[nameToInt.Count];
            HashSet<int> adjVerticies;
            int weight;
            Stack<int> verticies = new Stack<int>();

            for (int i = 0; i < nameToInt.Count; i++)
            {
                visted[i] = false;
            }

            for (int i = 0; i < nameToInt.Count; i++)
            {

                if (!visted[i])
                {
                    topoSortHelp(i, visted, ref verticies);
                }
            }

            for (int i = 0; i < nameToInt.Count; i++) {
                distance[i] = int.MaxValue;
            }
            distance[sourceVertex] = 0;

            while (verticies.Count != 0) {

                int u = verticies.Pop();
                if (adjList.TryGetValue(u, out adjVerticies)) {
                    foreach (int vertex in adjVerticies) {
                        vertexWeight.TryGetValue(vertex, out weight);
                        if (distance[u] != int.MaxValue && distance[vertex] > (uint)(distance[u] + weight)) {
                            distance[vertex] = distance[u] + weight;
                        }

                    }
                }
            }
            
            if (distance[destVertex] == int.MaxValue) {
                Console.WriteLine("NO");
            }else
            {
                
                Console.WriteLine(distance[destVertex]); }

        }

        static void Main(string[] args)
        {
            
            int numVerticies;
            int numEdges;
            int numSearches;
            numVerticies = Int32.Parse(Console.ReadLine());
            Graph myGraph = new Graph();

            string[] tokens;
            for (int i = 0; i < numVerticies; i++) {
                tokens = Console.ReadLine().Split(' ');
                myGraph.AddAVertex(tokens[0], int.Parse(tokens[1]));
            }
            numEdges = int.Parse(Console.ReadLine());
            for (int i = 0; i < numEdges; i++) {
                tokens = Console.ReadLine().Split(' ');
                myGraph.AddAEdge(tokens[0], tokens[1]);
            }
            numSearches = int.Parse(Console.ReadLine());
            for (int i = 0; i < numSearches; i++) {
                tokens = Console.ReadLine().Split(' ');
                myGraph.FindShortestPath(tokens[0], tokens[1]);
            }
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

            Console.ReadLine();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
