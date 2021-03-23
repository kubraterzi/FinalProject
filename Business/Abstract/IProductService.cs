using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAllByCategoryID(int categoryId);
        IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<List<Product>> GetAll(); // Seçilecek olan veritabanı türü içerisindeki GetAll() metodunu çağırmak için, ProductManager içerisinde implement edilmek üzere bir GetAll() metodu yazıyoruz.
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Update(Product product);
        IResult Delete(Product product);

        IResult TransactionalOperation(Product product);
    }
}
