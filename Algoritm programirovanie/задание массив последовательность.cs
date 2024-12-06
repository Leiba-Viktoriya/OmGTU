using System;

class Program
{
    static void Main(string[] args)
    {
        // 1. Задаем размерность массива
        Console.Write("Введите размер массива: ");
        int size = int.Parse(Console.ReadLine());

        // 2. Инициализируем массив
        int[] array = new int[size];
        Random rand = new Random();

        // Заполняем массив случайными числами от 0 до 100
        for (int i = 0; i < size; i++)
        {
            array[i] = rand.Next(0, 100);
        }

        // Выводим массив
        Console.WriteLine("Элементы массива: " + string.Join(", ", array));

        // 3. Определяем количество элементов, оканчивающихся на 3
        int countEndingWith3 = 0;
        foreach (int number in array)
        {
            if (number % 10 == 3)
            {
                countEndingWith3++;
            }
        }
        Console.WriteLine($"Количество элементов, оканчивающихся на 3: {countEndingWith3}");

        // 4. Проверяем, является ли массив равномерно возрастающей последовательностью
        bool isIncreasing = true;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] <= array[i - 1])
            {
                isIncreasing = false;
                break;
            }
        }
        Console.WriteLine($"Массив является равномерно возрастающей последовательностью: {isIncreasing}");

        // 5. Меняем местами максимальный и минимальный элементы
        int minIndex = 0;
        int maxIndex = 0;

        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] < array[minIndex])
            {
                minIndex = i;
            }
            if (array[i] > array[maxIndex])
            {
                maxIndex = i;
            }
        }

        // Меняем местами
        int temp = array[minIndex];
        array[minIndex] = array[maxIndex];
        array[maxIndex] = temp;
        
        Console.WriteLine("Массив после замены местами максимального и минимального элементов: " + string.Join(", ", array));
    }
}