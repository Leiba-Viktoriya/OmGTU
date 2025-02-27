using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите количество элементов (>= 3): ");
        int p = int.Parse(Console.ReadLine());
        if (p < 3)
        {
            Console.WriteLine("Количество элементов должно быть не менее 3");
            return;
        }
        int countLocalMaxima = 0;
        Console.Write("Введите 1-й элемент: ");
        int previous = int.Parse(Console.ReadLine());

        Console.Write("Введите 2-й элемент: ");
        int current = int.Parse(Console.ReadLine());
        for (int i = 3; i <= p; i++)
        {
            Console.Write($"Введите {i}-й элемент: ");
            int next = int.Parse(Console.ReadLine());
            if (previous < current && current > next)
            {
                countLocalMaxima++;
            }
            previous = current;
            current = next;
        }
        Console.WriteLine("Количество локальных максимумов: " + countLocalMaxima);
    }
}