using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class ForbidenException : Exception
    {
        public ForbidenException(string message) : base(message)
        {
            
        }
    }
}
