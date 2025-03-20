using Models;

namespace ImportExport
{
    public class CsvExportVisitor : IExportVisitor
    {
        public string VisitBankAccount(BankAccount account)
        {
            return $"{account.Id},{account.Name},{account.Balance}";
        }

        public string VisitCategory(Category category)
        {
            return $"{category.Id},{category.Type},{category.Name}";
        }

        public string VisitOperation(Operation operation)
        {
            return $"{operation.Id},{operation.Type},{operation.BankAccountId},{operation.Amount},{operation.Date.ToShortDateString()},{operation.Description},{operation.CategoryId}";
        }
    }
}