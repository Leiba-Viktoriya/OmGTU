using System;

public class MyInteger
{
    public int A { get; private set; }
    public int B { get; private set; }

    public MyInteger()
    {
        A = 0;
        B = 0;
    }

    public MyInteger(int a)
    {
        A = a;
        B = 0;
    }

    public MyInteger(int a, int b)
    {
        A = a;
        B = b;
    }

    public int Addition()
    {
        return A + B;
    }

    public int Subtraction()
    {
        return A - B;
    }

    public int Multiplication()
    {
        return A * B;
    }

    public double Division()
    {
        if (B == 0)
        {
            throw new DivideByZeroException("Деление на ноль невозможно!");
        }
        return (double)A / B;
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyInteger obj1 = new MyInteger();
        MyInteger obj2 = new MyInteger(5);
        MyInteger obj3 = new MyInteger(10, 2);

        MyInteger[] objects = { obj1, obj2, obj3 };

        foreach (var obj in objects)
        {
            Console.WriteLine($"Объект: A={obj.A}, B={obj.B}");
            Console.WriteLine($"Сложение: {obj.Addition()}");
            Console.WriteLine($"Вычитание: {obj.Subtraction()}");
            Console.WriteLine($"Умножение: {obj.Multiplication()}");

            try
            {
                Console.WriteLine($"Деление: {obj.Division()}");
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine(new string('-', 30));
        }
    }
}