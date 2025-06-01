using System;
using System.Collections.Generic;
using System.Linq;

public class FordFulkerson
{
    static void FindConnectedComponents(int node, List<int> visitedNodes, List<int> remainingNodes, int[][] capacityMatrix)
    {
        if (visitedNodes.Contains(node))
        {
            return;
        }

        visitedNodes.Add(node);

        if (remainingNodes.Contains(node))
        {
            remainingNodes.Remove(node);
        }

        for (int i = 0; i < capacityMatrix.Length; i++)
        {
            if (capacityMatrix[node][i] > 0 && !visitedNodes.Contains(i))
            {
                FindConnectedComponents(i, visitedNodes, remainingNodes, capacityMatrix);
            }
        }
    }

    static List<int> FindPath(int[][] capacityMatrix)
    {
        int numberOfNodes = capacityMatrix.Length;
        int[] parent = new int[numberOfNodes];
        bool[] visited = new bool[numberOfNodes];
        Queue<int> queue = new Queue<int>();

        for (int i = 0; i < numberOfNodes; i++)
        {
            parent[i] = -1;
            visited[i] = false;
        }

        queue.Enqueue(0);
        visited[0] = true;

        while (queue.Count > 0)
        {
            int currentNode = queue.Dequeue();

            if (currentNode == numberOfNodes - 1)
            {
                List<int> path = new List<int>();
                int current = currentNode;

                while (current != -1)
                {
                    path.Add(current);
                    current = parent[current];
                }

                path.Reverse();
                return path;
            }

            for (int neighbor = 0; neighbor < numberOfNodes; neighbor++)
            {
                if (capacityMatrix[currentNode][neighbor] > 0 && !visited[neighbor])
                {
                    queue.Enqueue(neighbor);
                    visited[neighbor] = true;
                    parent[neighbor] = currentNode;
                }
            }
        }

        return null;
    }

    static int CalculateMaxFlow(int[][] capacityMatrix)
    {
        int numberOfNodes = capacityMatrix.Length;
        int maxFlow = 0;
        int[][] residualCapacityMatrix = capacityMatrix.Select(row => row.ToArray()).ToArray();

        while (true)
        {
            List<int> path = FindPath(residualCapacityMatrix);

            if (path == null)
            {
                break;
            }

            int pathFlow = int.MaxValue;
            for (int i = 0; i < path.Count - 1; i++)
            {
                int u = path[i];
                int v = path[i + 1];
                pathFlow = Math.Min(pathFlow, residualCapacityMatrix[u][v]);
            }

            maxFlow += pathFlow;

            for (int i = 0; i < path.Count - 1; i++)
            {
                int u = path[i];
                int v = path[i + 1];
                residualCapacityMatrix[u][v] -= pathFlow;
                residualCapacityMatrix[v][u] += pathFlow;
            }

            List<int> allNodes = Enumerable.Range(0, numberOfNodes).ToList();
            while (allNodes.Count > 0)
            {
                List<int> visitedNodes = new List<int>();
                FindConnectedComponents(allNodes[0], visitedNodes, allNodes, residualCapacityMatrix);
            }
        }

        return maxFlow;
    }

    public static void Main(string[] args)
    {
        int[][] capacityMatrix = new int[][] {
            new int[] { 0, 16, 13, 0, 0, 0 },
            new int[] { 0, 0, 10, 12, 0, 0 },
            new int[] { 0, 4, 0, 0, 14, 0 },
            new int[] { 0, 0, 9, 0, 0, 20 },
            new int[] { 0, 0, 0, 7, 0, 4 },
            new int[] { 0, 0, 0, 0, 0, 0 }
        };

        int maxFlow = CalculateMaxFlow(capacityMatrix);

        Console.WriteLine($"Максимальный поток: {maxFlow}");
        Console.ReadKey();
    }
}