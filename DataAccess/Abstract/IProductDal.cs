using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal :IEntityRepository<Product>
        // Herhangi bir veritabanı teknolojisinde, veritabanı işlemlerini yapmamızı sağlayacak bir yapı oluşturduk. Hangi veritabanı teknolojisini seçersek
        // seçelim, tek yapmamız gereken şey bu interface i implemente edip bu metotların gövdelerini, ilgili teknolojinin kodlama teknikleri ile doldurmak olacak.
    {
        List<ProductDetailDto> GetProductDetails();
    }
}
