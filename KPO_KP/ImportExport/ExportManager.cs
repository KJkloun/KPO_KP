using System.Text.Json;
using DataStore;

namespace ImportExport
{
    public class ExportManager
    {
        public string Export(IExportVisitor visitor)
        {
            // Если формат – JSON, формируем единый JSON-объект:
            if (visitor is JsonExportVisitor)
            {
                // Создаём анонимный объект, в котором храним все списки
                var data = new
                {
                    BankAccounts = InMemoryDataStore.BankAccounts,
                    Categories = InMemoryDataStore.Categories,
                    Operations = InMemoryDataStore.Operations
                };

                // Возвращаем красиво отформатированный JSON
                var options = new JsonSerializerOptions { WriteIndented = true };
                return JsonSerializer.Serialize(data, options);
            }

            // Иначе (CSV, YAML) – старый способ
            var result = "";
            result += "BankAccounts:\n";
            foreach (var account in InMemoryDataStore.BankAccounts)
            {
                result += visitor.VisitBankAccount(account) + "\n";
            }

            result += "\nCategories:\n";
            foreach (var category in InMemoryDataStore.Categories)
            {
                result += visitor.VisitCategory(category) + "\n";
            }

            result += "\nOperations:\n";
            foreach (var operation in InMemoryDataStore.Operations)
            {
                result += visitor.VisitOperation(operation) + "\n";
            }

            return result;
        }
    }
}