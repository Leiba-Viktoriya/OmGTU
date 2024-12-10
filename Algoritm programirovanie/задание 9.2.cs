using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        Dictionary<char, int> frequencyMap = new Dictionary<char, int>();

        foreach (char c in input)
        {
            if (char.IsLetter(c))
            {
                if (frequencyMap.ContainsKey(c))
                    frequencyMap[c]++;
                else
                    frequencyMap[c] = 1;
            }
        }

        int minFrequency = frequencyMap.Values.Min();
        var rareCharacters = frequencyMap.Where(x => x.Value == minFrequency).Select(x => x.Key).ToList();

        Console.WriteLine($"Редкие символы: {string.Join(", ", rareCharacters)}");
    }
}