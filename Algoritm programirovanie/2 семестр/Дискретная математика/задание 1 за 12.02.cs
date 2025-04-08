using System;
using System.Collections.Generic;
using System.Linq;

public class ConnectedComponents
{
    public static List<List<int>> FindConnectedComponents(List<List<string>> adjacencyMatrix)
    {
        int numNodes = adjacencyMatrix.Count;
        HashSet<int> unvisitedNodes = new HashSet<int>(Enumerable.Range(0, numNodes));
        List<List<int>> connectedComponents = new List<List<int>>();

        while (unvisitedNodes.Count > 0)
        {
            int startNode = unvisitedNodes.First();
            HashSet<int> component = new HashSet<int>();
            DiscoverComponent(startNode, adjacencyMatrix, component);
            connectedComponents.Add(component.ToList());
            unvisitedNodes.ExceptWith(component);
        }

        return connectedComponents;
    }

    private static void DiscoverComponent(int nodeIndex, List<List<string>> adjacencyMatrix, HashSet<int> visitedNodes)
    {
        visitedNodes.Add(nodeIndex);
        for (int neighborIndex = 0; neighborIndex < adjacencyMatrix.Count; neighborIndex++)
        {
            if (adjacencyMatrix[nodeIndex][neighborIndex] == "1" && !visitedNodes.Contains(neighborIndex))
            {
                DiscoverComponent(neighborIndex, adjacencyMatrix, visitedNodes);
            }
        }
    }

    public static void Main(string[] args)
    {
        List<List<string>> adjacencyMatrix = new List<List<string>>
        {
            new List<string> { "0", "1", "0", "0" },
            new List<string> { "1", "0", "1", "0" },
            new List<string> { "0", "1", "0", "1" },
            new List<string> { "0", "0", "1", "0" }
        };

        List<List<int>> components = FindConnectedComponents(adjacencyMatrix);

        Console.WriteLine($"Количество компонент связности графа: {components.Count}");
    }
}