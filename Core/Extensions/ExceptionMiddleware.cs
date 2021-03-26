using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task
            InvokeAsync(
                HttpContext httpContext) //api de bir istekte bulunulduğunda herhangi bir metot çağırıldığında, bu çaprı neticesinde tüm sistem bu bloklardan geçiyor. 
        {
            //tüm kodları try/catch içerisine alıyor.
            try // eğer hata yoksa devam et
            {
                await _next(httpContext);
            }
            catch (Exception e) // ama eğer hata varsa handle et
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        // eğer çalışan sistemde bir hata varsa, o hata incelemeye alınıyor.
        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            string message = "Internal Server Error";
            IEnumerable<ValidationFailure> errors;

            //validation hatası varsa aşağıdaki kodlar çalışacak
            
            if (e.GetType() == typeof(ValidationException)) 
            // eğer gelen hata validation hatası ise mesajı aşağıdaki ile değiştir.
            {
                message = e.Message;
                errors = ((ValidationException) e).Errors;
                httpContext.Response.StatusCode = 400;
                
                return httpContext.Response.WriteAsync(new ValidationErrorDetails // Validation a uygun olan bir hata nesnesi oluşturduk
                {
                    //oluşturduğumuz nesnenin içerisine aşağıdaki bilgileri atadık
                    StatusCode = 400,
                    Message = message,
                    Errors = errors
                }.ToString());
            }
            

            // sistemsel hata varsa aşağıdaki kodlar çalışacak
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}