using System;
using System.Collections;

namespace ReversePolishNotationCalculator
{
    class ExpressionEvaluator
    {
        static void Main()
        {
            Console.Write("Введите выражение в обратной польской нотации (через пробел): ");
            string inputExpression = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(inputExpression))
            {
                Console.WriteLine("Выражение не задано.");
                return;
            }

            string[] expressionElements = inputExpression.Split(' ');
            Stack numberStack = new Stack();
            string operationSymbols = "+-*/";
            bool isExpressionValid = true;

            foreach (string element in expressionElements)
            {
                if (isExpressionValid)
                {
                    if (operationSymbols.Contains(element))
                    {
                        if (numberStack.Count < 2)
                        {
                            Console.WriteLine("Недостаточно операндов для операции.");
                            isExpressionValid = false;
                            break;
                        }

                        double operandTwo = Convert.ToDouble(numberStack.Pop());
                        double operandOne = Convert.ToDouble(numberStack.Pop());

                        switch (element)
                        {
                            case "+":
                                numberStack.Push(operandOne + operandTwo);
                                break;
                            case "-":
                                numberStack.Push(operandOne - operandTwo);
                                break;
                            case "*":
                                numberStack.Push(operandOne * operandTwo);
                                break;
                            case "/":
                                if (operandTwo == 0)
                                {
                                    Console.WriteLine("Деление на ноль!");
                                    isExpressionValid = false;
                                    break;
                                }
                                numberStack.Push(operandOne / operandTwo);
                                break;
                        }
                    }
                    else
                    {
                        if (double.TryParse(element, out double number))
                        {
                            numberStack.Push(number);
                        }
                        else
                        {
                            Console.WriteLine($"Некорректный элемент выражения: {element}");
                            isExpressionValid = false;
                            break;
                        }
                    }
                }
                else break;
            }

            if (isExpressionValid)
            {
                if (numberStack.Count == 1)
                {
                    Console.WriteLine($"Результат: {numberStack.Pop()}");
                }
                else
                {
                    Console.WriteLine("Некорректное выражение: в стеке осталось больше одного операнда.");
                    Console.Write("Содержимое стека: ");
                    foreach (var item in numberStack)
                    {
                        Console.Write($"{item} ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Выражение не является корректным выражением в обратной польской нотации.");
            }
        }
    }
}