using System;
using System.Linq;

public class BellmanFord
{
    public static void FindShortestPaths(int[,] graph)
    {
        int numVertices = graph.GetLength(0);
        int[] distanceEstimates = new int[numVertices];
        int infinity = int.MaxValue;

        for (int i = 0; i < numVertices; i++)
        {
            distanceEstimates[i] = infinity;
        }

        Console.WriteLine($"Введите номер начальной вершины (от 1 до {numVertices}):");
        int startNodeIndex = int.Parse(Console.ReadLine()) - 1;

        if (startNodeIndex < 0 || startNodeIndex >= numVertices)
        {
            Console.WriteLine("Некорректный номер вершины!");
            return;
        }

        distanceEstimates[startNodeIndex] = 0;

        for (int k = 1; k < numVertices; k++)
        {
            for (int targetNode = 0; targetNode < numVertices; targetNode++)
            {
                if (targetNode != startNodeIndex)
                {
                    for (int sourceNode = 0; sourceNode < numVertices; sourceNode++)
                    {
                        if (distanceEstimates[sourceNode] != infinity && graph[sourceNode, targetNode] != 0)
                        {
                            if (distanceEstimates[targetNode] > distanceEstimates[sourceNode] + graph[sourceNode, targetNode])
                            {
                                distanceEstimates[targetNode] = distanceEstimates[sourceNode] + graph[sourceNode, targetNode];
                            }
                        }
                    }
                }
            }
        }

        for (int destinationNode = 0; destinationNode < numVertices; destinationNode++)
        {
            if (destinationNode != startNodeIndex)
            {
                string shortestPath = (distanceEstimates[destinationNode] == infinity) ? "бесконечность" : distanceEstimates[destinationNode].ToString();
                Console.WriteLine($"Минимальное расстояние от вершины {startNodeIndex + 1} до вершины {destinationNode + 1}: {shortestPath}");
            }
        }
    }

    public static void Main(string[] args)
    {
        int[,] exampleGraph = {
            { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
            { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
            { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
            { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
            { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
            { 0, 0, 4, 14, 10, 0, 2, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0, 1, 6 },
            { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
            { 0, 0, 2, 0, 0, 0, 6, 7, 0 }
        };

        FindShortestPaths(exampleGraph);
    }
}