using ShopApi.Common;
using ShopApi.Entities;
using System.Threading.Tasks;

namespace ShopApi.Services
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<PaginatedList<TEntity>> GetAsync(SearchInfo<TEntity> searchInfo = null, PageInfo pageInfo = null);
        Task<TEntity> GetAsync(object entityId);
        Task<bool> RemoveAsync(object entityId);
        Task<bool> RemoveAsync(TEntity entity);
        Task<bool> RemoveRangeAsync(TEntity[] arr);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> UpdateRangeAsync(TEntity[] arr);
    }
}
