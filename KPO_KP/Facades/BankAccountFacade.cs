using System.Collections.Generic;
using System.Linq;
using Models;
using DataStore;

namespace Facades
{
    // Реализация фасада для банковских счетов.
    public class BankAccountFacade : IBankAccountFacade
    {
        public void CreateAccount(BankAccount account)
        {
            InMemoryDataStore.BankAccounts.Add(account);
        }

        public void UpdateAccount(BankAccount account)
        {
            var existing = InMemoryDataStore.BankAccounts.FirstOrDefault(a => a.Id == account.Id);
            if (existing != null)
            {
                existing.Name = account.Name;
                existing.Balance = account.Balance;
            }
        }

        public void DeleteAccount(int id)
        {
            InMemoryDataStore.BankAccounts.RemoveAll(a => a.Id == id);
        }

        public List<BankAccount> GetAllAccounts()
        {
            return InMemoryDataStore.BankAccounts;
        }
    }
}