using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Constants;
using Core.Extensions;

namespace Business.BusinessAspects.Autofac
{
    //JWT
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation) // program başlamadan önce
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles(); // _httpContextAccessor içerisindeki kullanıcının(yoğunluk anında
                                                                                 // her kulanıcı için birer httpcontextaccessor üretiliyordu.) rolleri çek
                                                                                
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role)) // çekilen roller içerisinde, _role dizisinin içerisindeki rollerden biri varsa
                {
                    return; // programı sürdür
                }
            }
            throw new Exception(AspectMessages.AuthorizationDenied); // eğer o rol yokse hata gönder. (Yetkiniz yok.)
        }
    }
}