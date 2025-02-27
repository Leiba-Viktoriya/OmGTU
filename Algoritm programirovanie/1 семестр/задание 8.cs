using System;

public class ReverseEvenDigits
{
    public static int ReverseEven(int n)
    {
        int reversed = 0;
        bool evenFound = false;

        int temp = n;
        while (temp > 0)
        {
            int digit = temp % 10;
            if (digit % 2 == 0)
            {
                reversed = reversed * 10 + digit;
                evenFound = true;
            }
            temp /= 10;
        }

        return evenFound ? reversed : 0;
    }

    public static void Main(string[] args)
    {
        int num;

        while (true)
        {
            Console.Write("Введите положительное целое число: ");
            if (!int.TryParse(Console.ReadLine(), out num) || num <= 0)
            {
                break;
            }

            int reversedNum = ReverseEven(num);
            if (reversedNum != 0)
            {
                Console.WriteLine($"Чётные цифры в обратном порядке: {reversedNum}");
            }
            else
            {
                Console.WriteLine("Чётных цифр в числе нет.");
            }
        }
    }
}