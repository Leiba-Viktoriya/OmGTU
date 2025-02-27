using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Введите количество грядок (N): ");
        int N = int.Parse(Console.ReadLine());

        Console.Write("Введите ширину грядок (W): ");
        double W = double.Parse(Console.ReadLine());

        Console.Write("Введите расстояние от колодца до края огорода (K): ");
        double K = double.Parse(Console.ReadLine());
        double totalDistance = CalculateTotalDistance(N, W, K);
        Console.WriteLine($"Общее расстояние, которое пройдёт огородник: {totalDistance}");
    }

    static double CalculateTotalDistance(int N, double W, double K)
    {
        double distanceToEachBedAndBack = 2 * K + (N - 1);
        double distanceAroundBeds = N * (1 + 2 * W);          

        return distanceToEachBedAndBack + distanceAroundBeds;
    }
}