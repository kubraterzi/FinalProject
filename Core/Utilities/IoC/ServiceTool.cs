using System;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Utilities.IoC
{
    public class ServiceTool
    {
        // Aspect ler, WepApi->Business->EfProductDal injection zincirinde yoktur. Aspect başka bir yapıdır. Aspect ler içerisindeki injectionları yakalayabilmek adına
        // hazırlanmış bir yapıdır. Bu araç sayesinde injection alt yapımıza ulaşıp service lerin tümünü build edebiliyoruz. .Net teki tüm service leri alarak api üzerinde
        // yapabildiğimiz injectionları bu araç sayesinde yapabiliyoruz.
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}