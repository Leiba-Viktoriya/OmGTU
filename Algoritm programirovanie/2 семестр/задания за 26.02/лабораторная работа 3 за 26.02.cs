using System;
using System.Collections.Generic;
using System.Linq;

namespace Task3
{
    class FrequencyAnalyzer
    {
        static void Main()
        {
            List<int> inputList = GetInputList();
            HashSet<int> uniqueElements = new HashSet<int>(inputList);
            Dictionary<int, int> elementFrequency = CalculateFrequency(inputList);

            DisplayUniqueElements(uniqueElements);
            DisplayMostFrequentElements(elementFrequency);
        }

        static List<int> GetInputList()
        {
            Console.Write("Введите количество элементов: ");
            int listSize = Convert.ToInt32(Console.ReadLine());

            List<int> numbers = new List<int>();
            Console.WriteLine($"Введите {listSize} элементов:");
            for (int i = 0; i < listSize; i++)
            {
                numbers.Add(Convert.ToInt32(Console.ReadLine()));
            }
            return numbers;
        }

        static Dictionary<int, int> CalculateFrequency(List<int> data)
        {
            Dictionary<int, int> frequencyMap = new Dictionary<int, int>();
            foreach (int element in data)
            {
                if (frequencyMap.ContainsKey(element))
                {
                    frequencyMap[element]++;
                }
                else
                {
                    frequencyMap[element] = 1;
                }
            }
            return frequencyMap;
        }

        static void DisplayUniqueElements(HashSet<int> uniqueSet)
        {
            Console.WriteLine("Список состоит из следующих элементов: ");
            foreach (int element in uniqueSet)
            {
                Console.WriteLine(element);
            }
        }

        static void DisplayMostFrequentElements(Dictionary<int, int> frequency)
        {
            if (frequency.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            int maxFrequency = frequency.Values.Max();
            Console.WriteLine("Элемент(ы), которые чаще всего встречаются в списке: ");

            foreach (var pair in frequency)
            {
                if (pair.Value == maxFrequency)
                {
                    Console.WriteLine(pair.Key);
                }
            }
        }
    }
}