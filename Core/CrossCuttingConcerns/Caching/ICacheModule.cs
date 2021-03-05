using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
    public interface ICacheManager
    {
        T Get<T>(string key); // cache den geri dönüş tipi olan verileri cache te tut (Get() e göre daha kullanışlıdır)
        object Get(string key); // object bütün veri türlerinin babası olduğu için bu şekilde de Get() yapılabilir
        void Add(string key, object value, int duration); // cache ekle
        bool IsAdd(string key); // cache de mevcut mu kontrol et
        void Remove(string key); // cache den uçur
        void RemoveByPattern(string pattern); // ilgili pattern ı uçur(ismi get ile başlayanları uçur, ya da isminde category olanları uçur.
                                              // Çalışma anında bellekten silmeyi sağlar
    }
}
