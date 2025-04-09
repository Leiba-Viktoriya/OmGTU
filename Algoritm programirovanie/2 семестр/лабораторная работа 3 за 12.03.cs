using System;
using System.Collections.Generic;

namespace Task3Refactored
{
    public class Auto
    {
        public string Model { get; set; }
        public string DriverName { get; set; }
        public int ProductionYear { get; set; }
        public bool CleanStatus { get; set; }

        public Auto(string model, string driverName, int productionYear, bool cleanStatus)
        {
            Model = model;
            DriverName = driverName;
            ProductionYear = productionYear;
            CleanStatus = cleanStatus;
        }
    }

    public class ParkingSpot
    {
        public List<Auto> Autos { get; set; } = new List<Auto>();

        public void ParkAuto(Auto auto)
        {
            Autos.Add(auto);
        }
    }

    public class CleaningService
    {
        public delegate void CleaningAction(Auto auto);

        public void CleanAuto(Auto auto)
        {
            if (!auto.CleanStatus)
            {
                Console.WriteLine($"\nНачинаем мойку автомобиля: {auto.Model}");
                auto.CleanStatus = true;
                Console.WriteLine($"Автомобиль {auto.Model} теперь выглядит как новый!");
            }
            else
            {
                Console.WriteLine($"\nАвтомобиль {auto.Model} уже чистый!");
            }
        }

        public void CleanAllAutos(ParkingSpot spot)
        {
            CleaningAction washDelegate = CleanAuto;
            foreach (var auto in spot.Autos)
            {
                washDelegate(auto);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            ParkingSpot parkingSpot = new ParkingSpot();

            Console.Write("Укажите количество автомобилей для мойки: ");
            int autoCount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < autoCount; i++)
            {
                Console.WriteLine($"\nВвод данных для автомобиля №{i + 1}:");
                Console.Write("Марка автомобиля: ");
                string model = Console.ReadLine();
                Console.Write("ФИО водителя: ");
                string driverName = Console.ReadLine();
                Console.Write("Год выпуска: ");
                int productionYear = Convert.ToInt32(Console.ReadLine());
                Console.Write("Автомобиль чистый? (да/нет): ");
                string cleanInput = Console.ReadLine().ToLower();
                bool cleanStatus = cleanInput == "да";

                Auto auto = new Auto(model, driverName, productionYear, cleanStatus);
                parkingSpot.ParkAuto(auto);
            }

            CleaningService cleaningService = new CleaningService();
            cleaningService.CleanAllAutos(parkingSpot);
        }
    }
}