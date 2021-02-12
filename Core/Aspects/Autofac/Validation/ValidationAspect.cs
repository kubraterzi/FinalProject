using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Core.Constants;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) // eğer gönderilen tip, IValidator dan implement edilmiş bir tip değil ise
            {
                throw new Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType; // eğer hata yoksa,  _validatorType-> parametre olarak gönderilen validatorType'tır.
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); // _validatorType (ProductValidator) bellekte new le (Reflection yöntemi ile)
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; //BaseType=> MethodInterception, Base Type içerisindeki generic yapıda bulunan nesne tipini çek(ilk indisteki ([0]) -> Product)
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // ilgili metodun parametrelerine git, veri tipi Product olan nesneyi(parametreyi/parametreleri) bul, listele

            foreach (var entity in entities) // listeyi tek tek gez
            {
                ValidationTool.Validate(validator, entity); // listedeki her bir elemana, Product için tasarladığımız validation kurallarını uygula
            }
        }
        
        // Eğer birden fazla aspect çalıştırmak istersek(OnException, OnAfter,OnSuccess), diğer aspect leri de doldurmalıyız. Ancak burada yapılan Validation
        // kuralları olduğu için, başında uygulamamız yeterlidir.
    }
}