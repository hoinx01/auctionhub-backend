using System;
using System.Collections.Generic;
using System.Text;

namespace SrvCornet.Http.Exceptions
{
    public class NotFoundException : BaseCustomException
    {
        public NotFoundException(string message) : base(new List<string>() { message}, System.Net.HttpStatusCode.NotFound)
        {

        }
    }
}
