using System;
using System.Collections.Generic;
using System.Linq;

public class DijkstraAlgorithm
{
    public static int FindShortestDistance(int[,] graph, int startNode, int endNode)
    {
        int numVertices = graph.GetLength(0);
        List<int> nodes = Enumerable.Range(0, numVertices).ToList();

        if (!nodes.Contains(startNode) || !nodes.Contains(endNode))
        {
            throw new ArgumentException("Некорректные номера вершин.");
        }

        int[] shortestDistances = Enumerable.Repeat(int.MaxValue, numVertices).ToArray();
        shortestDistances[startNode] = 0;
        HashSet<int> unvisitedNodes = new HashSet<int>(nodes);

        Dictionary<int, int> previousNodes = new Dictionary<int, int>();

        while (unvisitedNodes.Count > 0)
        {
            int currentNode = unvisitedNodes.OrderBy(node => shortestDistances[node]).First();

            if (currentNode == endNode)
            {
                break;
            }

            unvisitedNodes.Remove(currentNode);

            for (int neighbor = 0; neighbor < numVertices; neighbor++)
            {
                int weight = graph[currentNode, neighbor];

                if (weight > 0)
                {
                    int potentialDistance = shortestDistances[currentNode] + weight;
                    if (potentialDistance < shortestDistances[neighbor])
                    {
                        shortestDistances[neighbor] = potentialDistance;
                        previousNodes[neighbor] = currentNode;
                    }
                }
            }
        }

        return shortestDistances[endNode];
    }

    public static void Main(string[] args)
    {
        int[,] graphMatrix = {
            {0, 4, 0, 0, 0, 0, 0, 8, 0},
            {4, 0, 8, 0, 0, 0, 0, 11, 0},
            {0, 8, 0, 7, 0, 4, 0, 0, 2},
            {0, 0, 7, 0, 9, 14, 0, 0, 0},
            {0, 0, 0, 9, 0, 10, 0, 0, 0},
            {0, 0, 4, 14, 10, 0, 2, 0, 0},
            {0, 0, 0, 0, 0, 2, 0, 1, 6},
            {8, 11, 0, 0, 0, 0, 1, 0, 7},
            {0, 0, 2, 0, 0, 0, 6, 7, 0}
        };

        int numVertices = graphMatrix.GetLength(0);

        Console.WriteLine($"Введите номер вершины от 1 до {numVertices}, с которой хотите начать путь:");
        int startVertex = int.Parse(Console.ReadLine()) - 1;

        Console.WriteLine($"Введите номер вершины от 1 до {numVertices}, в которой хотите закончить путь:");
        int endVertex = int.Parse(Console.ReadLine()) - 1;

        try
        {
            int shortestPathLength = FindShortestDistance(graphMatrix, startVertex, endVertex);
            Console.WriteLine($"Наименьшее расстояние: {shortestPathLength}");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Произошла ошибка: {e.Message}");
        }

        Console.ReadKey();
    }
}