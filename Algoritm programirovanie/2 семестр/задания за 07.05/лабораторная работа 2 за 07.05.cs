using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Task2
{
    class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }

        public Product(string productId, string productName)
        {
            ProductId = productId;
            ProductName = productName;
        }

        public override string ToString()
        {
            return $"ID: {ProductId}, Name: {ProductName}";
        }
    }

    class Supplier
    {
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string PhoneNumber { get; set; }

        public Supplier(string supplierId, string supplierName, string phoneNumber)
        {
            SupplierId = supplierId;
            SupplierName = supplierName;
            PhoneNumber = phoneNumber;
        }
        public override string ToString()
        {
            return $"ID: {SupplierId}, Name: {SupplierName}, Phone: {PhoneNumber}";
        }
    }

    class Transaction
    {
        public string ProductId { get; set; }
        public string SupplierId { get; set; }
        public string TransactionType { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TransactionDate { get; set; }

        public Transaction(string productId, string supplierId, string transactionType, int quantity, decimal price, DateTime transactionDate)
        {
            ProductId = productId;
            SupplierId = supplierId;
            TransactionType = transactionType;
            Quantity = quantity;
            Price = price;
            TransactionDate = transactionDate;
        }
        public override string ToString()
        {
            return $"Product ID: {ProductId}, Supplier ID: {SupplierId}, Type: {TransactionType}, Quantity: {Quantity}, Price: {Price}, Date: {TransactionDate.ToShortDateString()}";
        }
    }

    class Program
    {
        static void Main()
        {
            List<Product> products = new List<Product>();
            List<Supplier> suppliers = new List<Supplier>();
            List<Transaction> transactions = new List<Transaction>();

            while (true)
            {
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Добавить поставщика");
                Console.WriteLine("3. Добавить движение товара");
                Console.WriteLine("4. Отчет по остаткам товаров");
                Console.WriteLine("5. Отчет по поставкам товаров по поставщикам");
                Console.WriteLine("6. Отчет по продажам товаров по датам");
                Console.WriteLine("7. Отчет по выручке от продаж");
                Console.WriteLine("8. Поставщик с максимальным количеством поставок");
                Console.WriteLine("9. Выход");

                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddProduct(products);
                        break;
                    case "2":
                        AddSupplier(suppliers);
                        break;
                                            case "3":
                        AddTransaction(products, suppliers, transactions);
                        break;
                    case "4":
                        ShowProductBalances(products, transactions);
                        break;
                    case "5":
                        ShowSupplierDeliveries(products, suppliers, transactions);
                        break;
                    case "6":
                        ShowSalesByDate(products, transactions);
                        break;
                    case "7":
                        ShowSalesRevenue(transactions);
                        break;
                    case "8":
                        ShowTopSupplier(suppliers, transactions);
                        break;
                    case "9":
                        Console.WriteLine("Выход из программы.");
                        return;
                    default:
                        Console.WriteLine("Некорректный ввод. Пожалуйста, выберите действие из списка.");
                        break;
                }
            }
        }

        static void AddProduct(List<Product> products)
        {
            string productId = GetNonEmptyString("Введите ID товара: ", "ID товара");

            if (products.Any(p => p.ProductId == productId))
            {
                Console.WriteLine("Товар с таким ID уже существует!");
                return;
            }

            string productName = GetNonEmptyString("Введите наименование товара: ", "Наименование товара");

            products.Add(new Product(productId, productName));
            Console.WriteLine("Товар успешно добавлен.\n");
        }

        static void AddSupplier(List<Supplier> suppliers)
        {
            string supplierId = GetNonEmptyString("Введите ID поставщика: ", "ID поставщика");

            if (suppliers.Any(s => s.SupplierId == supplierId))
            {
                Console.WriteLine("Поставщик с таким ID уже существует!");
                return;
            }

            string supplierName = GetNonEmptyString("Введите наименование поставщика: ", "Наименование поставщика");
            string phoneNumber = GetNonEmptyString("Введите контактный телефон: ", "Контактный телефон");

            suppliers.Add(new Supplier(supplierId, supplierName, phoneNumber));
            Console.WriteLine("Поставщик успешно добавлен.\n");
        }

        static void AddTransaction(List<Product> products, List<Supplier> suppliers, List<Transaction> transactions)
        {
            string productId = GetExistingProductId(products, "Введите ID товара: ");

            string transactionType = GetTransactionType("Введите тип операции (поставка/продажа): ");

            string supplierId = null;
            if (transactionType == "поставка")
            {
                supplierId = GetExistingSupplierId(suppliers, "Введите ID поставщика: ");
            }

            int quantity = GetPositiveInt("Введите количество товаров: ", "Количество товаров");
            decimal price = GetPositiveDecimal("Введите цену товара за 1 шт: ", "Цена товара");
            DateTime transactionDate = GetValidDate("Введите дату операции (dd.MM.yyyy): ");

            transactions.Add(new Transaction(productId, supplierId, transactionType, quantity, price, transactionDate));
            Console.WriteLine("Движение товара успешно добавлено.\n");
        }
                static void ShowProductBalances(List<Product> products, List<Transaction> transactions)
        {
            var productBalances = transactions
                .GroupBy(t => t.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    ProductName = products.FirstOrDefault(p => p.ProductId == g.Key)?.ProductName,
                    Balance = g.Sum(t => t.TransactionType == "поставка" ? t.Quantity : -t.Quantity)
                })
                .OrderBy(x => x.ProductName);

            Console.WriteLine("Остатки товаров:");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("| ID товара | Наименование товара | Остаток |");
            Console.WriteLine("--------------------------------------------------");

            foreach (var item in productBalances)
            {
                Console.WriteLine($"| {item.ProductId,-9} | {item.ProductName,-19} | {item.Balance,7} |");
            }

            Console.WriteLine("--------------------------------------------------\n");
        }

        static void ShowSupplierDeliveries(List<Product> products, List<Supplier> suppliers, List<Transaction> transactions)
        {
            var supplierDeliveries = transactions
                .Where(t => t.TransactionType == "поставка")
                .GroupBy(t => t.SupplierId)
                .Select(g => new
                {
                    SupplierId = g.Key,
                    SupplierName = suppliers.FirstOrDefault(s => s.SupplierId == g.Key)?.SupplierName,
                    Deliveries = g.Select(d => new
                    {
                        ProductName = products.FirstOrDefault(p => p.ProductId == d.ProductId)?.ProductName,
                        Quantity = d.Quantity,
                        TransactionDate = d.TransactionDate
                    })
                })
                .OrderBy(x => x.SupplierName);

            foreach (var supplier in supplierDeliveries)
            {
                Console.WriteLine($"Поставщик: {supplier.SupplierName} (ID: {supplier.SupplierId})");
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("| Товар                | Количество | Дата        |");
                Console.WriteLine("--------------------------------------------------");

                foreach (var delivery in supplier.Deliveries)
                {
                    Console.WriteLine($"| {delivery.ProductName,-20} | {delivery.Quantity,10} | {delivery.TransactionDate.ToString("dd.MM.yyyy"),-10} |");
                }

                Console.WriteLine("--------------------------------------------------\n");
            }
        }

        static void ShowSalesByDate(List<Product> products, List<Transaction> transactions)
        {
            var salesByDate = transactions
                .Where(t => t.TransactionType == "продажа")
                .GroupBy(t => t.TransactionDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Sales = g.Select(s => new
                    {
                        ProductName = products.FirstOrDefault(p => p.ProductId == s.ProductId)?.ProductName,
                        Quantity = s.Quantity
                    })
                })
                .OrderBy(x => x.Date);

            foreach (var saleDate in salesByDate)
            {
                Console.WriteLine($"Дата: {saleDate.Date.ToString("dd.MM.yyyy")}");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("| Товар                | Количество |");
                Console.WriteLine("--------------------------------------");

                foreach (var sale in saleDate.Sales)
                {
                    Console.WriteLine($"| {sale.ProductName,-20} | {sale.Quantity,10} |");
                }

                Console.WriteLine("--------------------------------------\n");
            }
        }
                static void ShowSalesRevenue(List<Transaction> transactions)
        {
            decimal totalRevenue = transactions
                .Where(t => t.TransactionType == "продажа")
                .Sum(t => t.Quantity * t.Price);

            Console.WriteLine($"Общая выручка от продаж: {totalRevenue:C}\n");
        }

        static void ShowTopSupplier(List<Supplier> suppliers, List<Transaction> transactions)
        {
            var topSupplier = transactions
                .Where(t => t.TransactionType == "поставка")
                .GroupBy(t => t.SupplierId)
                .Select(g => new
                {
                    SupplierId = g.Key,
                    TotalQuantity = g.Sum(t => t.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .FirstOrDefault();

            if (topSupplier != null)
            {
                Supplier supplier = suppliers.FirstOrDefault(s => s.SupplierId == topSupplier.SupplierId);
                Console.WriteLine("Поставщик с максимальным количеством поставок:");
                Console.WriteLine($"Наименование: {supplier?.SupplierName}");
                Console.WriteLine($"ID: {topSupplier.SupplierId}");
                Console.WriteLine($"Всего поставлено товаров: {topSupplier.TotalQuantity}\n");
            }
            else
            {
                Console.WriteLine("Нет данных о поставках.\n");
            }
        }

        static string GetNonEmptyString(string prompt, string fieldName)
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"{fieldName} не может быть пустым. Пожалуйста, введите значение.");
                    continue;
                }
                return input;
            }
        }

        static string GetExistingProductId(List<Product> products, string prompt)
        {
            string productId;
            while (true)
            {
                productId = GetNonEmptyString(prompt, "ID товара");
                if (products.Any(p => p.ProductId == productId))
                {
                    return productId;
                }
                Console.WriteLine("Товара с таким ID не существует. Пожалуйста, введите существующий ID.");
            }
        }

        static string GetExistingSupplierId(List<Supplier> suppliers, string prompt)
        {
            string supplierId;
            while (true)
            {
                supplierId = GetNonEmptyString(prompt, "ID поставщика");
                if (suppliers.Any(s => s.SupplierId == supplierId))
                {
                    return supplierId;
                }
                Console.WriteLine("Поставщика с таким ID не существует. Пожалуйста, введите существующий ID.");
            }
        }

        static string GetTransactionType(string prompt)
        {
            string transactionType;
            while (true)
            {
                Console.Write(prompt);
                transactionType = Console.ReadLine()?.ToLower();
                if (transactionType == "поставка" || transactionType == "продажа")
                {
                    return transactionType;
                }
                Console.WriteLine("Некорректный тип операции. Пожалуйста, введите 'поставка' или 'продажа'.");
            }
        }

        static int GetPositiveInt(string prompt, string fieldName)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out value) && value > 0)
                {
                    return value;
                }
                Console.WriteLine($"Некорректный ввод для {fieldName}. Пожалуйста, введите положительное целое число.");
            }
        }

        static decimal GetPositiveDecimal(string prompt, string fieldName)
        {
            decimal value;
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out value) && value > 0)
                {
                    return value;
                }
                Console.WriteLine($"Некорректный ввод для {fieldName}. Пожалуйста, введите положительное число.");
            }
        }

        static DateTime GetValidDate(string prompt)
        {
            DateTime date;
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
                Console.WriteLine("Некорректный формат даты. Пожалуйста, введите дату в формате dd.MM.yyyy.");
            }
        }
    }
}