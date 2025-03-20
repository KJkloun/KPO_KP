using System;
using ImportExport;
using DataStore;

namespace Commands
{
    public class ImportDataCommand : ICommand
    {
        private readonly DataImporter _importer;
        private readonly string _filePath;

        public ImportDataCommand(DataImporter importer, string filePath)
        {
            _importer = importer;
            _filePath = filePath;
        }

        public void Execute()
        {
            try
            {
                var result = _importer.Import(_filePath);

                // Очищаем in‑memory хранилище
                InMemoryDataStore.BankAccounts.Clear();
                InMemoryDataStore.Categories.Clear();
                InMemoryDataStore.Operations.Clear();

                // Загружаем новые данные из результата импорта
                InMemoryDataStore.BankAccounts.AddRange(result.BankAccounts);
                InMemoryDataStore.Categories.AddRange(result.Categories);
                InMemoryDataStore.Operations.AddRange(result.Operations);

                Console.WriteLine($"Импорт данных из файла '{_filePath}' выполнен успешно.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка импорта данных: {ex.Message}");
            }
        }
    }
}