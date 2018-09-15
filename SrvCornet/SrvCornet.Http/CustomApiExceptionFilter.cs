using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using SrvCornet.Http.Exceptions;
using SrvCornet.Http.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SrvCornet.Http
{
    public class CustomApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            if (exception is BaseCustomException customException)
            {
                context.HttpContext.Response.StatusCode = (int)customException.StatusCode;
                context.Result = new JsonResult(new ErrorModel(customException.StatusCode, customException.Messages));
            }
            else
            {
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(
                    new ErrorModel(
                        HttpStatusCode.InternalServerError,
                        new List<string>()
                        {
                            exception.Message ?? "Có lỗi xảy ra"
                        }
                    )
                );
            }
            base.OnException(context);
        }
    }
}
