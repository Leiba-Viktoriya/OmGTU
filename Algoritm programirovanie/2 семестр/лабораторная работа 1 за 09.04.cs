using System;

namespace Task1
{
    public class GenericArray<DataType>
    {
        private DataType[] dataContainer;

        public GenericArray(DataType[] initialData)
        {
            dataContainer = initialData;
        }

        public void Append(DataType item)
        {
            Console.WriteLine($"Добавление элемента \"{item}\"");
            DataType[] tempArray = new DataType[dataContainer.Length + 1];
            for (int i = 0; i < dataContainer.Length; i++)
            {
                tempArray[i] = dataContainer[i];
            }
            tempArray[tempArray.Length - 1] = item;
            dataContainer = tempArray;
        }

        public bool RemoveAt(int index)
        {
            if (index < 0 || index >= dataContainer.Length)
            {
                return false;
            }

            Console.WriteLine($"Удаление элемента \"{dataContainer[index]}\" по индексу {index}");
            DataType[] tempArray = new DataType[dataContainer.Length - 1];
            for (int i = 0, j = 0; i < dataContainer.Length; i++)
            {
                if (i != index)
                {
                    tempArray[j] = dataContainer[i];
                    j++;
                }
            }
            dataContainer = tempArray;
            return true;
        }

        public (bool, DataType) Retrieve(int index)
        {
            if (index < 0 || index >= dataContainer.Length)
            {
                return (false, default(DataType));
            }
            else
            {
                return (true, dataContainer[index]);
            }
        }

        public void DisplayData()
        {
            Console.WriteLine("Содержимое массива:");
            foreach (var element in dataContainer)
            {
                Console.WriteLine(element);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            string[] stringArray = { "один", "два", "три" };
            GenericArray<string> array1 = new GenericArray<string>(stringArray);
            array1.DisplayData();

            array1.Append("четыре");
            array1.DisplayData();

            if (array1.RemoveAt(2))
            {
                array1.DisplayData();
            }
            else
            {
                Console.WriteLine("Невозможно удалить элемент: неверный индекс!");
            }

            var result1 = array1.Retrieve(2);
            if (result1.Item1)
            {
                Console.WriteLine($"Элемент по индексу 2: {result1.Item2}");
            }
            else
            {
                Console.WriteLine("Невозможно получить элемент: неверный индекс!");
            }

            Console.WriteLine();

            int[] intArray = { 10, 20, 30, 40 };
            GenericArray<int> array2 = new GenericArray<int>(intArray);
            array2.DisplayData();

            array2.Append(50);
            array2.DisplayData();

            array2.Append(60);
            array2.DisplayData();

            if (array2.RemoveAt(6))
            {
                array2.DisplayData();
            }
            else
            {
                Console.WriteLine("Невозможно удалить элемент: неверный индекс!");
                array2.DisplayData();
            }


            var result2 = array2.Retrieve(10);
            if (result2.Item1)
            {
                Console.WriteLine($"Элемент по индексу 10: {result2.Item2}");
            }
            else
            {
                Console.WriteLine("Невозможно получить элемент: неверный индекс!");
            }

            var result3 = array2.Retrieve(4);
            if (result3.Item1)
            {
                Console.WriteLine($"Элемент по индексу 4: {result3.Item2}");
            }
            else
            {
                Console.WriteLine("Невозможно получить элемент: неверный индекс!");
            }
        }
    }
}