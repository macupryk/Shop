using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Common;
using ShopApi.Logic;
using ShopApi.Models;

namespace ShopApi.Controllers
{

    [Route("api/[controller]")]
    public class ItemsController : BaseController
    {
        IItemLogic _Logic;
        public ItemsController(IItemLogic Logic)
        {
            _Logic = Logic;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _Logic.GetAsync();
            return GetResponse(result);
            
        }
        [HttpPost]
        [Route("{id}/buy")]
        [Authorize]
        public async Task<IActionResult> Buy(Int64 id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
 
            var result = await _Logic.GetAsync(id);
            if(result==null)
            {
                throw new Exception("This item is not exists ");
            }
            else
            {

                if(result.Stock>0)
                {
                    result.Stock -= 1;
                    await _Logic.UpdateAsync (result);
                }
                else
                {
                    throw new Exception("This item is not available now ");
                }
            }
            return GetResponse(HttpStatusCode.OK, "The order has been done");


        }
 
    }
}
