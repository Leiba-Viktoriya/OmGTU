using System;
using System.Collections.Generic;
using System.Linq;

namespace PhonebookApp
{
    class PhoneRecord
    {
        public string PhoneNumber { get; set; }
        public string OwnerName { get; set; }
        public int ProductionYear { get; set; }
        public string Carrier { get; set; }

        public PhoneRecord(string phoneNumber, string ownerName, int productionYear, string carrier)
        {
            PhoneNumber = phoneNumber;
            OwnerName = ownerName;
            ProductionYear = productionYear;
            Carrier = carrier;
        }

        public override string ToString()
        {
            return $"Владелец: {OwnerName}, Телефон: {PhoneNumber}, Год: {ProductionYear}, Оператор: {Carrier}";
        }
    }

    class Program
    {
        static void Main()
        {
            List<PhoneRecord> phoneDatabase = new List<PhoneRecord>();
            bool continueRunning = true;

            while (continueRunning)
            {
                DisplayMenu();

                Console.Write("Введите ваш выбор: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine();

                    switch (choice)
                    {
                        case 1:
                            AddPhoneRecord(phoneDatabase);
                            break;
                        case 2:
                            FindPhonesByCarrier(phoneDatabase);
                            break;
                        case 3:
                            FindPhonesByYear(phoneDatabase);
                            break;
                        case 4:
                            GroupAndDisplayByCarrier(phoneDatabase);
                            break;
                        case 5:
                            GroupAndDisplayByYear(phoneDatabase);
                            break;
                        case 6:
                            continueRunning = false;
                            Console.WriteLine("Выход из приложения.\n");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Пожалуйста, попробуйте еще раз.\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Пожалуйста, введите число.\n");
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("1. Добавить новую запись о телефоне");
            Console.WriteLine("2. Найти телефоны по оператору");
            Console.WriteLine("3. Найти записи о телефонах по году выпуска");
            Console.WriteLine("4. Сгруппировать и отобразить записи о телефонах по оператору");
            Console.WriteLine("5. Сгруппировать и отобразить записи о телефонах по году выпуска");
            Console.WriteLine("6. Выход");
        }

        static void AddPhoneRecord(List<PhoneRecord> phoneDatabase)
        {
            Console.Write("Введите имя владельца: ");
            string ownerName = Console.ReadLine();
            Console.Write("Введите номер телефона: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Введите оператора: ");
            string carrier = Console.ReadLine();

            int year;
            Console.Write("Введите год выпуска: ");
            if (int.TryParse(Console.ReadLine(), out year))
            {
                phoneDatabase.Add(new PhoneRecord(phoneNumber, ownerName, year, carrier));
                Console.WriteLine("Запись о телефоне успешно добавлена!\n");
            }
            else
            {
                Console.WriteLine("Неверный формат года. Пожалуйста, введите число.\n");
                return;
            }
        }

        static void FindPhonesByCarrier(List<PhoneRecord> phoneDatabase)
        {
            if (phoneDatabase.Count == 0)
            {
                Console.WriteLine("База данных пуста!\n");
                return;
            }

            Console.Write("Введите оператора: ");
            string carrier = Console.ReadLine();

            var phoneRecords = phoneDatabase.Where(p => p.Carrier == carrier).ToList();

            if (phoneRecords.Count == 0)
            {
                Console.WriteLine("Записи о телефонах с таким оператором не найдены.\n");
            }
            else
            {
                Console.WriteLine($"Номера телефонов с оператором \"{carrier}\":");
                foreach (var record in phoneRecords)
                {
                    Console.WriteLine(record.PhoneNumber);
                }
                Console.WriteLine();
            }
        }

        static void FindPhonesByYear(List<PhoneRecord> phoneDatabase)
        {
            if (phoneDatabase.Count == 0)
            {
                Console.WriteLine("База данных пуста!\n");
                return;
            }

            Console.Write("Введите год выпуска: ");
            if (int.TryParse(Console.ReadLine(), out int year))
            {
                var phoneRecords = phoneDatabase.Where(p => p.ProductionYear == year).ToList();

                if (phoneRecords.Count == 0)
                {
                    Console.WriteLine("Записи о телефонах за этот год не найдены.\n");
                }
                else
                {
                    Console.WriteLine($"Записи о телефонах за {year} год:");
                    foreach (var record in phoneRecords)
                    {
                        Console.WriteLine(record.ToString());
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Неверный формат года. Пожалуйста, введите число.\n");
            }
        }

        static void GroupAndDisplayByCarrier(List<PhoneRecord> phoneDatabase)
        {
            if (phoneDatabase.Count == 0)
            {
                Console.WriteLine("База данных пуста!\n");
                return;
            }

            var carrierGroups = phoneDatabase.GroupBy(p => p.Carrier);

            foreach (var group in carrierGroups)
            {
                Console.WriteLine($"Оператор: {group.Key}");
                foreach (var record in group)
                {
                    Console.WriteLine(record.ToString());
                }
                Console.WriteLine();
            }
        }

        static void GroupAndDisplayByYear(List<PhoneRecord> phoneDatabase)
        {
            if (phoneDatabase.Count == 0)
            {
                Console.WriteLine("База данных пуста!\n");
                return;
            }

            var yearGroups = phoneDatabase.GroupBy(p => p.ProductionYear);

            foreach (var group in yearGroups)
            {
                Console.WriteLine($"Год: {group.Key}");
                foreach (var record in group)
                {
                    Console.WriteLine(record.ToString());
                }
                Console.WriteLine();
            }
        }
    }
}