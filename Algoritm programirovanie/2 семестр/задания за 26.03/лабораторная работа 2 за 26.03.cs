using System;

namespace RaceGame
{
    public class Racer
    {
        public string Name { get; set; }
        public int Speed { get; set; }
        public int DistanceCovered { get; set; }

        public Racer(string name, int speed)
        {
            Name = name;
            Speed = speed;
            DistanceCovered = 0;
        }
    }

    public delegate void RaceFinishedEventHandler(string message);

    public class RaceController
    {
        private readonly Racer _racer1;
        private readonly Racer _racer2;
        private readonly Racer _racer3;
        private readonly int _interval;
        private readonly int _raceDistance;
        private readonly Random _random = new Random();

        public event RaceFinishedEventHandler RaceFinished;

        public RaceController(int interval, Racer racer1, Racer racer2, Racer racer3, int raceDistance)
        {
            _interval = interval;
            _racer1 = racer1;
            _racer2 = racer2;
            _racer3 = racer3;
            _raceDistance = raceDistance;
        }

        public void UpdateRacerStats()
        {
            _racer1.DistanceCovered += _interval * _racer1.Speed;
            _racer2.DistanceCovered += _interval * _racer2.Speed;
            _racer3.DistanceCovered += _interval * _racer3.Speed;

            Racer leadingRacer = GetLeadingRacer();

            if (leadingRacer.DistanceCovered >= _raceDistance)
            {
                OnRaceFinished($"{leadingRacer.Name} wins the race!");
            }
            else
            {
                _racer1.Speed = _random.Next(1, 101);
                _racer2.Speed = _random.Next(1, 101);
                _racer3.Speed = _random.Next(1, 101);
            }
        }

        private Racer GetLeadingRacer()
        {
            Racer leadingRacer = _racer1;
            if (_racer2.DistanceCovered > leadingRacer.DistanceCovered)
            {
                leadingRacer = _racer2;
            }
            if (_racer3.DistanceCovered > leadingRacer.DistanceCovered)
            {
                leadingRacer = _racer3;
            }
            return leadingRacer;
        }

        protected virtual void OnRaceFinished(string message)
        {
            RaceFinished?.Invoke(message);
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Добро пожаловать на гонку!");

            Racer racer1 = CreateRacer(1);
            Racer racer2 = CreateRacer(2);
            Racer racer3 = CreateRacer(3);

            Console.Write("Введите дистанцию гонки (метров): ");
            int raceDistance = Convert.ToInt32(Console.ReadLine());

            RaceController raceController = new RaceController(1, racer1, racer2, racer3, raceDistance);
            bool raceOver = false;

            raceController.RaceFinished += message =>
            {
                Console.WriteLine(message);
                raceOver = true;
            };

            Console.WriteLine("\nГонка началась!");
            while (!raceOver)
            {
                raceController.UpdateRacerStats();
                //System.Threading.Thread.Sleep(100);
            }

            Console.WriteLine("Гонка завершена!");
        }

        static Racer CreateRacer(int racerNumber)
        {
            Console.Write($"Введите имя и начальную скорость (м/мин) участника {racerNumber}, разделенные пробелом: ");
            string[] data = Console.ReadLine().Split(' ');
            return new Racer(data[0], Convert.ToInt32(data[1]));
        }
    }
}