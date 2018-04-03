using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshelf.Exceptions
{
    public class ApiConnectionException : Exception
    {
        public ApiConnectionException(string message) : base(message)
        {
        }

        public ApiConnectionException() : base()
        {

        }
    }
}