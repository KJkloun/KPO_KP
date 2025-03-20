using System;
using System.Diagnostics;

namespace Commands
{
    // Декоратор для измерения времени выполнения команды.
    public class CommandTimerDecorator : ICommand
    {
        private readonly ICommand _innerCommand;

        public CommandTimerDecorator(ICommand command)
        {
            _innerCommand = command;
        }

        public void Execute()
        {
            Stopwatch sw = Stopwatch.StartNew();
            _innerCommand.Execute();
            sw.Stop();
            Console.WriteLine($"Время выполнения: {sw.ElapsedMilliseconds} мс.");
        }
    }
}