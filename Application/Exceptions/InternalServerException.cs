using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string message) : base(message)
        {
            
        }
    }
}
