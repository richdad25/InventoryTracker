using System;
using System.Collections.Generic;
using System.Linq;
using InventoryTracker.Domain.Entities;
using InventoryTracker.Domain.Enums;
using InventoryTracker.Domain.ValueObjects;
using ReportType = InventoryTracker.Domain.Enums.ReportType;
using TransactionType = InventoryTracker.Domain.Enums.TransactionType;
using EntitiesReportType = InventoryTracker.Domain.Entities.ReportType;
using EntitiesTransactionType = InventoryTracker.Domain.Entities.TransactionType;

namespace InventoryTracker.ConsoleApp
{
    class Program
    {
        // Тестовые данные (вместо БД)
        private static List<Product> _products = new List<Product>();
        private static List<Supplier> _suppliers = new List<Supplier>();
        private static List<Warehouse> _warehouses = new List<Warehouse>();
        private static List<InventoryTransaction> _transactions = new List<InventoryTransaction>();
        private static List<Report> _reports = new List<Report>();

        static void Main(string[] args)
        {
            Console.WriteLine("=== InventoryTracker Console Tester ===");
            SeedTestData(); // Заполняем тестовыми данными

            while (true)
            {
                Console.WriteLine("\nГлавное меню:");
                Console.WriteLine("1. Управление товарами");
                Console.WriteLine("2. Управление поставщиками");
                Console.WriteLine("3. Управление складами");
                Console.WriteLine("4. Управление транзакциями");
                Console.WriteLine("5. Генерация отчётов");
                Console.WriteLine("6. Просмотр всех данных");
                Console.WriteLine("7. Выход");
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ManageProducts();
                            break;
                        case "2":
                            ManageSuppliers();
                            break;
                        case "3":
                            ManageWarehouses();
                            break;
                        case "4":
                            ManageTransactions();
                            break;
                        case "5":
                            GenerateReports();
                            break;
                        case "6":
                            ShowAllData();
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

        // Заполнение тестовыми данными
        static void SeedTestData()
        {
            var supplier = new Supplier(Guid.NewGuid(), "Поставщик ООО 'Ромашка'", "contact@romashka.ru");
            _suppliers.Add(supplier);

            var warehouse = new Warehouse(Guid.NewGuid(), "Основной склад", "Москва, ул. Ленина 1", 1000);
            _warehouses.Add(warehouse);

            var product = new Product(
                Guid.NewGuid(),
                new ProductName("Ноутбук Lenovo"),
                "Игровой ноутбук",
                new ArticleNumber("LEN-123"),
                new Money(1500),
                10,
                "Электроника",
                supplier.Id,
                null
            );
            _products.Add(product);
        }

        #region Управление товарами
        static void ManageProducts()
        {
            Console.WriteLine("\n=== Управление товарами ===");
            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Изменить цену товара");
            Console.WriteLine("3. Увеличить количество");
            Console.WriteLine("4. Уменьшить количество");
            Console.WriteLine("5. Показать все товары");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddProduct();
                    break;
                case "2":
                    UpdateProductPrice();
                    break;
                case "3":
                    ChangeProductQuantity(true);
                    break;
                case "4":
                    ChangeProductQuantity(false);
                    break;
                case "5":
                    ShowAllProducts();
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }

        static void AddProduct()
        {
            Console.Write("Название товара: ");
            var name = Console.ReadLine();

            Console.Write("Описание: ");
            var description = Console.ReadLine();

            Console.Write("Артикул: ");
            var article = Console.ReadLine();

            Console.Write("Цена: ");
            var price = decimal.Parse(Console.ReadLine());

            Console.Write("Количество: ");
            var quantity = int.Parse(Console.ReadLine());

            Console.Write("Категория: ");
            var category = Console.ReadLine();

            Console.Write("ID поставщика: ");
            var supplierId = Guid.Parse(Console.ReadLine());

            var supplier = _suppliers.FirstOrDefault(s => s.Id == supplierId);
            if (supplier == null)
            {
                Console.WriteLine("Поставщик не найден!");
                return;
            }

            var product = new Product(
                Guid.NewGuid(),
                new ProductName(name),
                description,
                new ArticleNumber(article),
                new Money(price),
                quantity,
                category,
                supplierId,
                null // Срок годности (опционально)
            );

            _products.Add(product);
            Console.WriteLine($"Товар добавлен! ID: {product.Id}");
        }

        static void UpdateProductPrice()
        {
            ShowAllProducts();
            Console.Write("Введите ID товара: ");
            var productId = Guid.Parse(Console.ReadLine());

            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                Console.WriteLine("Товар не найден!");
                return;
            }

            Console.Write("Новая цена: ");
            var newPrice = decimal.Parse(Console.ReadLine());

            product.UpdatePrice(new Money(newPrice));
            Console.WriteLine("Цена обновлена!");
        }

        static void ChangeProductQuantity(bool isIncrease)
        {
            ShowAllProducts();
            Console.Write("Введите ID товара: ");
            var productId = Guid.Parse(Console.ReadLine());

            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                Console.WriteLine("Товар не найден!");
                return;
            }

            Console.Write($"Введите количество для {(isIncrease ? "увеличения" : "уменьшения")}: ");
            var amount = int.Parse(Console.ReadLine());

            try
            {
                if (isIncrease)
                    product.IncreaseQuantity(amount);
                else
                    product.DecreaseQuantity(amount);

                Console.WriteLine($"Теперь товара {product.Name.Value}: {product.Quantity} шт.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ShowAllProducts()
        {
            Console.WriteLine("\nСписок товаров:");
            foreach (var p in _products)
            {
                Console.WriteLine($"{p.Id} | {p.Name.Value} | {p.Article} | {p.Price.Value:C} | {p.Quantity} шт. | Поставщик: {_suppliers.First(s => s.Id == p.SupplierId).Name}");
            }
        }
        #endregion

        #region Управление поставщиками
        static void ManageSuppliers()
        {
            Console.WriteLine("\n=== Управление поставщиками ===");
            Console.WriteLine("1. Добавить поставщика");
            Console.WriteLine("2. Показать всех поставщиков");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddSupplier();
                    break;
                case "2":
                    ShowAllSuppliers();
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }

        static void AddSupplier()
        {
            Console.Write("Название поставщика: ");
            var name = Console.ReadLine();

            Console.Write("Контактная информация: ");
            var contactInfo = Console.ReadLine();

            var supplier = new Supplier(Guid.NewGuid(), name, contactInfo);
            _suppliers.Add(supplier);

            Console.WriteLine($"Поставщик добавлен! ID: {supplier.Id}");
        }

        static void ShowAllSuppliers()
        {
            Console.WriteLine("\nСписок поставщиков:");
            foreach (var s in _suppliers)
            {
                Console.WriteLine($"{s.Id} | {s.Name} | Контакты: {s.ContactInfo}");
            }
        }
        #endregion

        #region Управление складами
        static void ManageWarehouses()
        {
            Console.WriteLine("\n=== Управление складами ===");
            Console.WriteLine("1. Добавить склад");
            Console.WriteLine("2. Добавить товар на склад");
            Console.WriteLine("3. Показать все склады");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddWarehouse();
                    break;
                case "2":
                    AddProductToWarehouse();
                    break;
                case "3":
                    ShowAllWarehouses();
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }

        static void AddWarehouse()
        {
            Console.Write("Название склада: ");
            var name = Console.ReadLine();

            Console.Write("Местоположение: ");
            var location = Console.ReadLine();

            Console.Write("Вместимость (в м³): ");
            var capacity = double.Parse(Console.ReadLine());

            var warehouse = new Warehouse(Guid.NewGuid(), name, location, capacity);
            _warehouses.Add(warehouse);

            Console.WriteLine($"Склад добавлен! ID: {warehouse.Id}");
        }

        static void AddProductToWarehouse()
        {
            ShowAllWarehouses();
            Console.Write("Введите ID склада: ");
            var warehouseId = Guid.Parse(Console.ReadLine());

            var warehouse = _warehouses.FirstOrDefault(w => w.Id == warehouseId);
            if (warehouse == null)
            {
                Console.WriteLine("Склад не найден!");
                return;
            }

            ShowAllProducts();
            Console.Write("Введите ID товара: ");
            var productId = Guid.Parse(Console.ReadLine());

            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                Console.WriteLine("Товар не найден!");
                return;
            }

            Console.Write("Количество: ");
            var quantity = int.Parse(Console.ReadLine());

            warehouse.AddProduct(product, quantity);
            Console.WriteLine($"Товар добавлен на склад {warehouse.Name}!");
        }

        static void ShowAllWarehouses()
        {
            Console.WriteLine("\nСписок складов:");
            foreach (var w in _warehouses)
            {
                Console.WriteLine($"{w.Id} | {w.Name} | {w.Location} | Вместимость: {w.Capacity} м³");
            }
        }
        #endregion

        #region Управление транзакциями
        static void ManageTransactions()
        {
            Console.WriteLine("\n=== Управление транзакциями ===");
            Console.WriteLine("1. Создать транзакцию");
            Console.WriteLine("2. Показать все транзакции");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateTransaction();
                    break;
                case "2":
                    ShowAllTransactions();
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }

        static void CreateTransaction()
        {
            ShowAllProducts();
            Console.Write("Введите ID товара: ");
            var productId = Guid.Parse(Console.ReadLine());

            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                Console.WriteLine("Товар не найден!");
                return;
            }

            Console.WriteLine("Тип операции:");
            Console.WriteLine("1. Поступление");
            Console.WriteLine("2. Списание");
            Console.WriteLine("3. Корректировка");
            Console.Write("Выберите тип: ");
            var typeChoice = Console.ReadLine();

            var type = typeChoice switch
            {
                "1" => TransactionType.Incoming,
                "2" => TransactionType.Outgoing,
                "3" => TransactionType.Adjustment,
                _ => throw new ArgumentException("Неверный тип операции!")
            };

            Console.Write("Количество: ");
            var quantity = int.Parse(Console.ReadLine());

            var transaction = new InventoryTransaction(
                Guid.NewGuid(),
                productId,
                quantity,
                (InventoryTracker.Domain.Entities.TransactionType)type, // Явное приведение типа
                DateTime.UtcNow
            );

            try
            {
                transaction.Execute();
                _transactions.Add(transaction);
                Console.WriteLine($"Транзакция создана! ID: {transaction.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ShowAllTransactions()
        {
            Console.WriteLine("\nСписок транзакций:");
            foreach (var t in _transactions)
            {
                var product = _products.FirstOrDefault(p => p.Id == t.ProductId);
                Console.WriteLine($"{t.Id} | {t.Date} | {product?.Name.Value} | {t.Type} | {t.Quantity} шт.");
            }
        }
        #endregion

        #region Генерация отчётов
        static void GenerateReports()
        {
            Console.WriteLine("\n=== Генерация отчётов ===");
            Console.WriteLine("1. Остатки на складах");
            Console.WriteLine("2. Просроченные товары");
            Console.Write("Выберите тип отчёта: ");

            var choice = Console.ReadLine();

            var reportType = choice switch
            {
                "1" => ReportType.InventoryLevels,
                "2" => ReportType.ExpiredProducts,
                _ => throw new ArgumentException("Неверный тип отчёта!")
            };

            var report = new Report(
                Guid.NewGuid(),
                (InventoryTracker.Domain.Entities.ReportType)reportType, // Явное приведение типа
                DateTime.UtcNow,
                GenerateReportContent(reportType)
            );

            _reports.Add(report);
            Console.WriteLine($"Отчёт сгенерирован! ID: {report.Id}\nСодержимое:\n{report.Content}");
        }

        static string GenerateReportContent(ReportType type)
        {
            return type switch
            {
                Domain.Enums.ReportType.InventoryLevels => string.Join("\n", _products.Select(p => $"{p.Name.Value}: {p.Quantity} шт.")),
                Domain.Enums.ReportType.ExpiredProducts => "Просроченных товаров нет (тестовые данные).",
                _ => "Неизвестный тип отчёта."
            };
        }
        #endregion

        #region Вспомогательные методы
        static void ShowAllData()
        {
            ShowAllProducts();
            ShowAllSuppliers();
            ShowAllWarehouses();
            ShowAllTransactions();
        }
        #endregion
    }
}