using System.Collections.Generic;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult();
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var result = _userDal.Get(u => u.Email == email); //dışarıdan gelen email i veritabanında arat
            if (result != null) // eğer arama sonucunda veri bulduysan, yani null değilse
            {
                return new ErrorDataResult<User>(result); // bu mail adresi daha önce kullanıldığı için alamayacağını bildir
            }
            return new SuccessDataResult<User>(result);
        }
    }
}