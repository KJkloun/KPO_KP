using System.Collections.Generic;
using System.Linq;
using Models;
using DataStore;

namespace Facades
{
    public class CategoryFacade
    {
        public void CreateCategory(Category category)
        {
            InMemoryDataStore.Categories.Add(category);
        }

        public void UpdateCategory(Category category)
        {
            var existing = InMemoryDataStore.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (existing != null)
            {
                existing.Name = category.Name;
                existing.Type = category.Type;
            }
        }

        public void DeleteCategory(int id)
        {
            InMemoryDataStore.Categories.RemoveAll(c => c.Id == id);
        }

        public List<Category> GetAllCategories()
        {
            return InMemoryDataStore.Categories;
        }
    }
}