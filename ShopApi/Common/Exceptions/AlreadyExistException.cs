using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApi.Common
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException( ) : base("This item already exists")
        {
        }

        public AlreadyExistException(string message) : base(message)
        {
        }
    }
}
