using Business.Abstract;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;

namespace Business.Concrete
{
    public class ProductManager : IProductService // ProductManager class ı tercih edeceğimiz veritabanı teknolojisini seçmemiz için yazıldı. Veritabanı teknolojilerini yönet(Birini seç)
    {
        IProductDal _productDal; // ProductDal class ı üzerinden implement ettiği(kalıtım verdiği), veritabanı teknolojilerinden biriymiş gibi simule ettiğimiz InMemory teknolojisine ulaşacağız.

        public ProductManager(IProductDal productDal) // ProductManager nesnesi ilk new lendiğinde çalışacak olan yapıcı metodumuz içerisinde, parametre olarak bir veritabanı teknolojisini vermesini kullanıcıya zorunlu kılıyoruz.
        {
            _productDal = productDal;
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(x => x.ProductID == productId), Messages.Listed);
        }

        [ValidationAspect(typeof(ProductValidator), Priority = 1)]
        public IResult Add(Product product)
        {
                _productDal.Add(product);
                return new SuccessResult(Messages.Added);

        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.Deleted); // Boş daha geçilebilirdi, hiçbir mesaj döndürmez, yalnızca bool dönüşünü verirdi.  - return new SuccessResult(); -
        }

        public IDataResult<List<Product>> GetAll()
        {
                return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

        public IDataResult<List<Product>> GetAllByCategoryID(int brandId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryID == brandId), Messages.Listed);
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            if (max <= 0 || min <= 0)
            {
                return new ErrorDataResult<List<Product>>(Messages.InvalidEntry);
            }
            else
            {
                return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max)); // Mesaj kullanmadan denedik, istersek mesaj verisi de girebiliriz.
            }
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {

            if (DateTime.Now.Hour == 03)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }

            else
            {
                return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
            }
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(); // İstersek mesaj ya da başarı durumunu da listeleyebilirdik. -  return new SuccessResult(Messages.Deleted);  -
        }
    }
}
