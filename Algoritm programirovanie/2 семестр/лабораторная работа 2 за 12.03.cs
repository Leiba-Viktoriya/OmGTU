using System;

namespace Task2
{
    class NumberProcessor
    {
        public int ValueA { get; set; }
        public int ValueB { get; set; }

        public NumberProcessor(int a, int b)
        {
            this.ValueA = a;
            this.ValueB = b;
        }

        public int Add(int first, int second) { return first + second; }
        public int Subtract(int first, int second) { return first - second; }
        public int Multiply(int first, int second) { return first * second; }
        public int Divide(int first, int second)
        {
            if (second == 0)
            {
                throw new DivideByZeroException("Деление на ноль!");
            }
            return first / second;
        }

        public int ComputeSequenceOne(int a, int b)
        {
            return Multiply(Subtract(Add(a, b), b), b);
        }

        public int ComputeSequenceTwo(int a, int b)
        {
            return Divide(Subtract(Multiply(a, b), a), a);
        }
    }

    class Program
    {
        public delegate int ArithmeticOperation(int x, int y);

        static void Main()
        {
            Console.Write("Введите первое значение: ");
            int inputA = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите второе значение: ");
            int inputB = Convert.ToInt32(Console.ReadLine());

            NumberProcessor processor = new NumberProcessor(inputA, inputB);

            ArithmeticOperation sequenceOne = processor.ComputeSequenceOne;
            ArithmeticOperation sequenceTwo = processor.ComputeSequenceTwo;

            Console.WriteLine($"Результат первой последовательности: {sequenceOne(inputA, inputB)}");
            try
            {
                Console.WriteLine($"Результат второй последовательности: {sequenceTwo(inputA, inputB)}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}