using System;
using System.IO;

namespace FileProcessingApp
{
    public class TextFilter
    {
        public void FilterAndWrite(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                string[] allLines = File.ReadAllLines(sourceFilePath);
                var selectedLines = Array.FindAll(allLines, HasOddDigitSequence);
                File.WriteAllLines(destinationFilePath, selectedLines);
                Console.WriteLine("Файл успешно обработан и отфильтрован.");
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Произошла ошибка во время обработки файла: {exception.Message}");
            }
        }

        private bool HasOddDigitSequence(string textLine)
        {
            string currentNumber = "";
            foreach (char character in textLine)
            {
                if (char.IsDigit(character))
                {
                    currentNumber += character;
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentNumber) && TryParseOdd(currentNumber))
                    {
                        return true;
                    }
                    currentNumber = "";
                }
            }

            return !string.IsNullOrEmpty(currentNumber) && TryParseOdd(currentNumber);
        }

        private bool TryParseOdd(string numberString)
        {
            if (int.TryParse(numberString, out int parsedNumber))
            {
                return parsedNumber % 2 != 0;
            }
            return false;
        }
    }

    class EntryPoint
    {
        static void Main()
        {
            var filter = new TextFilter();
            filter.FilterAndWrite("input.txt", "output.txt");
        }
    }
}