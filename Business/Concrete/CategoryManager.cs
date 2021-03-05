using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }



        [CacheAspect]
        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll()); // İstersek listelendi mesajı yazabiliriz. -   return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(), Messages.Listed); -
        }




        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Add(Category category)
        {
            if (category.CategoryName.Length <= 5)
            {
                return new ErrorResult(Messages.NameInvalid);
            }
            else
            {
                _categoryDal.Add(category);
                return new SuccessResult(Messages.Added);
            }
        }




        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Delete(Category category)
        {
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.Deleted);
        }




        [CacheRemoveAspect("ICategoryService.Get")]
        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.Updated);
        }
    }
}
