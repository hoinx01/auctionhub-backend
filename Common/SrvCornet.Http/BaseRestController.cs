using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SrvCornet.Http.Exceptions;

namespace SrvCornet.Http
{
    public abstract class BaseRestController : Controller
    {
        protected void ValidateInputModel()
        {
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelState in ModelState.Values)
                    foreach (var error in modelState.Errors)
                        errors.Add(error.ErrorMessage);
                throw new DomainValidateException(errors);
            }
        }
    }
}
