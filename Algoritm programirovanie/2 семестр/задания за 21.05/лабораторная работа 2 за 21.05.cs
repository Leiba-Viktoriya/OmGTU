using System;
using System.Collections.Generic;

namespace PalindromeFinder
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите размер массива: ");
            if (!int.TryParse(Console.ReadLine(), out int arraySize) || arraySize <= 0)
            {
                Console.WriteLine("Некорректный размер массива. Пожалуйста, введите положительное целое число.");
                return;
            }

            List<int> numbers = FillArray(arraySize);

            FindAndPrintPalindromes(numbers);
        }

        static List<int> FillArray(int size)
        {
            List<int> arr = new List<int>();
            for (int i = 0; i < size; i++)
            {
                Console.Write($"Введите {i + 1}-й элемент: ");
                if (!int.TryParse(Console.ReadLine(), out int value))
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                    i--;
                    continue;
                }
                arr.Add(value);
            }
            return arr;
        }

        static void FindAndPrintPalindromes(List<int> arr)
        {
            foreach (int number in arr)
            {
                if (IsPalindrome(number))
                {
                    Console.WriteLine($"Число {number} — палиндром");
                }
            }
        }

        static bool IsPalindrome(int number)
        {
            int reversedNumber = 0;
            int originalNumber = number;

            while (number > 0)
            {
                reversedNumber = reversedNumber * 10 + number % 10;
                number /= 10;
            }

            return originalNumber == reversedNumber;
        }
    }
}