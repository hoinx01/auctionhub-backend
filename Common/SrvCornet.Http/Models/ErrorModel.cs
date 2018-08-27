using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SrvCornet.Http.Models
{
    public class ErrorModel
    {
        public int StatusCode { get; set; }
        public List<string> Messages { get; set; }

        public ErrorModel(HttpStatusCode statusCode, List<string> messages)
        {
            StatusCode = (int) statusCode;
            Messages = messages;
        }
    }
}
