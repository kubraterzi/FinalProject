using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAllByCategoryID(int brandId);
        List<Product> GetAllByUnitPrice(decimal min, decimal max);
        List<ProductDetailDto> GetProductDetails();
        List<Product> GetAll(); // Seçilecek olan veritabanı türü içerisindeki GetAll() metodunu çağırmak için, ProductManager içerisinde implement edilmek üzere bir GetAll() metodu yazıyoruz.
    }
}
