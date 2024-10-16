using System:

class Program
{
    static void Main()
    {
        Console.Write("Введите количество элементов (не менее 3): ");
        int p = int.Parse(Console.ReadLine());

        if (p < 3)
        {
            Console.WriteLine("Количество элементов должно быть не менее 3.");
        }
        else
        {
            bool allOdd = true;

            for (int i = 0; i < p; i++)
            {
                Console.Write("Введите элемент: ");
                int element = int.Parse(Console.ReadLine());

                if (element % 2 == 0)
                {
                    allOdd = false;
                }
            }

            if (allOdd)
            {
                Console.WriteLine("Да, все элементы нечётные.");
            }
            else
            {
                Console.WriteLine("Нет, не все элементы нечётные.");
            }
        }
    }
}