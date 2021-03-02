using System.Data.Common;
using Castle.DynamicProxy;

namespace Core.Entities.Concrete
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; } // tabloda karşılığı varbinary(500) -> ikilik düzende olacağı için
        public byte[] PasswordSalt { get; set; }
        public bool Status { get; set; } // tablodaki karşılığı bit -> true, false döneceği için
    }
}