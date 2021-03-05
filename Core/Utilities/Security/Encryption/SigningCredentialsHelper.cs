using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        // Bir kullanıcının, bir sisteme giriş yapabilmek adına elinde bulunan donelerin tamamı Credentials tır.
        // Biz kendi  parola hash leme işlemimizi yaptık, ancak WebApi de aldığı token ı doğrulayabilmesi adına aynı yapıya ihtiyaç duyar. SigningCredentials bu sebeple oluşturulur.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            var result= new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512); // gelen security yi HmacSha512 algoritması ile doğruluyor.
            return result; 
        }
        
        // burada bir token işlemi yürütülecek, bu işlemde kullanılacak anahtar -> securityKey, algoritma -> HmacSha512 dir.
    }
}