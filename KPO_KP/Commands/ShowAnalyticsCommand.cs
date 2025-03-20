using System;
using Facades;

namespace Commands
{
    public class ShowAnalyticsCommand : ICommand
    {
        private readonly AnalyticsFacade _analyticsFacade;
        private readonly DateTime _start;
        private readonly DateTime _end;

        public ShowAnalyticsCommand(AnalyticsFacade analyticsFacade, DateTime start, DateTime end)
        {
            _analyticsFacade = analyticsFacade;
            _start = start;
            _end = end;
        }

        public void Execute()
        {
            var (income, expense) = _analyticsFacade.GetIncomeExpenseDifference(_start, _end);
            Console.WriteLine($"За период с {_start.ToShortDateString()} по {_end.ToShortDateString()}:");
            Console.WriteLine($"  Доход: {income}");
            Console.WriteLine($"  Расход: {expense}");
            Console.WriteLine($"  Разница: {income - expense}");
        }
    }
}