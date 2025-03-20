using System;
using Models;

namespace Factories
{
    // Фабрика для создания доменных объектов с базовой валидацией.
    public static class FinancialEntityFactory
    {
        public static BankAccount CreateBankAccount(int id, string name, decimal balance)
        {
            // Проверка корректности
            if (id < 0) throw new ArgumentException("ID счёта не может быть отрицательным.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Название счёта не может быть пустым.");
            if (balance < 0) throw new ArgumentException("Начальный баланс не может быть отрицательным.");

            return new BankAccount { Id = id, Name = name, Balance = balance };
        }

        public static Category CreateCategory(int id, TransactionType type, string name)
        {
            // Проверка корректности
            if (id < 0) throw new ArgumentException("ID категории не может быть отрицательным.");
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Название категории не может быть пустым.");

            return new Category { Id = id, Type = type, Name = name };
        }

        public static Operation CreateOperation(int id, TransactionType type, int bankAccountId, decimal amount, DateTime date, string description, int categoryId)
        {
            if (id < 0) throw new ArgumentException("ID операции не может быть отрицательным.");
            if (amount < 0) throw new ArgumentException("Сумма операции не может быть отрицательной.");
            if (string.IsNullOrWhiteSpace(description)) throw new ArgumentException("Описание операции не может быть пустым.");
            if (bankAccountId < 0) throw new ArgumentException("ID счёта в операции не может быть отрицательным.");
            if (categoryId < 0) throw new ArgumentException("ID категории в операции не может быть отрицательным.");

            return new Operation
            {
                Id = id,
                Type = type,
                BankAccountId = bankAccountId,
                Amount = amount,
                Date = date,
                Description = description,
                CategoryId = categoryId
            };
        }
    }
}