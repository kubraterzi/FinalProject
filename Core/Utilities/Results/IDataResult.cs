using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; } // Her türde datayı döndürebilmesi için Generic yapıda tanımladık.(Product, int, string, List<>, Exception..)
    }
}
