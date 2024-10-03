using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите первое число: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Введите второе число: ");
        int b = int.Parse(Console.ReadLine());

        int max = (a + b + Math.Abs(a - b)) / 2;
        int min = (a + b - Math.Abs(a - b)) / 2;

        Console.WriteLine("Наибольшее значение: " + max);
        Console.WriteLine("Наименьшее значение: " + min);
    }
}