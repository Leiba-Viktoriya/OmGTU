using System;
using System.Collections.Generic;
using System.Linq;

public class GraphAnalyzer
{
    static void TraverseGraph(int startNode, int[][] adjacencyMatrix, HashSet<int> visitedNodes)
    {
        visitedNodes.Add(startNode);
        for (int neighbor = 0; neighbor < adjacencyMatrix.Length; neighbor++)
        {
            if (adjacencyMatrix[startNode][neighbor] != 0 && !visitedNodes.Contains(neighbor))
            {
                TraverseGraph(neighbor, adjacencyMatrix, visitedNodes);
            }
        }
    }

    public static List<(int, int)> FindBridges(int[][] graphMatrix)
    {
        int numNodes = graphMatrix.Length;
        List<(int, int)> edgeList = new List<(int, int)>();
        for (int i = 0; i < numNodes; i++)
        {
            for (int j = i + 1; j < numNodes; j++)
            {
                if (graphMatrix[i][j] != 0)
                {
                    edgeList.Add((i, j));
                }
            }
        }
        int[] nodeComponents = Enumerable.Range(0, numNodes).ToArray();
        List<(int, int)> spanningTree = new List<(int, int)>();
        foreach (var edge in edgeList)
        {
            int startNode = edge.Item1;
            int endNode = edge.Item2;
            if (nodeComponents[startNode] != nodeComponents[endNode])
            {
                spanningTree.Add(edge);
                int componentToMerge = Math.Min(nodeComponents[startNode], nodeComponents[endNode]);
                int componentToReplace = Math.Max(nodeComponents[startNode], nodeComponents[endNode]);
                for (int i = 0; i < numNodes; i++)
                {
                    if (nodeComponents[i] == componentToReplace)
                    {
                        nodeComponents[i] = componentToMerge;
                    }
                }
            }
        }
        List<(int, int)> bridges = new List<(int, int)>();
        foreach (var edge in spanningTree)
        {
            int node1 = edge.Item1;
            int node2 = edge.Item2;
            int edgeWeight = graphMatrix[node1][node2];
            graphMatrix[node1][node2] = graphMatrix[node2][node1] = 0;
            HashSet<int> visitedNodes = new HashSet<int>();
            List<HashSet<int>> graphComponents = new List<HashSet<int>>();
            for (int startNode = 0; startNode < numNodes; startNode++)
            {
                if (!visitedNodes.Contains(startNode))
                {
                    HashSet<int> currentComponent = new HashSet<int>();
                    TraverseGraph(startNode, graphMatrix, currentComponent);
                    graphComponents.Add(currentComponent);
                    visitedNodes.UnionWith(currentComponent);
                }
            }
            if (graphComponents.Count > 1)
            {
                bridges.Add(edge);
            }
            graphMatrix[node1][node2] = graphMatrix[node2][node1] = edgeWeight;
        }
        return bridges;
    }

    public static void Main(string[] args)
    {
        int[][] graphMatrix = new int[][] {
            new int[] { 0, 1, 0, 0, 0 },
            new int[] { 1, 0, 1, 0, 0 },
            new int[] { 0, 1, 0, 1, 0 },
            new int[] { 0, 0, 1, 0, 1 },
            new int[] { 0, 0, 0, 1, 0 }
        };

        List<(int, int)> foundBridges = FindBridges(graphMatrix);
        if (foundBridges.Count > 0)
        {
            Console.WriteLine("Мостами являются ребра:");
            foreach (var edge in foundBridges)
            {
                Console.WriteLine($"({edge.Item1 + 1}, {edge.Item2 + 1})");
            }
        }
        else
        {
            Console.WriteLine("Мостов нет!");
        }
    }
}