using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using DataStore;

namespace Facades
{
    public class AnalyticsFacade
    {
        public (decimal TotalIncome, decimal TotalExpense) GetIncomeExpenseDifference(DateTime start, DateTime end)
        {
            var filtered = InMemoryDataStore.Operations.Where(o => o.Date >= start && o.Date <= end);
            decimal income = filtered.Where(o => o.Type == TransactionType.Income).Sum(o => o.Amount);
            decimal expense = filtered.Where(o => o.Type == TransactionType.Expense).Sum(o => o.Amount);
            return (income, expense);
        }

        public Dictionary<string, decimal> GroupOperationsByCategory()
        {
            var result = new Dictionary<string, decimal>();
            var groups = InMemoryDataStore.Operations.GroupBy(o => o.CategoryId);
            foreach (var group in groups)
            {
                var category = InMemoryDataStore.Categories.FirstOrDefault(c => c.Id == group.Key);
                string categoryName = category != null ? category.Name : "Неизвестная категория";
                decimal total = group.Sum(o => o.Amount);
                result[categoryName] = total;
            }
            return result;
        }
    }
}