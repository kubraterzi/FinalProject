using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector :IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            //parametre olarak gelen, aspect e eklediğimiz type ın(örneğin, ProductManager gibi), MethodInterceptionBaseAttribute u inherit eden Attribute larının ve
            //birbirini inherit eden manager class larının olması durumunda da tamamının attribute larının listesini al

            var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            // İlgili class(type) ın, parametre olarak gelen metodunun, MethodInterceptionBaseAttribute ten inherit edilen tüm attribute larını ve aynı zamanda
            // birbirini inherit eden manager class larının olması durumunda da tamamının attribute larının listesini al
            
            classAttributes.AddRange(methodAttributes); // classAttribute listesinin sonuna methodAttributes ları da ekle (AddRange-> Mevcut listeye başka bir liste daha eklemek için kullanılır)

            return classAttributes.OrderBy(c => c.Priority).ToArray(); //Attribute verilirken, Priority özelliğinin de verilmesi durumunda, öncelik sıralamasını yap
       

        }
    }
}