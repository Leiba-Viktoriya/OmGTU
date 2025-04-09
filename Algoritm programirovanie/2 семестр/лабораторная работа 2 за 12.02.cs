using System;

namespace GeometryCalculator
{
    class Figure
    {
        public string Label { get; set; }

        public Figure(string label)
        {
            this.Label = label;
        }
    }

    public interface IComputable
    {
        double CalculateArea();
        double CalculatePerimeter();
    }

    class Round : Figure, IComputable
    {
        public double Extent { get; set; }
        public Round(string label, double extent) : base(label)
        {
            this.Extent = extent;
        }
        public double CalculateArea() { return Extent * Extent * Math.PI; }
        public double CalculatePerimeter() { return 2 * Extent * Math.PI; }
    }

    class Block : Figure, IComputable
    {
        public double Span { get; set; }
        public Block(string label, double span) : base(label)
        {
            this.Span = span;
        }
        public double CalculateArea() { return Span * Span; }
        public double CalculatePerimeter() { return 4 * Span; }
    }

    class RegularTriangle : Figure, IComputable
    {
        public double Edge { get; set; }
        public RegularTriangle(string label, double edge) : base(label)
        {
            this.Edge = edge;
        }
        public double CalculateArea() { return (Math.Sqrt(3) / 4) * Edge * Edge; }
        public double CalculatePerimeter() { return 3 * Edge; }
    }

    class Application
    {
        static void Main()
        {
            Console.Write("Укажите имя для окружности: ");
            string circleName = Console.ReadLine();
            Console.Write($"Введите размер радиуса для окружности \"{circleName}\": ");
            double circleRadius = Convert.ToDouble(Console.ReadLine());
            Round circle = new Round(circleName, circleRadius);

            Console.Write("Укажите имя для квадрата: ");
            string squareName = Console.ReadLine();
            Console.Write($"Введите размер стороны для квадрата \"{squareName}\": ");
            double squareSide = Convert.ToDouble(Console.ReadLine());
            Block square = new Block(squareName, squareSide);

            Console.Write("Укажите имя для равностороннего треугольника: ");
            string triangleName = Console.ReadLine();
            Console.Write($"Введите размер стороны для равностороннего треугольника \"{triangleName}\": ");
            double triangleSide = Convert.ToDouble(Console.ReadLine());
            RegularTriangle triangle = new RegularTriangle(triangleName, triangleSide);

            Console.WriteLine();

            Console.WriteLine($"Фигура: {circle.Label}\nПлощадь: {circle.CalculateArea()}\nПериметр: {circle.CalculatePerimeter()}\n");
            Console.WriteLine($"Фигура: {square.Label}\nПлощадь: {square.CalculateArea()}\nПериметр: {square.CalculatePerimeter()}\n");
            Console.WriteLine($"Фигура: {triangle.Label}\nПлощадь: {triangle.CalculateArea()}\nПериметр: {triangle.CalculatePerimeter()}\n");
        }
    }
}