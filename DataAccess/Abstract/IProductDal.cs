using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal // Herhangi bir veritabanı teknolojisinde, veritabanı işlemlerini yapmamızı sağlayacak bir yapı oluşturduk. Hangi veritabanı teknolojisini seçersek
        // seçelim, tek yapmamız gereken şey bu interface i implemente edip bu metotların gövdelerini, ilgili teknolojinin kodlama teknikleri ile doldurmak olacak.
    {
        List<Product> GetAll();
        void Add(Product product);
        void Delete(Product product);
        void Update(Product product);
        List<Product> GetAllByCategoryId(int categoryID);

    }
}
