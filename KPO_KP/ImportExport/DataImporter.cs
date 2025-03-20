using System;
using System.IO;

namespace ImportExport
{
    // Абстрактный класс, реализующий алгоритм импорта.
    public abstract class DataImporter
    {
        public ImportResult Import(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Файл импорта не найден.", filePath);

            string rawData = File.ReadAllText(filePath);
            return ParseData(rawData);
        }

        protected abstract ImportResult ParseData(string rawData);
    }
}