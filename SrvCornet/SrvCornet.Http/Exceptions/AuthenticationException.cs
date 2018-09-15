using System;
using System.Collections.Generic;
using System.Text;

namespace SrvCornet.Http.Exceptions
{
    public class AuthenticationException : BaseCustomException
    {
        public AuthenticationException(string message) : base(new List<string>() { message }, System.Net.HttpStatusCode.Unauthorized)
        {

        }
    }
}
