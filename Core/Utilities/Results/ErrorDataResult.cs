using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult <T> : DataResult<T>
    {
        // Kullanıcı data ile birlikte mesaj görmek isterse 
        public ErrorDataResult(T data, string message) : base(data, false,  message) // Varsayılan bool değeri - false - tur.
        {

        }

        // Kullanıcı yalnızca data yı görmek isterse
        public ErrorDataResult(T data) : base(data, false)
        {

        }

        // Kulllanıcı yalnızca durum mesajı görmek isterse
        public ErrorDataResult(string message) : base(default, false, message)
        {

        }

        // Kullanıcı yalnızca başarı durumunu görmek isterse
        public ErrorDataResult() : base(default, false)
        {

        }
    }
}
