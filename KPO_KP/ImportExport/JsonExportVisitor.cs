using System.Text.Json;
using Models;

namespace ImportExport
{
    public class JsonExportVisitor : IExportVisitor
    {
        public string VisitBankAccount(BankAccount account) => JsonSerializer.Serialize(account);
        public string VisitCategory(Category category) => JsonSerializer.Serialize(category);
        public string VisitOperation(Operation operation) => JsonSerializer.Serialize(operation);
    }
}