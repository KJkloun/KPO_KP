using System;
using System.Collections.Generic;
using Models;

namespace Facades
{
    // Прокси для банковских счетов с кэшированием .
    public class BankAccountFacadeProxy : IBankAccountFacade
    {
        private readonly BankAccountFacade _realFacade;
        private List<BankAccount> _cachedAccounts;
        private bool _cacheValid = false;

        public BankAccountFacadeProxy()
        {
            _realFacade = new BankAccountFacade();
        }

        public void CreateAccount(BankAccount account)
        {
            _realFacade.CreateAccount(account);
            _cacheValid = false;
        }

        public void UpdateAccount(BankAccount account)
        {
            _realFacade.UpdateAccount(account);
            _cacheValid = false;
        }

        public void DeleteAccount(int id)
        {
            _realFacade.DeleteAccount(id);
            _cacheValid = false;
        }

        public List<BankAccount> GetAllAccounts()
        {
            if (!_cacheValid)
            {
                Console.WriteLine("Получение данных из основного хранилища...");
                _cachedAccounts = _realFacade.GetAllAccounts();
                _cacheValid = true;
            }
            else
            {
                Console.WriteLine("Получение данных из кэша...");
            }
            return _cachedAccounts;
        }
    }
}