using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using Models;

namespace ImportExport
{
    public class YamlDataImporter : DataImporter
    {
        protected override ImportResult ParseData(string rawData)
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();
            
            var yamlModel = deserializer.Deserialize<YamlModel>(rawData);
            
            var result = new ImportResult();
            if (yamlModel.BankAccounts != null)
            {
                foreach (var ba in yamlModel.BankAccounts)
                {
                    result.BankAccounts.Add(new BankAccount
                    {
                        Id = ba.Id,
                        Name = ba.Name,
                        Balance = ba.Balance
                    });
                }
            }

            if (yamlModel.Categories != null)
            {
                foreach (var cat in yamlModel.Categories)
                {
                    result.Categories.Add(new Category
                    {
                        Id = cat.Id,
                        Type = Enum.Parse<TransactionType>(cat.Type, true),
                        Name = cat.Name
                    });
                }
            }

            if (yamlModel.Operations != null)
            {
                foreach (var op in yamlModel.Operations)
                {
                    result.Operations.Add(new Operation
                    {
                        Id = op.Id,
                        Type = Enum.Parse<TransactionType>(op.Type, true),
                        BankAccountId = op.BankAccountId,
                        Amount = op.Amount,
                        Date = DateTime.Parse(op.Date),
                        Description = op.Description,
                        CategoryId = op.CategoryId
                    });
                }
            }

            return result;
        }
    }

    // Вспомогательная модель для десериализации YAML
    public class YamlModel
    {
        public List<YamlBankAccount> BankAccounts { get; set; }
        public List<YamlCategory> Categories { get; set; }
        public List<YamlOperation> Operations { get; set; }
    }

    public class YamlBankAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }

    public class YamlCategory
    {
        public int Id { get; set; }
        public string Type { get; set; }  // "Income" или "Expense"
        public string Name { get; set; }
    }

    public class YamlOperation
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int BankAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}