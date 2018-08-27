using Microsoft.AspNetCore.Mvc;
using SrvCornet.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserController.Controllers
{
    [Route("callback")]
    public class CallbackController : BaseRestController
    {
        [Route("facebook")]
        public void Facebook()
        {
            Console.WriteLine(HttpContext.Request.Host);
            Console.WriteLine(HttpContext.Request.Path);
            Console.WriteLine(HttpContext.Request.QueryString);
            var headers = HttpContext.Request.Headers;
            foreach(var header in headers)
            {
                Console.WriteLine(string.Format("{0}:{1}", header.Key, header.Value));
            }
        }
    }
}
