using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class Graph
    {
        
        Dictionary<string,int> indexLookup;
        Dictionary<int, string> nameLookup;
        List<List<int>> adjList;


        Graph() {
            adjList = new List<List<int>>();
            indexLookup = new Dictionary<string, int>();
            nameLookup = new Dictionary<int, string>();

        }

        void addVertex(string name) {
            adjList.Add(new List<int>());
            indexLookup.Add(name, indexLookup.Count);
            nameLookup.Add(nameLookup.Count, name);

        }

        void addConnection(string first, string second) {
            HashSet<string> edge;
            int indexFirst;
            int indexSecond;
            indexLookup.TryGetValue(first, out indexFirst);
            indexLookup.TryGetValue(second, out indexSecond);
            adjList[indexFirst].Add(indexSecond);
            adjList[indexSecond].Add(indexFirst);

        }
        string findStudentRumorGroups(string source) {
            Dictionary<int, string> rumorGroups = new Dictionary<int, string>();
            SortedDictionary<int, SortedSet<string>> friendDistance = new SortedDictionary<int, SortedSet<string>>();
            Queue<int> bfsQueue = new Queue<int>();
            StringBuilder sb = new StringBuilder();
            SortedSet<string> friends;
            int personIndex;
            string person;
            int V = adjList.Count;
            int[] distances = new int[V];
            bool[] visited = new bool[V];
            for (int i = 0; i < V; i++) {
                distances[i] = int.MaxValue;
                visited[i] = false;
            }
            indexLookup.TryGetValue(source, out personIndex);
            distances[personIndex] = 0;
           
            visited[personIndex] = true;
            bfsQueue.Enqueue(personIndex);

            while (bfsQueue.Count != 0) {
                int u = bfsQueue.Dequeue();
                for (int i = 0; i < adjList[u].Count; i++) {
                    if (visited[adjList[u][i]] == false) {
                        visited[adjList[u][i]] = true;
                        distances[adjList[u][i]] = distances[u] + 1;
                        bfsQueue.Enqueue(adjList[u][i]);


                    }

                }

            }
            for (int i = 0; i < distances.Length; i++) {

                if (friendDistance.TryGetValue(distances[i], out friends))
                {
                    nameLookup.TryGetValue(i, out person);
                    friends.Add(person);

                }
                else
                {
                    nameLookup.TryGetValue(i, out person);
                    friendDistance.Add(distances[i], new SortedSet<string> { person });
                }

            }
           
                foreach(SortedSet<string> people in friendDistance.Values) {
                foreach (string friend in people) {

                    sb.Append(friend+" ");
                }

            }


            return (sb.ToString());


        }
        static void Main(string[] args)
        {
            int numStudents;
            int numPairs;
            int numReports;
            Graph G = new Graph();
            string[] tokens;
            numStudents = int.Parse(Console.ReadLine());
            for (int i = 0; i < numStudents; i++) {
                G.addVertex(Console.ReadLine());
            }
            numPairs = int.Parse(Console.ReadLine());
            for (int i = 0; i < numPairs; i++) {
                tokens = Console.ReadLine().Split(' ');
                G.addConnection(tokens[0], tokens[1]);
            }
            numReports = int.Parse(Console.ReadLine());
                
             for (int i = 0; i< numReports; i++) {
                Console.WriteLine(G.findStudentRumorGroups(Console.ReadLine()));

            }

            Console.ReadKey();


        }
    }
}
