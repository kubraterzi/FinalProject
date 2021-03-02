using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace DataAccess.Concrete
{
    public class EfUserDal : EfEntityRepositoryBase<User,NorthwindContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from operationClaim in context.OperationClaims
                    join userOperationClaim in context.UserOperationClaims 
                        on operationClaim.Id equals userOperationClaim.Id
                    where userOperationClaim.UserId == user.Id
                    select new OperationClaim
                    {
                        Id = userOperationClaim.OperationClaimId,
                        Name = operationClaim.Name
                    };

                return result.ToList();
            }
            
        }
    }
}