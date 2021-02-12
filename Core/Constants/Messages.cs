using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Constants 
{
    // Bu class, yalnızca Northwind projesinin mesajları olduğu için Business katmanı içerisinde Contants(Araçlar) kalsöründe tanımlandı.
    public static class Messages
    {
        public static string Listed = "Listed!";
        public static string Added = "Added!";
        public static string Updated = "Updated!";        
        public static string Deleted = "Deleted!";
        public static string NameInvalid = "Invalid Name";
        public static string MaintenanceTime= "Maintenance Time";
        public static string InvalidEntry = "Invalid Entry";
    }
}
