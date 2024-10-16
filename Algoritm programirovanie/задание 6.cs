using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество элементов (не менее 3): ");
        int n = int.Parse(Console.ReadLine());
        
        if (n < 3)
        {
            Console.WriteLine("Количество элементов должно быть не менее 3.");
            return;
        }

        int max = int.MinValue;
        int secondMax = int.MinValue;

        for (int i = 0; i < n; i++)
        {
            Console.Write("Введите элемент: ");
            int element = int.Parse(Console.ReadLine());

            if (element > max)
            {
                secondMax = max;
                max = element;
            }
            else if (element > secondMax && element < max)
            {
                secondMax = element;
            }
        }

        if (secondMax == int.MinValue)
        {
            Console.WriteLine("Второго максимального элемента нет.");
        }
        else
        {
            Console.WriteLine($"Второй максимальный элемент: {secondMax}");
        }
    }
}