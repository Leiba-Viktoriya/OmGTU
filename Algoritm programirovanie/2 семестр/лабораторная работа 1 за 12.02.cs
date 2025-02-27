using System;

namespace ConsoleApplication1
{
    class Contact
    {
        public string FullName { get; set; }
        public Phone[] PhoneNumbers { get; set; }
        public string ResidenceCity { get; set; }

        public Contact(string fullName, Phone[] phoneNumbers, string residenceCity)
        {
            FullName = fullName;
            PhoneNumbers = phoneNumbers;
            ResidenceCity = residenceCity;
        }

        public void Display()
        {
            Console.WriteLine("ФИО: " + FullName);
            foreach (var phone in PhoneNumbers)
            {
                Console.WriteLine("Номер телефона: " + phone.Number);
            }
            Console.WriteLine("Город проживания: " + ResidenceCity);
        } 
    }

    class Phone
    {
        public int Number { get; set; }
        public string Carrier { get; set; }
        public int ContractYear { get; set; }

        public Phone(int number, string carrier, int contractYear)
        {
            Number = number;
            Carrier = carrier;
            ContractYear = contractYear;
        }

        public void Display()
        {
            Console.WriteLine("Номер телефона: " + Number);
            Console.WriteLine("Оператор мобильной связи: " + Carrier);
            Console.WriteLine("Дата заключения договора (год): " + ContractYear);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int selectedOption = 0;
            int contactCount = 0;
            Contact[] contactsDatabase = new Contact[contactCount];
            while (selectedOption < 6)
            {
                Console.WriteLine("1. Заполнение\n" +
                    "2. Выборка по дате заключения договора (год)\n" +
                    "3. Выборка по оператору связи\n" +
                    "4. Выборка по номеру телефона\n" +
                    "5. Выборка по городу проживания абонента\n" +
                    "6. Выход");
                Console.Write("Выберите опцию: ");
                selectedOption = Convert.ToInt32(Console.ReadLine());

                switch (selectedOption)
                {
                    case 1:
                        Console.Write("\nВведите количество абонентов: ");
                        contactCount = Convert.ToInt32(Console.ReadLine());
                        contactsDatabase = new Contact[contactCount];
                        for (int i = 0; i < contactCount; i++)
                        {
                            Console.WriteLine("\nВведите ФИО, город проживания, количество номеров телефонов: ");
                            Console.Write("ФИО: ");
                            string fullName = Console.ReadLine();
                            Console.Write("Город проживания: ");
                            string residenceCity = Console.ReadLine();
                            Console.Write("Количество номеров телефонов: ");
                            int phoneCount = Convert.ToInt32(Console.ReadLine());
                            Phone[] phoneNumbers = new Phone[phoneCount];
                            for (int j = 0; j < phoneCount; j++)
                            {
                                Console.Write("\nНомер телефона: ");
                                int number = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Оператор связи: ");
                                string carrier = Console.ReadLine();
                                Console.Write("Дата заключения договора (год): ");
                                int contractYear = Convert.ToInt32(Console.ReadLine());
                                phoneNumbers[j] = new Phone(number, carrier, contractYear);
                            }
                            contactsDatabase[i] = new Contact(fullName, phoneNumbers, residenceCity);
                        }
                        break;
                    case 2:
                        if (contactCount > 0)
                        {
                            Console.Write("\nВведите год заключения договора: ");
                            int selectedYear = Convert.ToInt32(Console.ReadLine());
                            foreach (var contact in contactsDatabase)
                            {
                                foreach (var phone in contact.PhoneNumbers)
                                {
                                    if (phone.ContractYear == selectedYear)
                                    {
                                        Console.WriteLine();
                                        phone.Display();
                                    }
                                }
                            }
                        }
                        else 
                        {
                            Console.WriteLine("\nБаза данных не заполнена\n");
                        }
                        break;
                    case 3:
                        if (contactCount > 0)
                        {
                            Console.Write("\nВведите название оператора связи: ");
                            string selectedCarrier = Console.ReadLine();
                            foreach (var contact in contactsDatabase)
                            {
                                foreach (var phone in contact.PhoneNumbers)
                                {
                                    if (phone.Carrier == selectedCarrier)
                                    {
                                        Console.WriteLine(phone.Number);
                                    }
                                }
                            }
                        }
                        else 
                        {
                            Console.WriteLine("\nБаза данных не заполнена\n");
                        }
                        break;
                    case 4:
                        if (contactCount > 0)
                        {
                            Console.Write("\nВведите номер телефона: ");
                            int selectedNumber = Convert.ToInt32(Console.ReadLine());
                            foreach (var contact in contactsDatabase)
                            {
                                foreach (var phone in contact.PhoneNumbers)
                                {
                                    if (phone.Number == selectedNumber)
                                    {
                                        Console.WriteLine("\nФИО: " + contact.FullName);
                                        phone.Display();
                                        break;
                                    }
                                }
                            }
                        }
                        else 
                        {
                            Console.WriteLine("\nБаза данных не заполнена\n");
                        }
                        break;
                    case 5:
                        if (contactCount > 0)
                        {
                            Console.Write("\nВведите город проживания: ");
                            string selectedCity = Console.ReadLine();
                            foreach (var contact in contactsDatabase)
                            {
                                if (contact.ResidenceCity == selectedCity)
                                {
                                    Console.WriteLine();
                                    contact.Display();
                                }
                            }
                        }
                        else 
                        {
                            Console.WriteLine("\nБаза данных не заполнена\n");
                        }
                        break;
                }
            }
        }
    }
}