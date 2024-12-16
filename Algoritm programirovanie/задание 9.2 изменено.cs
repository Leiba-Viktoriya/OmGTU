using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        List<KeyValuePair<char, int>> frequencyList = new List<KeyValuePair<char, int>>();

        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                bool found = false;
                for (int i = 0; i < frequencyList.Count; i++)
                {
                    if (frequencyList[i].Key == c)
                    {
                        frequencyList[i] = new KeyValuePair<char, int>(c, frequencyList[i].Value + 1);
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    frequencyList.Add(new KeyValuePair<char, int>(c, 1));
                }
            }
        }
        
         if (frequencyList.Count == 0)
        {
            Console.WriteLine("Нет буквенных символов.");
            return;
        }
        
        int minFrequency = frequencyList[0].Value;
        for(int i = 1; i < frequencyList.Count; i++)
        {
            if(frequencyList[i].Value < minFrequency)
            {
                minFrequency = frequencyList[i].Value;
            }
        }

        List<char> rareCharacters = new List<char>();
        
        for(int i = 0; i < frequencyList.Count; i++)
        {
            if(frequencyList[i].Value == minFrequency)
            {
                rareCharacters.Add(frequencyList[i].Key);
            }
        }

        Console.WriteLine($"Редкие символы: {string.Join(", ", rareCharacters)}");
    }
}