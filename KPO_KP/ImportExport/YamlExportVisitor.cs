using Models;

namespace ImportExport
{
    // Экспорт данных в YAML.
    public class YamlExportVisitor : IExportVisitor
    {
        public string VisitBankAccount(BankAccount account)
        {
            return $"- Id: {account.Id}\n  Name: {account.Name}\n  Balance: {account.Balance}";
        }

        public string VisitCategory(Category category)
        {
            return $"- Id: {category.Id}\n  Type: {category.Type}\n  Name: {category.Name}";
        }

        public string VisitOperation(Operation operation)
        {
            return $"- Id: {operation.Id}\n  Type: {operation.Type}\n  BankAccountId: {operation.BankAccountId}\n  Amount: {operation.Amount}\n  Date: {operation.Date.ToShortDateString()}\n  Description: {operation.Description}\n  CategoryId: {operation.CategoryId}";
        }
    }
}