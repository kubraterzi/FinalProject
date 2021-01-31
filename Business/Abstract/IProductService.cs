using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll(); // Seçilecek olan veritabanı türü içerisindeki GetAll() metodunu çağırmak için, ProductManager içerisinde implement edilmek üzere bir GetAll() metodu yazıyoruz.
    }
}
