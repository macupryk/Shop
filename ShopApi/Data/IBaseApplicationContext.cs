using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopApi.Common;
using ShopApi.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Data
{
    public interface IBaseApplicationContext
    {
        Task<EntityEntry<T>> AddAsync<T>(T obj) where T : BaseEntity;
        EntityEntry<T> Remove<T>(T obj) where T : BaseEntity;
        void RemoveRange<T>(T[] arr) where T : BaseEntity;
        IQueryable<TEntity> EntitySet<TEntity>() where TEntity : BaseEntity;
        Task<PaginatedList<TEntity>> GetAsync<TEntity>(SearchInfo<TEntity> searchInfo = null, PageInfo pageInfo = null) where TEntity : BaseEntity;
        Task<TEntity> GetAsync<TEntity>(object id) where TEntity : BaseEntity;
        Task<int> SaveAsync();
        EntityEntry<T> Update<T>(T obj) where T : BaseEntity;
        void UpdateRange<T>(T[] arr) where T : BaseEntity;
    }
}
