using System;
using Facades;
using Factories;
using Models;

namespace Commands
{
    public class CreateOperationCommand : ICommand
    {
        private readonly OperationFacade _operationFacade;
        private readonly int _id;
        private readonly TransactionType _type;
        private readonly int _bankAccountId;
        private readonly decimal _amount;
        private readonly DateTime _date;
        private readonly string _description;
        private readonly int _categoryId;

        public CreateOperationCommand(OperationFacade operationFacade,
            int id,
            TransactionType type,
            int bankAccountId,
            decimal amount,
            DateTime date,
            string description,
            int categoryId)
        {
            _operationFacade = operationFacade;
            _id = id;
            _type = type;
            _bankAccountId = bankAccountId;
            _amount = amount;
            _date = date;
            _description = description;
            _categoryId = categoryId;
        }

        public void Execute()
        {
            try
            {
                var operation = FinancialEntityFactory.CreateOperation(
                    _id, _type, _bankAccountId, _amount, _date, _description, _categoryId
                );
                _operationFacade.CreateOperation(operation);
                Console.WriteLine("Операция создана успешно.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка создания операции: {ex.Message}");
            }
        }
    }
}