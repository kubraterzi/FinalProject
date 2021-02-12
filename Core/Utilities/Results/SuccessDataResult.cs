using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult <T> : DataResult<T>
    {
        // Kullanıcı data ile birlikte mesaj görmek isterse
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {

        }

        // Kullanıcı yalnızca data yı görmek isterse
        public SuccessDataResult(T data) : base(data, true)
        {

        }

        // Kulllanıcı yalnızca durum mesajı görmek isterse
        public SuccessDataResult(string message) : base(default, true, message) //  buradaki default değeri, kullanıcının hiçbir data döndürmek istediği zaman yalnızca veri türünü döndürmesini sağlar.(int, List<Product>, string)
        {

        }

        // Kullanıcı yalnızca başarı durumunu görmek isterse
        public SuccessDataResult() : base(default, true)
        {

        }
    }
}
