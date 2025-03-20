using System.Collections.Generic;
using Models;

namespace ImportExport
{
    // Результат импорта – набор списков для каждой сущности.
    public class ImportResult
    {
        public List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Operation> Operations { get; set; } = new List<Operation>();
    }
}