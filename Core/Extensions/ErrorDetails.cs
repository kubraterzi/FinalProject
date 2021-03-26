using System.Collections.Generic;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace Core.Extensions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }


    public class ValidationErrorDetails : ErrorDetails
    {
        // aşağıdaki ValidationFailure nesnesi YALNIZCA Validation error lar için yazılacağından onun Error class ı farklı, ama ErrorDetails class ından inherit edilmiş bir class olacak.
        public IEnumerable<ValidationFailure> Errors { get; set; }
    }
}