using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService // ProductManager class ı tercih edeceğimiz veritabanı teknolojisini seçmemiz için yazıldı. Veritabanı teknolojilerini yönet(Birini seç)
    {
        IProductDal _productDal; // ProductDal class ı üzerinden implement ettiği(kalıtım verdiği), veritabanı teknolojilerinden biriymiş gibi simule ettiğimiz InMemory teknolojisine ulaşacağız.

        public ProductManager(IProductDal productDal) // ProductManager nesnesi ilk new lendiğinde çalışacak olan yapıcı metodumuz içerisinde, parametre olarak bir veritabanı teknolojisini vermesini kullanıcıya zorunlu kılıyoruz.
        {
            _productDal = productDal;
        }

        public List<Product> GetAll() 
        {
            return _productDal.GetAll();// ProductManager içerisinde bulunan GetAll() metodu içerisinde, aslında tercih edilen veritabanı teknolojisinin içerisinde bulunan GetAll() metodunu çağrıyoruz.
        }

        public List<Product> GetAllByCategoryID(int brandId)
        {
            return _productDal.GetAll(p=> p.CategoryID == brandId);
        }

        public List<Product> GetAllByUnitPrice(decimal min, decimal max)
        {
           return _productDal.GetAll(p=> p.UnitPrice >= min && p.UnitPrice<=max);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }
    }
}
