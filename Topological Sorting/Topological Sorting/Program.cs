using System;
using System.Collections.Generic;
//https://www.interviewcake.com/concept/java/topological-sort
namespace Topological_Sorting
{
    class Program
    {

        static List<string> topologicalSort(Dictionary<string, List<string>> graph)
        {
            List<string> topologicalOrdering = new List<string>();
            
            
            Dictionary<string, int> inDegreeMapping = new Dictionary<string, int>();
            foreach(string gk in graph.Keys) { inDegreeMapping.Add(gk, 0); }

            List<string> templs;
            int tempi;
            foreach(string gk in graph.Keys)
            {
                templs = graph[gk];
                foreach(string neighbor in templs)
                {
                    tempi = 0;
                    if (inDegreeMapping.ContainsKey(neighbor))
                    {
                        tempi = inDegreeMapping[neighbor];
                        inDegreeMapping.Remove(neighbor);
                    }
                    tempi++;
                    inDegreeMapping.Add(neighbor, tempi);
                }
            }

            //collections of nodes with no incoming edges
            Stack<string> noIncoming = new Stack<string>();

            foreach (string gk in graph.Keys)
            {
                if (inDegreeMapping[gk] == 0)
                {
                    noIncoming.Push(gk);
                }
            }

            string tempNode;
            while(noIncoming.Count > 0)
            {
                tempNode = noIncoming.Pop();
                topologicalOrdering.Add(tempNode);

                templs = graph[tempNode];
                foreach(string neighbor in templs)
                {
                    tempi = inDegreeMapping[neighbor];
                    tempi--;
                    inDegreeMapping.Remove(neighbor);
                    inDegreeMapping.Add(neighbor, tempi);
                    if (tempi == 0) { noIncoming.Push(neighbor); }
                }
            }

            if(topologicalOrdering.Count == graph.Keys.Count)
            {
                return topologicalOrdering;
            }
            else
            {
                throw new Exception("Graph has cycle! No topological ordering exists.");
            }

            return new List<string>();
        }
        static void Main(string[] args)
        {
            Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();
            graph.Add("a", new List<string> { "c", "d" });
            graph.Add("b", new List<string> { "c", "e" });
            graph.Add("c", new List<string> { "d" });
            graph.Add("d", new List<string>());
            graph.Add("e", new List<string> { "c", "a" });
            List<string> result = topologicalSort(graph);
            for(int i = 0; i < result.Count; i++) { Console.Write(result[i] + " "); }
            Console.WriteLine();
        }
    }
}
