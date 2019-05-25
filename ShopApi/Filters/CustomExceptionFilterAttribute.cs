using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopApi.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        ///////// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
             HandleException(context);
            context.ExceptionHandled = true;
            base.OnException(context);
        }

        private static void HandleException(ExceptionContext context)
        {
            var exception = context.Exception;
            //if (exception is MyNotFoundException)
            //    SetExceptionResult(context, exception, HttpStatusCode.NotFound);
            //else if (exception is MyUnauthorizedException)
            //    SetExceptionResult(context, exception, HttpStatusCode.Unauthorized);
            //else if (exception is MyException)
            //    SetExceptionResult(context, exception, HttpStatusCode.BadRequest);
            //else
            SetExceptionResult(context, exception, HttpStatusCode.InternalServerError);
        }

        private static void SetExceptionResult(
            ExceptionContext context,
            Exception exception,
            HttpStatusCode code)
        {
            context.Result = new JsonResult(exception)
            {
                StatusCode = (int)code
            };
        }
    }
}
