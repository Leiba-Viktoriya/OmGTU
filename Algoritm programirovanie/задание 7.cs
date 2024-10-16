using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество элементов (не менее 3): ");
        int n = int.Parse(Console.ReadLine());
        int count = 0;

        for (int i = 0; i < n; i++)
        {
            Console.Write("Введите элемент: ");
            int element = int.Parse(Console.ReadLine());
            if (element % 10 == 5)
            {
                count++;
            }
        }

        Console.WriteLine($"Количество элементов, оканчивающихся на 5: {count}");
    }
}