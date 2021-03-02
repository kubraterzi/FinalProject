using System.Collections.Generic;
using Core.Entities.Concrete;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        // Girilen kullanıcı adı ve parola doğruysa, veritabanına giderek claimlerini bulacak ve jsonwebtoken üretecek.
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}