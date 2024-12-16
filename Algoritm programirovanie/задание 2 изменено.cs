using System;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите числа для сортировки через пробел:");
        string input = Console.ReadLine();

        if (string.IsNullOrEmpty(input)) { Console.WriteLine("Ввод пуст."); return; }

        int[] numbers;
        try {
            numbers = input.Split(' ').Select(int.Parse).ToArray();
        } catch (FormatException) {
            Console.WriteLine("Ошибка ввода: целые числа, пробел."); return;
        } catch (OverflowException) {
            Console.WriteLine("Ошибка ввода: слишком большое число."); return;
        }

        Array.Sort(numbers);
        Console.WriteLine("Отсортированный массив:\n" + string.Join(" ", numbers));
    }
}