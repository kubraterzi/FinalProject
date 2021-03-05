using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionScopeAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed(); // ilgili metodu çalıştır
                    transactionScope.Complete(); // işlemi tamamla
                }
                catch (Exception) // bir hatayla karşılaşma durumunda
                {
                    transactionScope.Dispose(); // işlemi geri al
                    throw; // hata fırlat
                }

            }

        }
    }
}
