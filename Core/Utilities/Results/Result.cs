using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //Kullanıcı hem başarı durumunu hem de durum mesajını görmek isterse
        public Result(bool success, string message) : this(success) // Bu class ta tek parametreli olan constructor a succes parametresini göndererek, diğer constructor ı tetikler. Böylelikle iki kez 
            //Success=success;  ataması gerekmeksizin, diğer metodun içeriğinde atamayı çeker. Aslında constructor içerisinde constructor çalıştırmış olur.
        {
            Message = message;
        }

        // Kullanıcı yalnızca başarı durumunu görmek isterse
        public Result(bool success)
        {
            SuccessStatus = success;
        }

        // Properties Implementasyonu
        public bool SuccessStatus { get; }

        public string Message { get; }
    }
}
