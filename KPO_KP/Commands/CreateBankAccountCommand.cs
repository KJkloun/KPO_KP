using System;
using Facades;
using Factories;
using Models;

namespace Commands
{
    public class CreateBankAccountCommand : ICommand
    {
        private readonly IBankAccountFacade _accountFacade;
        private readonly int _id;
        private readonly string _name;
        private readonly decimal _balance;

        public CreateBankAccountCommand(IBankAccountFacade accountFacade, int id, string name, decimal balance)
        {
            _accountFacade = accountFacade;
            _id = id;
            _name = name;
            _balance = balance;
        }

        public void Execute()
        {
            try
            {
                var account = FinancialEntityFactory.CreateBankAccount(_id, _name, _balance);
                _accountFacade.CreateAccount(account);
                Console.WriteLine("Счёт создан успешно.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка создания счёта: {ex.Message}");
            }
        }
    }
}