using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper 
    { // hashleme aracı - yalnızca araç olarak kullanacağımız için herhangi bir interface implement edilmedi. Metotlar static olacak.
        
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) // out anahtar kelimesi sayesinde return etmek zorunda kalmadan,
                                                                                                                 // referansı üzerinden değerine ulaşabiliyoruz.
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512()) 
            {
                passwordSalt = hmac.Key; // hmac te bulunan key i, salt değeri olarak vereceğiz. Kullanılan algoritaya göre anlık olarak üretildiği için, güvenlidir.
                                         // Burada istenilen key verilebilirdi, ancak ileride çözmek(isteğe göre) için kullanılacağından unutulmamalıdır.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));//ComputeHash byte array üzerinden işlem yapabildiği için, ilgili password ün byte değerini almalıyız
            }
        }

        // kullanıcıdan gelen password hash inin, bizim veritabanımızdaki pasword hash i ile örtüşüp örtüşmediğine bakıyor. Yine aynı passwordSalt kullanılarak hash leniyor.
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt) //yukarıda out dediğimiz için doğrudan veri tabanımızdaydı, burada da veritabanından ilgili verileri çekiyor
        {
            using (var hmac= new System.Security.Cryptography.HMACSHA512(passwordSalt)) // HMACSHA512 class ı iki construtor a sahip, hem parametresiz, hem byte[] türünden
                                                                                        // key isteyen parametreli
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) // burada hash lenen password, out ile referansta tuttuğumuz passwordHash ile eşleşiyor mu kontrol ediyor.
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
   
}