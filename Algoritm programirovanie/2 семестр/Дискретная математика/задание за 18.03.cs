using System;

public class FloydWarshall
{
    public static int[,] FloydWarshallAlgorithm(int[,] graph)
    {
        int verticesCount = graph.GetLength(0);

        int[,] distance = new int[verticesCount, verticesCount];
        for (int i = 0; i < verticesCount; i++)
        {
            for (int j = 0; j < verticesCount; j++)
            {
                distance[i, j] = graph[i, j];
            }
        }

        for (int k = 0; k < verticesCount; k++)
        {
            for (int i = 0; i < verticesCount; i++)
            {
                for (int j = 0; j < verticesCount; j++)
                {
                    if (distance[i, k] != int.MaxValue && distance[k, j] != int.MaxValue
                        && distance[i, k] + distance[k, j] < distance[i, j])
                    {
                        distance[i, j] = distance[i, k] + distance[k, j];
                    }
                }
            }
        }

        for (int i = 0; i < verticesCount; i++)
        {
            if (distance[i, i] < 0)
            {
                Console.WriteLine("Обнаружен отрицательный цикл!");
                break;
            }
        }

        return distance;
    }

    public static void PrintMatrix(int[,] matrix)
    {
        int verticesCount = matrix.GetLength(0);
        for (int i = 0; i < verticesCount; i++)
        {
            for (int j = 0; j < verticesCount; j++)
            {
                if (matrix[i, j] == int.MaxValue)
                {
                    Console.Write("INF\t");
                }
                else
                {
                    Console.Write(matrix[i, j] + "\t");
                }
            }
            Console.WriteLine();
        }
    }

    public static void Main(string[] args)
    {
        int[,] graph = {
            {0,   5,  int.MaxValue, 10},
            {int.MaxValue, 0,   3, int.MaxValue},
            {int.MaxValue, int.MaxValue, 0,   1},
            {int.MaxValue, int.MaxValue, int.MaxValue, 0}
        };

        Console.WriteLine("Исходный граф:");
        PrintMatrix(graph);

        int[,] shortestDistances = FloydWarshallAlgorithm(graph);

        Console.WriteLine("\nМатрица кратчайших расстояний:");
        PrintMatrix(shortestDistances);

        Console.ReadKey();
    }
}