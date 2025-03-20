using System;
using System.Globalization;
using Models;

namespace ImportExport
{
    // Импортер из CSV (демонстрационный формат).
    public class CsvDataImporter : DataImporter
    {
        protected override ImportResult ParseData(string rawData)
        {
            var result = new ImportResult();
            string[] lines = rawData.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string currentSection = string.Empty;
            string[] headers = null;

            foreach (var line in lines)
            {
                if (line.StartsWith("###"))
                {
                    currentSection = line.Replace("###", "").Trim();
                    headers = null;
                    continue;
                }

                if (headers == null)
                {
                    headers = line.Split(',');
                    continue;
                }

                string[] parts = line.Split(',');
                switch (currentSection)
                {
                    case "BankAccounts":
                        result.BankAccounts.Add(new BankAccount
                        {
                            Id = int.Parse(parts[0]),
                            Name = parts[1],
                            Balance = decimal.Parse(parts[2], CultureInfo.InvariantCulture)
                        });
                        break;
                    case "Categories":
                        result.Categories.Add(new Category
                        {
                            Id = int.Parse(parts[0]),
                            Type = Enum.Parse<TransactionType>(parts[1], true),
                            Name = parts[2]
                        });
                        break;
                    case "Operations":
                        result.Operations.Add(new Operation
                        {
                            Id = int.Parse(parts[0]),
                            Type = Enum.Parse<TransactionType>(parts[1], true),
                            BankAccountId = int.Parse(parts[2]),
                            Amount = decimal.Parse(parts[3], CultureInfo.InvariantCulture),
                            Date = DateTime.Parse(parts[4]),
                            Description = parts[5],
                            CategoryId = int.Parse(parts[6])
                        });
                        break;
                }
            }

            return result;
        }
    }
}