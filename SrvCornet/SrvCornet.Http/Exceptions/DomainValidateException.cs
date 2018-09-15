using System;
using System.Collections.Generic;
using System.Text;

namespace SrvCornet.Http.Exceptions
{
    public class DomainValidateException : BaseCustomException
    {
        public DomainValidateException(List<string> errors) : base(errors, System.Net.HttpStatusCode.Forbidden)
        {

        }
    }
}
