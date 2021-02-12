using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IResult
    {
        bool SuccessStatus { get;  }
        string Message { get;  }
    }
}
