using System.Collections.Generic;
using System.Linq;
using Models;
using DataStore;

namespace Facades
{
    public class OperationFacade
    {
        public void CreateOperation(Operation operation)
        {
            InMemoryDataStore.Operations.Add(operation);

            // Автоматическое обновление баланса счёта.
            var account = InMemoryDataStore.BankAccounts.FirstOrDefault(a => a.Id == operation.BankAccountId);
            if (account != null)
            {
                if (operation.Type == TransactionType.Income)
                    account.Balance += operation.Amount;
                else
                    account.Balance -= operation.Amount;
            }
        }

        public void UpdateOperation(Operation operation)
        {
            var existing = InMemoryDataStore.Operations.FirstOrDefault(o => o.Id == operation.Id);
            if (existing != null)
            {
                existing.Type = operation.Type;
                existing.BankAccountId = operation.BankAccountId;
                existing.Amount = operation.Amount;
                existing.Date = operation.Date;
                existing.Description = operation.Description;
                existing.CategoryId = operation.CategoryId;
            }
        }

        public void DeleteOperation(int id)
        {
            InMemoryDataStore.Operations.RemoveAll(o => o.Id == id);
        }

        public List<Operation> GetAllOperations()
        {
            return InMemoryDataStore.Operations;
        }
    }
}