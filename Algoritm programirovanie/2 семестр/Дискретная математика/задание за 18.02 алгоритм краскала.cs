using System;
using System.Collections.Generic;
using System.Linq;

public class KruskalAlgorithm
{
    public static void Main(string[] args)
    {
        int[,] exampleMatrix = {
            { 0, 2, 0, 6, 0 },
            { 2, 0, 3, 8, 5 },
            { 0, 3, 0, 0, 7 },
            { 6, 8, 0, 0, 9 },
            { 0, 5, 7, 9, 0 }
        };

        Kruskal(exampleMatrix);
    }

    public static void Kruskal(int[,] adjacencyMatrix)
    {
        int numVertices = adjacencyMatrix.GetLength(0);
        bool[] visited = new bool[numVertices];
        DFS(0, adjacencyMatrix, visited);

        if (!visited.All(v => v))
        {
            Console.WriteLine("Граф не связный!");
            return;
        }

        List<(int u, int v, int weight)> edgeList = new List<(int u, int v, int weight)>();
        for (int i = 0; i < numVertices; i++)
        {
            for (int j = i + 1; j < numVertices; j++)
            {
                if (adjacencyMatrix[i, j] != 0)
                {
                    edgeList.Add((i, j, adjacencyMatrix[i, j]));
                }
            }
        }

        edgeList = edgeList.OrderBy(edge => edge.weight).ToList();

        int minSpanningTreeWeight = 0;
        int[] vertexSets = Enumerable.Range(0, numVertices).ToArray();

        foreach (var edge in edgeList)
        {
            int u = edge.u;
            int v = edge.v;
            int weight = edge.weight;

            if (vertexSets[u] != vertexSets[v])
            {
                minSpanningTreeWeight += weight;
                int rootU = Find(vertexSets, u);
                int rootV = Find(vertexSets, v);

                if (rootU != rootV)
                {
                    vertexSets[rootV] = rootU;
                }
            }
        }

        Console.WriteLine($"Минимальный вес остовного дерева: {minSpanningTreeWeight}");
    }

    private static int Find(int[] parent, int i)
    {
        if (parent[i] == i)
        {
            return i;
        }

        return parent[i] = Find(parent, parent[i]);
    }

    private static void DFS(int vertex, int[,] adjacencyMatrix, bool[] visited)
    {
        visited[vertex] = true;
        int numVertices = adjacencyMatrix.GetLength(0);

        for (int i = 0; i < numVertices; i++)
        {
            if (adjacencyMatrix[vertex, i] != 0 && !visited[i])
            {
                DFS(i, adjacencyMatrix, visited);
            }
        }
    }
}