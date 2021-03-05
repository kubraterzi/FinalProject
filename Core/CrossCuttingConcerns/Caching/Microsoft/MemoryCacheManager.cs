using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions; //Regex
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    public class MemoryCacheManager : ICacheManager
    {
        // ADAPTER PATTERN -Adaptasyon Deseni
        // Bu class sayesinde .NetCore dan gelen aşağıdaki metotların her birini kendimize göre uyarlamış olduk.
        private IMemoryCache _memoryCache;

        public MemoryCacheManager()
        {
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>(); // İnjection dan aldık. (CoreModule.cs)
        }
        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _); // bu metot hem varolup olmama durumunu hem datayı döndürüyor. Bize sadece olma durumu lazım olduğu için
                                                         // out _ kullandık. bu geri dönüş tipi döndürmeyeceğini bildiriyor. C# ta kullanılan bir tekniktir.
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern) // çalışma anında bellekten silmeyi sağlıyor. (Reflection ile)
        {
            // MemoryCache, .Net in IMemoryCache inden implement edilmiş bir class tır.
            //EntriesCollection -> Microsoft' un bunu cache lediğinde cache datalarını topladığı alan
            
            var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", // Git bellekteki EntriesCollection ı bul.
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            
            var cacheEntriesCollection = cacheEntriesCollectionDefinition.GetValue(_memoryCache) as dynamic; // bulduğun EntriesCollection' da definition ı _memoryCache olanları bul 
            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>(); //bulduklarını bir listeye at.

            foreach (var cacheItem in cacheEntriesCollection) // her bir cache elemanını kes
            {
                ICacheEntry cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null); // bu kurala uyanları 
                cacheCollectionValues.Add(cacheItemValue);// cacheCollectionValues listesinde topla
            }

            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase); // pattern ın olması gereken özellikleri
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();
            // formata uyan elemanların key lerini keysTo Remove listesine ata

            foreach (var key in keysToRemove) // tek tek gez ve cache den sil
            {
                _memoryCache.Remove(key);
            }
        }
    }
}