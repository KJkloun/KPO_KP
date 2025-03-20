using System;

namespace Models
{
    // Сущность операции (доход/расход).
    public class Operation
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public int BankAccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public override string ToString()
        {
            return $"[{Id}] {Type} на сумму {Amount} от {Date.ToShortDateString()} (Счёт: {BankAccountId}, Категория: {CategoryId}) - {Description}";
        }
    }
}