using System;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();
        int maxLength = 0;
        int currentLength = 0;
        string target = "xyz";

        for (int i = 0; i < input.Length; i++)
        {
            if (currentLength < target.Length && input[i] == target[currentLength])
            {
                currentLength++;
                if (currentLength == target.Length)
                {
                    maxLength = Math.Max(maxLength, currentLength);
                    currentLength = 0;
                }
            }
            else
            {
               if(input[i] == 'x')
               {
                    currentLength = 1;
               }
                else
                {
                  currentLength = 0;
                }
            }
        }

        Console.WriteLine(maxLength > 0 ? target.Length : 0);
    }
}