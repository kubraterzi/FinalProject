using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheRemoveAspect : MethodInterception
    {
        // Silme, güncelleme, ekleme gibi verinin tamamını etkileyen işlemlerde, cache deki veri değişeceği için, eski verileri cache ten temizlemek için yapılan bir class tır.
        private ICacheManager _cacheManager;
        private string _pattern;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation) // bu attribute ü ekleme,silme bve güncelleme metotlarının üzerine ekleyeceğimiz için, eğer bu
                                                                  // işlemler başarılı olursa cache temizleneceğinden OnSuccess zamanına operasyon yazdık.
        {
            _cacheManager.RemoveByPattern(_pattern);
        }
    }
}