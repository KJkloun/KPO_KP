using System;
using Facades;
using Factories;
using Models;

namespace Commands
{
    public class CreateCategoryCommand : ICommand
    {
        private readonly CategoryFacade _categoryFacade;
        private readonly int _id;
        private readonly TransactionType _type;
        private readonly string _name;

        public CreateCategoryCommand(CategoryFacade categoryFacade, int id, TransactionType type, string name)
        {
            _categoryFacade = categoryFacade;
            _id = id;
            _type = type;
            _name = name;
        }

        public void Execute()
        {
            try
            {
                var category = FinancialEntityFactory.CreateCategory(_id, _type, _name);
                _categoryFacade.CreateCategory(category);
                Console.WriteLine("Категория создана успешно.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка создания категории: {ex.Message}");
            }
        }
    }
}