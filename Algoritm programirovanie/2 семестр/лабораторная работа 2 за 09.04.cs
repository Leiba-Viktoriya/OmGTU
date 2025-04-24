using System;

namespace CalculationModule
{
    public class Arithmetics<T>
    {
        public T OperandA { get; set; }
        public T OperandB { get; set; }

        public Arithmetics(T val1, T val2)
        {
            OperandA = val1;
            OperandB = val2;
        }

        public void Addition()
        {
            dynamic a = OperandA;
            dynamic b = OperandB;
            Console.WriteLine($"Сложение: {a + b}");
        }

        public void Subtraction()
        {
            dynamic a = OperandA;
            dynamic b = OperandB;
            Console.WriteLine($"Вычитание: {a - b}");
        }

        public void Product()
        {
            dynamic a = OperandA;
            dynamic b = OperandB;
            Console.WriteLine($"Умножение: {a * b}");
        }

        public void Quotient()
        {
            dynamic a = OperandA;
            dynamic b = OperandB;
            if (b.Equals(0))
            {
                Console.WriteLine("Ошибка: деление на ноль!\n");
            }
            else
            {
                Console.WriteLine($"Деление: {a / b}\n");
            }
        }
    }

    class EntryPoint
    {
        static void Main(string[] args)
        {
            Arithmetics<int> integerCalc = new Arithmetics<int>(5, 8);
            integerCalc.Addition();
            integerCalc.Subtraction();
            integerCalc.Product();
            integerCalc.Quotient();

            Arithmetics<double> doubleCalc = new Arithmetics<double>(1.5, 0.7);
            doubleCalc.Addition();
            doubleCalc.Subtraction();
            doubleCalc.Product();
            doubleCalc.Quotient();

            Arithmetics<uint> unsignedCalc = new Arithmetics<uint>(100, 10);
            unsignedCalc.Addition();
            unsignedCalc.Subtraction();
            unsignedCalc.Product();
            unsignedCalc.Quotient();
        }
    }
}