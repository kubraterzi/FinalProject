using System;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { } // Attribute -> metodun başında çalıştır
        protected virtual void OnException(IInvocation invocation) { } // Attribute -> metot içerisinde hata anında çalıştır
        
        protected virtual void OnSuccess(IInvocation invocation) { } // Attribute ->  metot başarıyla tamamlandığında çalıştır
        protected virtual void OnAfter(IInvocation invocation) { } // Attribute -> metot sonunda çalıştır
        
        
        public override void Intercept(IInvocation invocation)
        {
            // Bir metodun nasıl ele alınacağı tasarlandı
            
            var isSuccess = true; // ilk etapta işlemin başarılı olduğunu varsay
            OnBefore(invocation); // OnBefore içerisi doluysa invocation(metodu) çalıştır
            try
            {
                invocation.Proceed();
            }
            catch (Exception e) // eğer hata olursa
            {
                isSuccess = false; // başarı durumunu false yap
                OnException(invocation); // OnException çalıştır
                throw; // hata fırlat
            }
            finally
            {
                if (isSuccess) // eğer başarı durumu hala true ise
                {
                    OnSuccess(invocation); // OnSuccess çalıştır
                }
            }
            
            OnAfter(invocation); // OnAfter çalıştır
        }
    }
}