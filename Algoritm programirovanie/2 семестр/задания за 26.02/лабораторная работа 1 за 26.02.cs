using System;
using System.Collections.Generic;

namespace BracketValidator
{
    class BracketChecker
    {
        static void Main(string[] args)
        {
            Console.Write("Введите строку для проверки скобок: ");
            string inputString = Console.ReadLine();

            if (IsValidBrackets(inputString))
            {
                Console.WriteLine("Скобки в строке расставлены корректно.");
            }
            else
            {
                Console.WriteLine("Скобки в строке расставлены некорректно.");
            }
        }

        static bool IsValidBrackets(string input)
        {
            Stack<char> bracketStack = new Stack<char>();
            Dictionary<char, char> bracketPairs = new Dictionary<char, char>()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' }
            };

            foreach (char character in input)
            {
                if (bracketPairs.ContainsKey(character))
                {
                    bracketStack.Push(character);
                }
                else if (bracketPairs.ContainsValue(character))
                {
                    if (bracketStack.Count == 0)
                    {
                        return false;
                    }

                    char top = bracketStack.Pop();
                    if (bracketPairs[top] != character)
                    {
                        return false;
                    }
                }
            }

            return bracketStack.Count == 0;
        }
    }
}