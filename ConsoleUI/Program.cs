using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductTest();

            //CategoryTest();

            //CustomerTest();

            //OrderTest();

        }

        private static void OrderTest()
        {
            OrderManager orderManager = new OrderManager(new EfOrderDal());
            foreach (var order in orderManager.GetAll().Data)
            {
                Console.WriteLine(order.ShipCity);
            }
        }

        private static void CustomerTest()
        {
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            foreach (var customer in customerManager.GetAll().Data)
            {
                Console.WriteLine(customer.ContactName);
            }
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());


            //var result = productManager.Add(new Product { ProductName = "Sandwich", CategoryID = 3, UnitPrice = 13, UnitsInStock = 50 });
            //Console.WriteLine(result.Message);

            //var result = productManager.GetAll();
            //Console.WriteLine(result.Message);

            //var result = productManager.GetProductDetails();
            //if (result.SuccessStatus== true)
            //{
            //    foreach (var product in productManager.GetProductDetails().Data)
            //    {
            //        Console.WriteLine(product.ProductName + " / " + product.CategoryName);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine(result.Message);
            //}

            productManager.Add(new Product { ProductName= "aaa", CategoryID=3, UnitPrice=100, UnitsInStock=10 });

        }
    }
}
