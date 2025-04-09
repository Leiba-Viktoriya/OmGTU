using System;
using System.Collections.Generic;

namespace Task1
{
    class Contact
    {
        public string Provider { get; set; }
        public string PhoneNumber { get; set; }

        public Contact(string provider, string number)
        {
            Provider = provider;
            PhoneNumber = number;
        }
    }

    class PhoneDirectory
    {
        static void Main()
        {
            List<Contact> phoneBook = new List<Contact>();
            Dictionary<string, int> operatorFrequency = new Dictionary<string, int>();

            Console.Write("Введите количество контактов для добавления: ");
            int recordCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"Введите данные для {recordCount} контактов:");

            for (int i = 0; i < recordCount; i++)
            {
                Console.Write($"Введите номер телефона: ");
                string phoneNumber = Console.ReadLine();
                Console.Write($"Введите название оператора: ");
                string providerName = Console.ReadLine();
                Contact newContact = new Contact(providerName, phoneNumber);
                phoneBook.Add(newContact);
            }

            foreach (var contact in phoneBook)
            {
                if (operatorFrequency.ContainsKey(contact.Provider))
                {
                    operatorFrequency[contact.Provider]++;
                }
                else
                {
                    operatorFrequency.Add(contact.Provider, 1);
                }
            }

            Console.WriteLine("Статистика операторов:");
            foreach (var entry in operatorFrequency)
            {
                Console.WriteLine($"{entry.Key} - {entry.Value}");
            }
        }
    }
}