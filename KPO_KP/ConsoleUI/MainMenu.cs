using System;
using System.IO;
using System.Linq;
using Facades;
using Commands;
using ImportExport;
using Models;
using DataStore;

namespace ConsoleUI
{
    public class MainMenu
    {
        private readonly IBankAccountFacade _accountFacade;
        private readonly CategoryFacade _categoryFacade;
        private readonly OperationFacade _operationFacade;
        private readonly AnalyticsFacade _analyticsFacade;
        private readonly ExportManager _exportManager;

        private static readonly string OUTPUT_FOLDER = Path.GetFullPath(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "output")
        );

        public MainMenu()
        {
            _accountFacade = new BankAccountFacadeProxy();
            _categoryFacade = new CategoryFacade();
            _operationFacade = new OperationFacade();
            _analyticsFacade = new AnalyticsFacade();
            _exportManager = new ExportManager();
        }

        public void Run()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Главное меню ---");
                Console.WriteLine("1. Создать счёт");
                Console.WriteLine("2. Создать категорию");
                Console.WriteLine("3. Создать операцию");
                Console.WriteLine("4. Показать все счета");
                Console.WriteLine("5. Показать все категории");
                Console.WriteLine("6. Показать все операции");
                Console.WriteLine("7. Показать аналитику (доход/расход за период)");
                Console.WriteLine("8. Экспорт данных");
                Console.WriteLine("9. Импорт данных");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите пункт: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateBankAccount();
                        break;
                    case "2":
                        CreateCategory();
                        break;
                    case "3":
                        CreateOperation();
                        break;
                    case "4":
                        ShowAllAccounts();
                        break;
                    case "5":
                        ShowAllCategories();
                        break;
                    case "6":
                        ShowAllOperations();
                        break;
                    case "7":
                        ShowAnalytics();
                        break;
                    case "8":
                        ExportData();
                        break;
                    case "9":
                        ImportData();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Выбран неверный пункт.");
                        break;
                }
            }
        }

        // Проверяем, не занят ли указанный ID, недопустим ли баланс и т.д.
        private void CreateBankAccount()
        {
            int id = InputHelper.ReadInt("Введите ID счёта: ", 0);
            bool idExists = InMemoryDataStore.BankAccounts.Any(a => a.Id == id);
            if (idExists)
            {
                Console.WriteLine($"Счёт с ID={id} уже существует. Операция отменена.");
                return;
            }

            string name = InputHelper.ReadNonEmptyString("Введите название счёта: ");
            decimal balance = InputHelper.ReadDecimal("Введите начальный баланс: ", 0);

            ICommand command = new CreateBankAccountCommand(_accountFacade, id, name, balance);
            new CommandTimerDecorator(command).Execute();
        }

        // Аналогично проверяем, не занят ли ID категории
        private void CreateCategory()
        {
            int id = InputHelper.ReadInt("Введите ID категории: ", 0);
            bool idExists = InMemoryDataStore.Categories.Any(c => c.Id == id);
            if (idExists)
            {
                Console.WriteLine($"Категория с ID={id} уже существует. Операция отменена.");
                return;
            }

            var catType = ReadCategoryType();
            string name = InputHelper.ReadNonEmptyString("Введите название категории: ");

            ICommand command = new CreateCategoryCommand(_categoryFacade, id, catType, name);
            new CommandTimerDecorator(command).Execute();
        }

        // При расходе проверяем, чтобы не уходить в минус.
        // ID операции генерируется автоматически (макс + 1).
        private void CreateOperation()
        {
            var accounts = InMemoryDataStore.BankAccounts;
            if (!accounts.Any())
            {
                Console.WriteLine("Нет доступных счетов. Создайте счёт сначала.");
                return;
            }

            var categories = InMemoryDataStore.Categories;
            if (!categories.Any())
            {
                Console.WriteLine("Нет доступных категорий. Создайте категорию сначала.");
                return;
            }

            Console.WriteLine("\nДоступные счета:");
            for (int i = 0; i < accounts.Count; i++)
                Console.WriteLine($"{i+1}. {accounts[i]}");
            int accountIndex = InputHelper.ReadInt($"Выберите счёт (1..{accounts.Count}): ", 1);
            if (accountIndex > accounts.Count)
            {
                Console.WriteLine("Неверный номер счёта.");
                return;
            }
            var selectedAccount = accounts[accountIndex - 1];

            Console.WriteLine("\nДоступные категории:");
            for (int i = 0; i < categories.Count; i++)
                Console.WriteLine($"{i+1}. {categories[i]}");
            int categoryIndex = InputHelper.ReadInt($"Выберите категорию (1..{categories.Count}): ", 1);
            if (categoryIndex > categories.Count)
            {
                Console.WriteLine("Неверный номер категории.");
                return;
            }
            var selectedCategory = categories[categoryIndex - 1];

            decimal amount = InputHelper.ReadDecimal("Введите сумму: ", 0);
            DateTime date = InputHelper.ReadDate("Введите дату (yyyy-MM-dd): ");
            string description = InputHelper.ReadNonEmptyString("Введите описание операции: ");

            if (selectedCategory.Type == TransactionType.Expense)
            {
                if (selectedAccount.Balance - amount < 0)
                {
                    Console.WriteLine("Недостаточно средств на счёте. Операция не создана.");
                    return;
                }
            }

            int newOperationId = (InMemoryDataStore.Operations.Any())
                ? InMemoryDataStore.Operations.Max(o => o.Id) + 1
                : 1;

            ICommand command = new CreateOperationCommand(
                _operationFacade,
                newOperationId,
                selectedCategory.Type,
                selectedAccount.Id,
                amount,
                date,
                description,
                selectedCategory.Id
            );

            new CommandTimerDecorator(command).Execute();
            Console.WriteLine($"Операция создана с ID = {newOperationId}.");
        }

        private void ShowAllAccounts()
        {
            Console.WriteLine("\nСписок счетов:");
            foreach (var account in _accountFacade.GetAllAccounts())
                Console.WriteLine(account);
        }

        private void ShowAllCategories()
        {
            Console.WriteLine("\nСписок категорий:");
            foreach (var cat in InMemoryDataStore.Categories)
                Console.WriteLine(cat);
        }

        private void ShowAllOperations()
        {
            Console.WriteLine("\nСписок операций:");
            foreach (var op in InMemoryDataStore.Operations)
                Console.WriteLine(op);
        }

        private void ShowAnalytics()
        {
            DateTime start = InputHelper.ReadDate("Введите дату начала (yyyy-MM-dd): ");
            DateTime end = InputHelper.ReadDate("Введите дату конца (yyyy-MM-dd): ");
            ICommand command = new ShowAnalyticsCommand(_analyticsFacade, start, end);
            new CommandTimerDecorator(command).Execute();
        }

        private void ExportData()
        {
            if (!Directory.Exists(OUTPUT_FOLDER))
                Directory.CreateDirectory(OUTPUT_FOLDER);

            int format = InputHelper.ReadFileFormat("Выберите формат экспорта");
            string correctExt = format switch
            {
                1 => ".json",
                2 => ".csv",
                3 => ".yaml",
                _ => ".json"
            };
            string defaultFileName = format switch
            {
                1 => "default_export.json",
                2 => "default_export.csv",
                3 => "default_export.yaml",
                _ => "default_export.json"
            };
            Console.WriteLine($"Нажмите Enter, чтобы использовать '{defaultFileName}', или введите имя файла:");
            string inputName = Console.ReadLine();

            string finalPath;
            if (string.IsNullOrWhiteSpace(inputName))
            {
                finalPath = Path.Combine(OUTPUT_FOLDER, defaultFileName);
            }
            else
            {
                finalPath = Path.Combine(OUTPUT_FOLDER, inputName);
                string existingExt = Path.GetExtension(finalPath).ToLower();
                if (string.IsNullOrEmpty(existingExt) || existingExt != correctExt)
                {
                    string baseName = Path.GetFileNameWithoutExtension(finalPath);
                    finalPath = Path.Combine(Path.GetDirectoryName(finalPath) ?? "", baseName + correctExt);
                }
            }

            IExportVisitor visitor = format switch
            {
                1 => new JsonExportVisitor(),
                2 => new CsvExportVisitor(),
                3 => new YamlExportVisitor(),
                _ => new JsonExportVisitor()
            };

            ICommand command = new ExportDataCommand(_exportManager, visitor, finalPath);
            new CommandTimerDecorator(command).Execute();
        }

        private void ImportData()
        {
            int format = InputHelper.ReadFileFormat("Выберите формат импорта");
            string defaultFileName = format switch
            {
                1 => "testdata.json",
                2 => "testdata.csv",
                3 => "testdata.yaml",
                _ => "testdata.json"
            };
            Console.WriteLine($"Нажмите Enter, чтобы использовать '{defaultFileName}', или введите путь к файлу:");
            string inputPath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputPath))
            {
                inputPath = defaultFileName;
            }

            DataImporter importer = format switch
            {
                1 => new JsonDataImporter(),
                2 => new CsvDataImporter(),
                3 => new YamlDataImporter(),
                _ => new JsonDataImporter()
            };

            ICommand command = new ImportDataCommand(importer, inputPath);
            new CommandTimerDecorator(command).Execute();
        }

        private TransactionType ReadCategoryType()
        {
            while (true)
            {
                Console.Write("Выберите тип категории (1=Income, 2=Expense): ");
                string input = Console.ReadLine();
                if (input == "1") return TransactionType.Income;
                if (input == "2") return TransactionType.Expense;
                Console.WriteLine("Ошибка: введите 1 или 2.");
            }
        }
    }
}