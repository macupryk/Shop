using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ShopApi.Models;
using System;
using System.Linq;
using System.Net;
 
namespace ShopApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
       
        /// <summary>
        /// 
        /// </summary>
        public ValidateModelFilterAttribute()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {

            if (actionContext.ModelState.IsValid == false)
            {
                ResponseModel<String> rd = new ResponseModel<string>();
                rd.StatusCode = (int)HttpStatusCode.BadRequest;
                rd.ValidationErrors = actionContext.ModelState.Values.SelectMany(v => v.Errors).Select(modelError => modelError.ErrorMessage).ToList();
                // actionContext.Result = new BadRequestObjectResult(rd);// actionContext.ModelState);
                actionContext.Result = new BadRequestObjectResult(rd);
            }

            if (actionContext.ActionArguments.Any(kv => kv.Value == null))
            {
                ResponseModel<String> rd = new ResponseModel<string>();
                rd.StatusCode = (int)HttpStatusCode.BadRequest;
                rd.MessageCode = "Arguments cannot be null";
                //actionContext.Result = new BadRequestObjectResult("Arguments cannot be null");

                actionContext.Result = new BadRequestObjectResult(rd);

            }
            base.OnActionExecuting(actionContext);
        }


    }

}
