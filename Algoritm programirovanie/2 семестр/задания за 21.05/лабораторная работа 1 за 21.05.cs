using System;
using System.Linq;
using System.Collections.Generic;

namespace Task1
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }

    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Vendor(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Incoming
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public int Quantity { get; set; } // Исправлено здесь: убрано лишнее "int"
        public DateTime Date { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Incoming(int id, int productId, int vendorId, int quantity, DateTime date, DateTime expiryDate)
        {
            Id = id;
            ProductId = productId;
            VendorId = vendorId;
            Quantity = quantity;
            Date = date;
            ExpiryDate = expiryDate;
        }
    }

    public class Outgoing
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public Outgoing(int id, int productId, int quantity, DateTime date)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            Date = date;
        }
    }

    public class Disposal
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public Disposal(int id, int productId, int quantity, DateTime date)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            Date = date;
        }
    }

    public class ShopData
    {
        public List<Vendor> Vendors { get; set; } = new List<Vendor>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Incoming> Incomings { get; set; } = new List<Incoming>();
        public List<Outgoing> Outgoings { get; set; } = new List<Outgoing>();
        public List<Disposal> Disposals { get; set; } = new List<Disposal>();
    }

    class Program
    {
        static void Main()
        {
            var shopData = new ShopData();

            shopData.Products.AddRange(new[]
            {
                new Product(1, "Молоко", 70),
                new Product(2, "Хлеб", 40),
                new Product(3, "Яйца", 90)
            });

            shopData.Vendors.AddRange(new[]
            {
                new Vendor(1, "Молочный завод"),
                new Vendor(2, "Хлебозавод"),
                new Vendor(3, "Птицефабрика")
            });

            shopData.Incomings.AddRange(new[]
            {
                new Incoming(1, 1, 1, 100, new DateTime(2025, 1, 1), new DateTime(2025, 2, 1)),
                new Incoming(2, 2, 2, 200, new DateTime(2025, 1, 2), new DateTime(2025, 1, 15)),
                new Incoming(3, 3, 3, 150, new DateTime(2025, 1, 3), new DateTime(2025, 2, 10))
            });

            shopData.Outgoings.AddRange(new[]
            {
                new Outgoing(1, 1, 50, new DateTime(2025, 1, 5)),
                new Outgoing(2, 2, 100, new DateTime(2025, 1, 6)),
                new Outgoing(3, 3, 75, new DateTime(2025, 1, 7))
            });

            shopData.Disposals.AddRange(new[]
            {
                new Disposal(1, 1, 10, new DateTime(2025, 1, 10)),
                new Disposal(2, 2, 5, new DateTime(2025, 1, 12))
            });

            DateTime startDate = new DateTime(2025, 1, 1);
            DateTime endDate = new DateTime(2025, 1, 31);

            decimal totalRevenue = shopData.Outgoings
                .Where(sale => sale.Date >= startDate && sale.Date <= endDate)
                .Join(shopData.Products, sale => sale.ProductId, product => product.Id, (sale, product) => sale.Quantity * product.Price)
                .Sum();

            Console.WriteLine($"Выручка с {startDate.ToShortDateString()} по {endDate.ToShortDateString()}: {totalRevenue} руб.\n");

            Console.WriteLine("Остатки на складе:");

            var stockLevels = shopData.Products.Select(product => new
            {
                product.Name,
                Quantity = shopData.Incomings.Where(incoming => incoming.ProductId == product.Id).Sum(incoming => incoming.Quantity)
                           - shopData.Outgoings.Where(sale => sale.ProductId == product.Id).Sum(sale => sale.Quantity)
                           - shopData.Disposals.Where(writeOff => writeOff.ProductId == product.Id).Sum(writeOff => writeOff.Quantity)
            });

            foreach (var item in stockLevels)
            {
                Console.WriteLine($"{item.Name}: {item.Quantity} шт.");
            }

            Console.WriteLine("\nСписание товаров:");

            var disposalsSortedByProduct = shopData.Disposals
                .OrderBy(disposal => shopData.Products.First(product => product.Id == disposal.ProductId).Name)
                .Select(disposal => new
                {
                    Product = shopData.Products.First(product => product.Id == disposal.ProductId).Name,
                    disposal.Quantity,
                    disposal.Date
                });

            foreach (var item in disposalsSortedByProduct)
            {
                Console.WriteLine($"{item.Product}: {item.Quantity} шт. (дата: {item.Date.ToShortDateString()})");
            }

            Console.WriteLine("\nПоставки по поставщикам:");

            var incomingsByVendor = shopData.Incomings
                .GroupBy(incoming => incoming.VendorId)
                .OrderBy(group => shopData.Vendors.First(vendor => vendor.Id == group.Key).Name)
                .Select(group => new
                {
                    Vendor = shopData.Vendors.First(vendor => vendor.Id == group.Key).Name,
                    Incomings = group.Select(incoming => new
                    {
                        Product = shopData.Products.First(product => product.Id == incoming.ProductId).Name,
                        incoming.Quantity,
                        incoming.Date
                    })
                });

            foreach (var vendorGroup in incomingsByVendor)
            {
                Console.WriteLine(vendorGroup.Vendor);
                foreach (var incoming in vendorGroup.Incomings)
                {
                    Console.WriteLine($"  {incoming.Product}: {incoming.Quantity} шт. (дата: {incoming.Date.ToShortDateString()})");
                }
            }

            Console.WriteLine("\nПродажи по дате:");

            var salesByDate = shopData.Outgoings
                .OrderBy(sale => sale.Date)
                .Select(sale => new
                {
                    Product = shopData.Products.First(product => product.Id == sale.ProductId).Name,
                    sale.Quantity,
                    sale.Date
                });

            foreach (var sale in salesByDate)
            {
                Console.WriteLine($"{sale.Date.ToShortDateString()}: {sale.Product} - {sale.Quantity} шт.");
            }
        }
    }
}