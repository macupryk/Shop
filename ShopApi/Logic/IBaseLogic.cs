using ShopApi.Common;
using ShopApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopApi.Logic 
{
    public interface IBaseLogic<TModel> where TModel : IBaseModel
    {
        Task<TModel> AddAsync(TModel entityModel);
        Task<PaginatedList<TModel>> GetAsync(SearchInfo<TModel> searchInfo = null, PageInfo pageInfo = null);
        Task<TModel> GetAsync(object entityId);
        Task<bool> RemoveAsync(object entityId);
        Task<bool> RemoveAsync(TModel entityModel);
        Task<TModel> UpdateAsync(TModel entityModel);
     }
}
