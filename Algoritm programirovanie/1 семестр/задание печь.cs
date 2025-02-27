using System;

class Печь
{
    public int Температура { get; set; }
    public int Длительность { get; set; }

    public Печь(int температура, int длительность)
    {
        Температура = температура;
        Длительность = длительность;
    }
}

class Хлеб : Печь
{
    public string Наименование { get; set; }

    public Хлеб(string наименование, int температура, int длительность) : base(температура, длительность)
    {
        Наименование = наименование;
    }
}

class Программа
{
    static void Main(string[] args)
    {
        Хлеб[] хлеба = new Хлеб[10];
        int count = 0;

        while (true)
        {
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Добавить хлеб");
            Console.WriteLine("2. Выборка по длительности");
            Console.WriteLine("3. Выборка по температуре");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите действие: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    if (count < хлеба.Length)
                    {
                        Console.Write("Введите наименование хлеба: ");
                        string наименование = Console.ReadLine();
                        Console.Write("Введите температуру: ");
                        int температура = int.Parse(Console.ReadLine());
                        Console.Write("Введите длительность: ");
                        int длительность = int.Parse(Console.ReadLine());

                        хлеба[count] = new Хлеб(наименование, температура, длительность);
                        count++;
                    }
                    else
                    {
                        Console.WriteLine("Массив заполнен.");
                    }
                    break;

                case "2":
                    Console.Write("Введите минимальную длительность: ");
                    int minDuration = int.Parse(Console.ReadLine());
                    Console.WriteLine("Хлеба, которые печутся дольше заданного времени:");
                    foreach (var хлеб in хлеба)
                    {
                        if (хлеб != null && хлеб.Длительность > minDuration)
                        {
                            Console.WriteLine($"Наименование: {хлеб.Наименование}, Температура: {хлеб.Температура}, Длительность: {хлеб.Длительность}");
                        }
                    }
                    break;

                case "3":
                    Console.Write("Введите температуру: ");
                    int temperature = int.Parse(Console.ReadLine());
                    Console.WriteLine("Хлеба, которые печутся при данной температуре:");
                    foreach (var хлеб in хлеба)
                    {
                        if (хлеб != null && хлеб.Температура == temperature)
                        {
                            Console.WriteLine($"Наименование: {хлеб.Наименование}, Длительность: {хлеб.Длительность}");
                        }
                    }
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}