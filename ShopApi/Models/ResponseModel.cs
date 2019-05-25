using ShopApi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  ShopApi.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {

        }

        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public string DeveloperMessage { get; set; }
        public List<string> ValidationErrors { get; set; }
        public PageInfo PageInfo { get; set; }

    }

}
