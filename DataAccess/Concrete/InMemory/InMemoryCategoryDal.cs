using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCategoryDal : ICategoryDal
    {
        List<Category> _categories;
        public InMemoryCategoryDal()
        {
            _categories = new List<Category>
            {
                new Category{CategoryID = 1, CategoryName = "Beverages" },
                new Category{CategoryID = 2, CategoryName= "Condiments"},
                new Category{CategoryID = 3 , CategoryName= "SeaFood"},
                new Category{CategoryID = 4 , CategoryName= "Coffee"}
            };
        }

        public void Add(Category category)
        {
            _categories.Add(category);
        }

        public void Delete(Category category)
        {
            Category categoryToDelete = _categories.SingleOrDefault(c=> c.CategoryID == category.CategoryID);
            _categories.Remove(categoryToDelete);
        }

        public List<Category> GetAll()
        {
            return _categories;
        }

        public void Update(Category category)
        {
            Category categoryToUpdate = _categories.SingleOrDefault(c => c.CategoryID == category.CategoryID);

            categoryToUpdate.CategoryID = category.CategoryID;
            categoryToUpdate.CategoryName = category.CategoryName;
        }
    }
}
