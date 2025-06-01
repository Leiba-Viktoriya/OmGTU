using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class AssignmentProblem
{
    public static void Main(string[] args)
    {
        string[] inputData = {
            "90 76 75 70",
            "35 85 55 65",
            "125 95 90 105",
            "45 110 95 115"
        };

        List<List<int>> costMatrix = new List<List<int>>();

        try
        {
            foreach (string line in inputData)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                List<int> row = line.Split(' ')
                                    .Select(s => s == "-0" ? int.MaxValue : int.Parse(s))
                                    .ToList();
                costMatrix.Add(row);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка при обработке входных данных: {e.Message}");
            return;
        }

        StringBuilder rowVertices = new StringBuilder(string.Concat(Enumerable.Range(0, costMatrix.Count)));
        StringBuilder colVertices = new StringBuilder(string.Concat(Enumerable.Range(0, costMatrix.Count)));

        int totalScore = 0;

        while (costMatrix.Count > 2)
        {
            int rowToRemoveIndex = 0;
            int colToRemoveIndex = 0;
            double maxRatio = 0;

            for (int i = 0; i < rowVertices.Length; i++)
            {
                int minInRow = costMatrix[i].Min();
                totalScore += minInRow;
                for (int j = 0; j < colVertices.Length; j++)
                {
                    if (costMatrix[i][j] != int.MaxValue)
                    {
                        costMatrix[i][j] -= minInRow;
                    }
                }
            }

            for (int j = 0; j < colVertices.Length; j++)
            {
                int minInCol = costMatrix.Select(row => row[j]).Min();
                totalScore += minInCol;
                for (int i = 0; i < rowVertices.Length; i++)
                {
                    if (costMatrix[i][j] != int.MaxValue)
                    {
                        costMatrix[i][j] -= minInCol;
                    }
                }
            }

            for (int i = 0; i < rowVertices.Length; i++)
            {
                for (int j = 0; j < colVertices.Length; j++)
                {
                    if (costMatrix[i][j] == 0)
                    {
                        int minInRow = FindSecondSmallest(costMatrix[i]);

                        List<int> columnValues = costMatrix.Select(row => row[j]).ToList();
                        int minInCol = FindSecondSmallest(columnValues);

                        if ((minInCol + minInRow) > maxRatio)
                        {
                            colToRemoveIndex = j;
                            rowToRemoveIndex = i;
                            maxRatio = minInCol + minInRow;
                        }
                    }
                }
            }

            List<List<int>> newCostMatrix = new List<List<int>>();
            for (int i = 0; i < rowVertices.Length; i++)
            {
                if (i != rowToRemoveIndex)
                {
                    List<int> newRow = new List<int>();
                    for (int j = 0; j < colVertices.Length; j++)
                    {
                        if (j != colToRemoveIndex)
                        {
                            newRow.Add(costMatrix[i][j]);
                        }
                    }
                    newCostMatrix.Add(newRow);
                }
            }
            costMatrix = newCostMatrix;

            Console.WriteLine($"{rowToRemoveIndex} {colToRemoveIndex} {totalScore}\n");
            rowVertices.Remove(rowToRemoveIndex, 1);
            colVertices.Remove(colToRemoveIndex, 1);


            for (int i = 0; i < rowVertices.Length; i++)
            {
                if (!costMatrix[i].Contains(int.MaxValue))
                {
                    for (int j = 0; j < colVertices.Length; j++)
                    {
                        List<int> columnValues = costMatrix.Select(row => row[j]).ToList();
                        if (!columnValues.Contains(int.MaxValue))
                        {
                            costMatrix[i][j] = int.MaxValue;
                        }
                    }
                }
            }
        }

        for (int i = 0; i < rowVertices.Length; i++)
        {
            if (!costMatrix[i].Contains(0))
            {
                for (int row = 0; row < rowVertices.Length; row++)
                {
                    int minInRow = costMatrix[row].Min();
                    totalScore += minInRow;
                    for (int col = 0; col < colVertices.Length; col++)
                    {
                        if (costMatrix[row][col] != int.MaxValue)
                        {
                            costMatrix[row][col] -= minInRow;
                        }
                    }
                }

                for (int col = 0; col < colVertices.Length; col++)
                {
                    int minInCol = costMatrix.Select(row => row[col]).Min();
                    totalScore += minInCol;
                    for (int row = 0; row < rowVertices.Length; row++)
                    {
                        if (costMatrix[row][col] != int.MaxValue)
                        {
                            costMatrix[row][col] -= minInCol;
                        }
                    }
                }
            }
        }

        Console.WriteLine($"Нижняя оценка: {totalScore}");
    }

    private static int FindSecondSmallest(List<int> numbers)
    {
        if (numbers.Count < 2)
        {
            return int.MaxValue;
        }

        int smallest = int.MaxValue;
        int secondSmallest = int.MaxValue;

        foreach (int number in numbers)
        {
            if (number < smallest)
            {
                secondSmallest = smallest;
                smallest = number;
            }
            else if (number < secondSmallest && number != smallest)
            {
                secondSmallest = number;
            }
        }

        return secondSmallest;
    }
}