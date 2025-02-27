using System;

class Program
{
    static Random random = new Random();

    // Функция для генерации одномерного массива
    static int[] GenerateArray(int length)
    {
        int[] array = new int[length];
        for (int i = 0; i < length; i++)
        {
            array[i] = random.Next(1, 101); // Генерация случайного числа от 1 до 100
        }
        return array;
    }

    // Функция для нахождения максимального и минимального элементов в массиве без использования Max и Min
    static (int max, int min) FindMaxMin(int[] arr)
    {
        if (arr == null || arr.Length == 0)
        {
            throw new ArgumentException("The input array cannot be empty or null.");
        }

        int maxElem = arr[0];
        int minElem = arr[0];

        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i] > maxElem)
            {
                maxElem = arr[i];
            }
            if (arr[i] < minElem)
            {
                minElem = arr[i];
            }
        }

        return (maxElem, minElem);
    }

    static void CreateJaggedArray(int m)
    {
        int[][] jaggedArray = new int[m][];

        for (int i = 0; i < m; i++)
        {
            int length = random.Next(1, 11);
            jaggedArray[i] = GenerateArray(length);
            var (maxElem, minElem) = FindMaxMin(jaggedArray[i]);

            Console.WriteLine($"Массив {i + 1}: [{string.Join(", ", jaggedArray[i])}], Максимум: {maxElem}, Минимум: {minElem}");
        }
    }

    
    static void Main(string[] args)
    {
        int m = 5;
        CreateJaggedArray(m);
    }
}