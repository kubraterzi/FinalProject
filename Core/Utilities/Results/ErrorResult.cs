using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message) // Bu class ın varsayılan değeri - false - tur.
        {
            // Kod bloklarının boş olma sebebi, gerekli atamaların  inherit edildiği class içerisinde yapılacak olmasıdır.
        }

        // Kullanıcı yalnızca başarı durumunu görmek isterse
        public ErrorResult() : base(false)  
        {
            // Kod bloklarının boş olma sebebi, gerekli atamaların inherit edildiği class içerisinde yapılacak olmasıdır.
        }
    }
}
