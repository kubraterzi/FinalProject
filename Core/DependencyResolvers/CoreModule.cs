using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Core.Aspects.Autofac.Caching;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<Stopwatch>(); 
            serviceCollection.AddMemoryCache(); // .NetCore dan geliyor. _memoryCache in karşılığı olacak.
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>(); // buraya daha sonra Redis eklemek istersek, yine ICacheManager dan implement edip
                                                                                 // MemoryCacheManager yerine RedisCacheManager yazacağız ve yukarıdaki servis
                                                                                 // sağlayıcını kaldıracağız. çünkü o .Net in kendi sağlayıcının instance ını
                                                                                 // oluşturuyor.
                                                                                 
                                                                                 
        }
    }
}
