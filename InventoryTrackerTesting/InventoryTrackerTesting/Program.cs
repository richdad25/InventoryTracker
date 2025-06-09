using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace InventoryTracker.ConsoleVerifier
{
    class Program
    {
        private static List<Product> products = new List<Product>();
        private static List<Warehouse> warehouses = new List<Warehouse>();
        private static List<InventoryTransaction> transactions = new List<InventoryTransaction>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Добавить продукт");
                Console.WriteLine("2. Добавить склад");
                Console.WriteLine("3. Создать транзакцию");
                Console.WriteLine("4. Показать все продукты");
                Console.WriteLine("5. Показать все склады");
                Console.WriteLine("6. Показать все транзакции");
                Console.WriteLine("7. Выход");
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddProduct();
                            break;
                        case "2":
                            AddWarehouse();
                            break;
                        case "3":
                            CreateTransaction();
                            break;
                        case "4":
                            ShowProducts();
                            break;
                        case "5":
                            ShowWarehouses();
                            break;
                        case "6":
                            ShowTransactions();
                            break;
                        case "7":
                            return;
                        default:
                            Console.WriteLine("Неверный выбор!");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        static void AddProduct()
        {
            Console.Write("Введите название продукта: ");
            var name = Console.ReadLine();

            Console.Write("Введите артикул: ");
            var article = Console.ReadLine();

            Console.Write("Введите цену: ");
            var price = decimal.Parse(Console.ReadLine());

            Console.Write("Введите количество: ");
            var quantity = int.Parse(Console.ReadLine());

            Console.Write("Введите категорию: ");
            var category = Console.ReadLine();

            var product = new Product(
                new Name(name),
                new Article(article),
                new Price(price),
                quantity,
                category
            );

            products.Add(product);
            Console.WriteLine($"Продукт создан с ID: {product.Id}");
        }

        static void AddWarehouse()
        {
            Console.Write("Введите название склада: ");
            var name = Console.ReadLine();

            Console.Write("Введите адрес: ");
            var address = Console.ReadLine();

            Console.Write("Введите вместимость: ");
            var capacity = int.Parse(Console.ReadLine());

            var warehouse = new Warehouse(
                new Name(name),
                address,
                capacity
            );

            warehouses.Add(warehouse);
            Console.WriteLine($"Склад создан с ID: {warehouse.Id}");
        }

        static void CreateTransaction()
        {
            var product = SelectProduct();
            var warehouse = SelectWarehouse();

            Console.Write("Введите количество: ");
            var quantity = int.Parse(Console.ReadLine());

            Console.Write("Введите тип транзакции (1 - Приход, 2 - Расход): ");
            var type = int.Parse(Console.ReadLine()) == 1
                ? TransactionType.Inbound
                : TransactionType.Outbound;

            var transaction = new InventoryTransaction(
                product.Id,
                warehouse.Id,
                quantity,
                type,
                DateTime.Now
            );

            transactions.Add(transaction);
            Console.WriteLine("Транзакция создана!");
        }

        static void ShowProducts()
        {
            Console.WriteLine("Список продуктов:");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id} - {p.Name.Value} (Артикул: {p.Article.Value}, Цена: {p.Price.Value}, Кол-во: {p.Quantity})");
            }
        }

        static void ShowWarehouses()
        {
            Console.WriteLine("Список складов:");
            foreach (var w in warehouses)
            {
                Console.WriteLine($"{w.Id} - {w.Name.Value} (Адрес: {w.Address}, Вместимость: {w.Capacity})");
            }
        }

        static void ShowTransactions()
        {
            Console.WriteLine("Список транзакций:");
            foreach (var t in transactions)
            {
                var product = products.FirstOrDefault(p => p.Id == t.ProductId);
                var warehouse = warehouses.FirstOrDefault(w => w.Id == t.WarehouseId);

                Console.WriteLine($"{t.Id} - {t.TransactionDate}: {product?.Name.Value} -> {warehouse?.Name.Value} ({t.Quantity} ед., Тип: {t.TransactionType})");
            }
        }

        static Product SelectProduct()
        {
            ShowProducts();
            Console.Write("Введите ID продукта: ");
            var id = Guid.Parse(Console.ReadLine());
            return products.FirstOrDefault(p => p.Id == id)
                ?? throw new Exception("Продукт не найден");
        }

        static Warehouse SelectWarehouse()
        {
            ShowWarehouses();
            Console.Write("Введите ID склада: ");
            var id = Guid.Parse(Console.ReadLine());
            return warehouses.FirstOrDefault(w => w.Id == id)
                ?? throw new Exception("Склад не найден");
        }
    }

    public enum TransactionType
    {
        Inbound,
        Outbound
    }

    public class InventoryTransaction : EntityBase
    {
        public Guid ProductId { get; }
        public Guid WarehouseId { get; }
        public int Quantity { get; }
        public TransactionType TransactionType { get; }
        public DateTime TransactionDate { get; }

        public InventoryTransaction(Guid productId, Guid warehouseId, int quantity,
            TransactionType type, DateTime date)
        {
            ProductId = productId;
            WarehouseId = warehouseId;
            Quantity = quantity;
            TransactionType = type;
            TransactionDate = date;
        }
    }
}