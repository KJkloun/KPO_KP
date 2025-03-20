using System.Collections.Generic;
using Models;

namespace Facades
{
    // Интерфейс для работы со счетами.
    public interface IBankAccountFacade
    {
        void CreateAccount(BankAccount account);
        void UpdateAccount(BankAccount account);
        void DeleteAccount(int id);
        List<BankAccount> GetAllAccounts();
    }
}