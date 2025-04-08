using System;
using System.Collections.Generic;
using System.Linq;

public class GraphAnalyzer
{
    public static int AnalyzeGraph(string[][] adjacencyMatrix)
    {
        int numNodes = adjacencyMatrix.Length;
        int[] nodeLabels = new int[numNodes];
        Array.Fill(nodeLabels, 0);
        int componentCounter = 0;

        for (int nodeIndex = 0; nodeIndex < numNodes; nodeIndex++)
        {
            if (nodeLabels[nodeIndex] == 0)
            {
                componentCounter++;
                nodeLabels[nodeIndex] = componentCounter;
            }

            for (int neighborIndex = 0; neighborIndex < nodeIndex; neighborIndex++)
            {
                if (adjacencyMatrix[neighborIndex][nodeIndex] == "1")
                {
                    if (nodeLabels[neighborIndex] == 0)
                    {
                        nodeLabels[neighborIndex] = nodeLabels[nodeIndex];
                    }
                    else
                    {
                        int minLabel = Math.Min(nodeLabels[nodeIndex], nodeLabels[neighborIndex]);
                        nodeLabels[nodeIndex] = minLabel;
                        nodeLabels[neighborIndex] = minLabel;
                    }
                }
            }
        }

        HashSet<int> componentIds = new HashSet<int>(nodeLabels.Where(x => x > 0));
        return componentIds.Count;
    }

    public static void Main(string[] args)
    {
        string[][] exampleMatrix = new string[][] {
            new string[] { "0", "1", "0", "0" },
            new string[] { "1", "0", "1", "0" },
            new string[] { "0", "1", "0", "1" },
            new string[] { "0", "0", "1", "0" }
        };

        int numComponents = AnalyzeGraph(exampleMatrix);
        Console.WriteLine($"Количество компонент связности графа: {numComponents}");

        string[][] exampleMatrix2 = new string[][] {
            new string[] { "0", "0", "0", "0" },
            new string[] { "0", "0", "0", "0" },
            new string[] { "0", "0", "0", "0" },
            new string[] { "0", "0", "0", "0" }
        };

        int numComponents2 = AnalyzeGraph(exampleMatrix2);
        Console.WriteLine($"Количество компонент связности графа: {numComponents2}");

        string[][] exampleMatrix3 = new string[][] {
            new string[] { "0", "1", "0", "0" },
            new string[] { "1", "0", "0", "0" },
            new string[] { "0", "0", "0", "1" },
            new string[] { "0", "0", "1", "0" }
        };

        int numComponents3 = AnalyzeGraph(exampleMatrix3);
        Console.WriteLine($"Количество компонент связности графа: {numComponents3}");

    }
}