using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        // Kullanıcı hem başarı durumunu hem de durum mesajını görmek isterse
        public SuccessResult(string message) : base(true, message) // ilgili parametreleri inherit edildiği (kalıtıldığı) class ın ilgili constructor ına gönderir ve o constructor içerisindeki atamaları
            // çalıştırır. Zaten bu class Success(başarılı) olma durumu için tasarlandığı için, varsayılan bool değeri true dur. Bu sebeple parametreyi - true - olarak gönderiyoruz. 
        {
            // Kod bloklarının boş olma sebebi, gerekli atamaların inherit edildiği class içerisinde yapılacak olmasıdır.
        }

        // Kullanıcı yalnızca başarı durumunu görmek isterse
        public SuccessResult() :base(true) // Success(başarılı) olma durumu için tasarlanmış bir class olduğu için varsayılan bool değeri - true - dur.
        {
            // Kod bloklarının boş olma sebebi, gerekli atamaların inherit edildiği class içerisinde yapılacak olmasıdır.
        }
    }
}
