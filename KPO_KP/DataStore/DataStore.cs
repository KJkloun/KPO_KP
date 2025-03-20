using System.Collections.Generic;
using Models;

namespace DataStore
{
    // Простое in-memory хранилище для объектов.
    public static class InMemoryDataStore
    {
        public static List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public static List<Category> Categories { get; set; } = new List<Category>();
        public static List<Operation> Operations { get; set; } = new List<Operation>();
    }
}