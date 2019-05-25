using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShopApi.Common;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : ControllerBase
    {
        #region Response 
        #region Success
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Data"></param>
        /// <param name="messageCode"></param>
        /// <param name="pageInfo"></param>
        /// <returns></returns>
        public IActionResult GetResponse<T>(T Data, string messageCode = null, PageInfo pageInfo = null)
        {
            ResponseModel<T> rd = new ResponseModel<T>();

            rd.MessageCode = messageCode;
            rd.Data = Data;
            rd.PageInfo = pageInfo;


            rd.StatusCode = (int)HttpStatusCode.OK;
            return StatusCode((int)rd.StatusCode, rd);
        }
        #endregion

        #region Error
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public IActionResult GetResponse<T>(Exception ex, string messageCode = null)
        {
            ResponseModel<T> rd = new ResponseModel<T>();
            rd.MessageCode = messageCode;
            rd.StatusCode = (int)HttpStatusCode.BadRequest;
            rd.Message = messageCode;
            rd.DeveloperMessage = ex.ToString();

            return BadRequest(rd);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="invalidModelState"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public IActionResult GetResponse<T>(ModelStateDictionary invalidModelState, string messageCode = null)
        {
            var validationErrorList = invalidModelState.Values.SelectMany(v => v.Errors).Select(modelError => modelError.ErrorMessage).ToList();
            return GetResponse<T>(validationErrorList, messageCode);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstErrors"></param>
        /// <param name="messageCode"></param>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public IActionResult GetResponse<T>(List<String> lstErrors,
            string messageCode = null,
            HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            ResponseModel<T> rd = new ResponseModel<T>();
            rd.MessageCode = messageCode;
            rd.StatusCode = (int)statusCode;

            rd.ValidationErrors = lstErrors;
            return StatusCode((int)statusCode, rd);
        }
        #endregion

        #region Success Or Errror
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="statusCode"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public IActionResult GetResponse<T>(
            HttpStatusCode statusCode,
            string messageCode = null)
        {
            ResponseModel<T> rd = new ResponseModel<T>
            {
                MessageCode = messageCode,
                StatusCode = (int)statusCode
            };

            return StatusCode((int)statusCode, rd);
        }
        #endregion
        #endregion

    
        
    }
}
