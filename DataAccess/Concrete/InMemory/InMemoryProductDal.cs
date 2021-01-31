using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product>
            {
                new Product{ProductID =1, CategoryID=1, ProductName="Chai", UnitsInStock= 39, UnitPrice=18},
                new Product{ProductID =2, CategoryID=2, ProductName="Chang", UnitsInStock= 17, UnitPrice=19},
                new Product{ProductID =3, CategoryID=3, ProductName="Ikura", UnitsInStock= 31, UnitPrice=31},
                new Product{ProductID =4, CategoryID=4, ProductName="Tofu", UnitsInStock= 22, UnitPrice=25},
                new Product{ProductID =5, CategoryID=5, ProductName="FiloMix", UnitsInStock= 14, UnitPrice=21},
                new Product{ProductID =6, CategoryID=6, ProductName="Pavlou", UnitsInStock= 13, UnitPrice=17}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //Product productToDelete=null;
            //foreach (var p in _products)
            //{
            //    if (p.ProductID == product.ProductID)
            //    {
            //        productToDelete = p;
            //    }
            //}

            //_products.Remove(productToDelete);

            Product productToDelete = _products.SingleOrDefault(p=> p.ProductID == product.ProductID);
            _products.Remove(productToDelete);

        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllByCategoryId(int categoryID)
        {
            return _products.Where(p=> p.CategoryID == categoryID).ToList();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p=> p.ProductID == product.ProductID);
            productToUpdate.ProductID = product.ProductID;
            productToUpdate.CategoryID = product.CategoryID;
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
