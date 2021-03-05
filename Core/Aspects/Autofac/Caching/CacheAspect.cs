using System.Linq;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration=60)
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            // invocation.Method.ReflectedType.FullName.invocation.Method.Name => Northwind.Business.IProductService.GetAll() => Reflections
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            var arguments = invocation.Arguments.ToList();
            var key = $"{methodName}({string.Join(",", arguments.Select(arg => arg?.ToString() ?? "<Null>"))})";

            if (_cacheManager.IsAdd(key)) // eğer hazırladığımız key cache de varsa
            {
                invocation.ReturnValue = _cacheManager.Get(key); // geri dönüş değerini cache ten getir(ReturnValue)
                return; // çalışmayı devam ettir
            }
            
            invocation.Proceed(); // eğer cache de yoksa, veritabanından çek ve çalıştır
            _cacheManager.Add(key, invocation.ReturnValue, _duration); // yeni key i cache e ekle
            
        }
    }
}