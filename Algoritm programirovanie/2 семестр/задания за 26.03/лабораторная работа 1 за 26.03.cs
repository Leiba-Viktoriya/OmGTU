using System;

namespace PointMovement
{
    public class SpatialPoint
    {
        public int Horizontal { get; set; }
        public int Vertical { get; set; }

        public SpatialPoint(int horizontal, int vertical)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
        }

        public void Shift(int horizontalChange, int verticalChange)
        {
            Horizontal += horizontalChange;
            Vertical += verticalChange;
        }
    }

    public delegate void BoundaryViolationHandler(string alert);

    public class PointNavigator
    {
        private SpatialPoint currentLocation;
        private SpatialPoint lowerBound;
        private SpatialPoint upperBound;
        private Random randomGenerator = new Random();

        public event BoundaryViolationHandler BoundaryViolation;

        public PointNavigator(SpatialPoint startPoint, SpatialPoint lowerLimit, SpatialPoint upperLimit)
        {
            this.currentLocation = startPoint;
            this.lowerBound = lowerLimit;
            this.upperBound = upperLimit;
        }

        public void MoveRandomly()
        {
            int horizontalChange = randomGenerator.Next(-100, 101);
            int verticalChange = randomGenerator.Next(-100, 101);

            currentLocation.Shift(horizontalChange, verticalChange);

            if (currentLocation.Horizontal < lowerBound.Horizontal || currentLocation.Horizontal > upperBound.Horizontal ||
                currentLocation.Vertical < lowerBound.Vertical || currentLocation.Vertical > upperBound.Vertical)
            {
                BoundaryViolation?.Invoke("Внимание! Точка покинула допустимую область.");
            }
        }

        public SpatialPoint CurrentLocation => currentLocation;
    }

    class Program
    {
        static void Main(string[] args)
        {
            SpatialPoint rectangleBottomLeft, rectangleTopRight, initialPoint;
            bool outOfBounds = false;

            Console.Write("Введите координаты левого нижнего угла прямоугольника (два числа через пробел): ");
            string[] inputData = Console.ReadLine().Split(' ');
            rectangleBottomLeft = new SpatialPoint(Convert.ToInt32(inputData[0]), Convert.ToInt32(inputData[1]));

            Console.Write("Введите координаты правого верхнего угла прямоугольника (два числа через пробел): ");
            inputData = Console.ReadLine().Split(' ');
            rectangleTopRight = new SpatialPoint(Convert.ToInt32(inputData[0]), Convert.ToInt32(inputData[1]));

            initialPoint = new SpatialPoint((rectangleTopRight.Horizontal + rectangleBottomLeft.Horizontal) / 2,
                                           (rectangleTopRight.Vertical + rectangleBottomLeft.Vertical) / 2);

            PointNavigator navigator = new PointNavigator(initialPoint, rectangleBottomLeft, rectangleTopRight);
            navigator.BoundaryViolation += alertMessage =>
            {
                Console.WriteLine(alertMessage);
                outOfBounds = true;
            };

            while (!outOfBounds)
            {
                navigator.MoveRandomly();
                Console.WriteLine($"Текущая позиция: ({navigator.CurrentLocation.Horizontal}, {navigator.CurrentLocation.Vertical})");
            }
        }
    }
}