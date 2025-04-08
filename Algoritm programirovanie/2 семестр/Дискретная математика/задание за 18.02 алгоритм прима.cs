using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void ExploreComponent(int startNode, int[,] adjacencyMatrix, List<int> visitedNodes, HashSet<int> unvisitedNodes)
    {
        visitedNodes.Add(startNode);
        for (int neighborNode = 0; neighborNode < adjacencyMatrix.GetLength(0); neighborNode++)
        {
            if (adjacencyMatrix[startNode, neighborNode] != 0 && !visitedNodes.Contains(neighborNode))
            {
                unvisitedNodes.Remove(neighborNode);
                ExploreComponent(neighborNode, adjacencyMatrix, visitedNodes, unvisitedNodes);
            }
        }
    }

    public static void Main(string[] args)
    {
        int[,] graphMatrix = {
            { 0, 2, 0, 6, 0 },
            { 2, 0, 3, 8, 5 },
            { 0, 3, 0, 0, 7 },
            { 6, 8, 0, 0, 9 },
            { 0, 5, 7, 9, 0 }
        };

        HashSet<int> unvisitedNodes = new HashSet<int>(Enumerable.Range(0, graphMatrix.GetLength(0)));
        List<List<int>> allComponents = new List<List<int>>();

        while (unvisitedNodes.Count > 0)
        {
            List<int> componentNodes = new List<int>();
            int startNode = unvisitedNodes.First();
            ExploreComponent(startNode, graphMatrix, componentNodes, unvisitedNodes);
            unvisitedNodes.Remove(startNode);
            allComponents.Add(componentNodes);
        }

        int numComponents = allComponents.Count;
        if (numComponents > 1)
        {
            Console.WriteLine("Граф не связный!");
        }
        else
        {
            List<int> allNodes = Enumerable.Range(0, graphMatrix.GetLength(0)).ToList();
            int startNodeIndex = 0;
            List<int> spanningTreeNodes = new List<int> { allNodes[startNodeIndex] };
            int totalWeight = 0;

            while (spanningTreeNodes.Count != graphMatrix.GetLength(0))
            {
                int minEdgeWeight = int.MaxValue;
                int edgeToAdd = -1;

                for (int nodeIndex = 0; nodeIndex < spanningTreeNodes.Count; nodeIndex++)
                {
                    int currentNode = spanningTreeNodes[nodeIndex];
                    List<int> possibleEdges = new List<int>();

                    for (int neighbor = 0; neighbor < graphMatrix.GetLength(0); neighbor++)
                    {
                        if (graphMatrix[currentNode, neighbor] != 0 && !spanningTreeNodes.Contains(neighbor))
                        {
                            possibleEdges.Add(graphMatrix[currentNode, neighbor]);
                        }
                    }

                    if (possibleEdges.Count > 0)
                    {
                        int minWeightFromNode = possibleEdges.Min();
                        if (minWeightFromNode < minEdgeWeight)
                        {
                            minEdgeWeight = minWeightFromNode;
                            for (int neighbor = 0; neighbor < graphMatrix.GetLength(0); neighbor++)
                            {
                                if (!spanningTreeNodes.Contains(neighbor) && graphMatrix[currentNode, neighbor] == minWeightFromNode)
                                {
                                    edgeToAdd = neighbor;
                                    break;
                                }
                            }
                        }
                    }
                }

                totalWeight += minEdgeWeight;
                spanningTreeNodes.Add(edgeToAdd);
            }

            Console.WriteLine($"Минимальный вес: {totalWeight}");
        }
    }
}