using Models;

namespace ImportExport
{
    public interface IExportVisitor
    {
        string VisitBankAccount(BankAccount account);
        string VisitCategory(Category category);
        string VisitOperation(Operation operation);
    }
}