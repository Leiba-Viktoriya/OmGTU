using System;
using System.Collections.Generic;

public class WaveAlgorithm
{
    static bool IsValid(int row, int col, char[,] grid)
    {
        return row >= 0 && row < grid.GetLength(0) && col >= 0 && col < grid.GetLength(1);
    }

    static void ExploreNeighbors(int rowIdx, int colIdx, char[,] grid, int currentValue)
    {
        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int newRow = rowIdx + dr[i];
            int newCol = colIdx + dc[i];

            if (IsValid(newRow, newCol, grid) && grid[newRow, newCol] == ' ')
            {
                grid[newRow, newCol] = (char)(currentValue + 1 + '0');
            }
        }
    }

    public static void Main(string[] args)
    {
        char[,] maze = {
            {'x', 'x', 'x', 'x', 'x', 'x', 'x'},
            {'x', ' ', ' ', ' ', ' ', ' ', 'x'},
            {'x', ' ', 'x', 'x', 'x', ' ', 'x'},
            {'x', ' ', 'x', ' ', 'x', ' ', 'x'},
            {'x', ' ', ' ', ' ', ' ', ' ', 'x'},
            {'x', 'x', 'x', 'x', 'x', 'x', 'x'}
        };

        Tuple<int, int> startPoint = Tuple.Create(4, 2);
        Tuple<int, int> endPoint = Tuple.Create(4, 5);

        maze[startPoint.Item1, startPoint.Item2] = '0';

        int maxIterations = maze.GetLength(0) * maze.GetLength(1);
        bool pathFound = false;

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            bool changed = false;
            for (int rowIndex = 0; rowIndex < maze.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < maze.GetLength(1); colIndex++)
                {
                    if (maze[rowIndex, colIndex] != 'x' && maze[rowIndex, colIndex] != ' ' && char.IsDigit(maze[rowIndex, colIndex]))
                    {
                        int currentValue = maze[rowIndex, colIndex] - '0';
                        ExploreNeighbors(rowIndex, colIndex, maze, currentValue);
                        changed = true;
                    }
                }
            }

            if (!changed)
            {
                break;
            }

            if (maze[endPoint.Item1, endPoint.Item2] != ' ')
            {
                pathFound = true;
                break;
            }
        }

        if (pathFound)
        {
            Console.WriteLine($"Путь найден! Длина пути: {maze[endPoint.Item1, endPoint.Item2].ToString()}");
        }
        else
        {
            Console.WriteLine("Путь не найден.");
        }
    }
}