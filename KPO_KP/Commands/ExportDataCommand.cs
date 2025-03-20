using System;
using System.IO;
using ImportExport;

namespace Commands
{
    public class ExportDataCommand : ICommand
    {
        private readonly ExportManager _exportManager;
        private readonly IExportVisitor _visitor;
        private readonly string _filePath; // Куда сохраняем результат

        public ExportDataCommand(ExportManager exportManager, IExportVisitor visitor, string filePath)
        {
            _exportManager = exportManager;
            _visitor = visitor;
            _filePath = filePath;
        }

        public void Execute()
        {
            // Получаем данные в виде строки
            string exportedData = _exportManager.Export(_visitor);

            // Записываем данные в указанный файл
            File.WriteAllText(_filePath, exportedData);

            Console.WriteLine($"Данные успешно экспортированы в файл '{_filePath}'.");
        }
    }
}