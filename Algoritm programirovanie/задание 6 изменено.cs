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
        int countMax = 0;

        for (int i = 0; i < n; i++)
        {
            Console.Write("Введите элемент: ");
            int element = int.Parse(Console.ReadLine());

            if (element > max)
            {
                secondMax = max;
                max = element;
                countMax = 1;
            }
            else if (element == max)
            {
                countMax++;
            }
            else if (element > secondMax)
            {
              secondMax = element;
            }

        }

        if (countMax > 1) 
        {
             Console.WriteLine($"Второй максимальный элемент: {max}");
        }
       
        else if (secondMax == int.MinValue)
        {
            Console.WriteLine("Второго максимального элемента нет.");
        }
        else
        {
            Console.WriteLine($"Второй максимальный элемент: {secondMax}");
        }
    }
}