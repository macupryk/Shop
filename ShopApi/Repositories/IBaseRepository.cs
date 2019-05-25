using ShopApi.Common;
using ShopApi.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        IQueryable<TEntity> EntitySet();
        Task<PaginatedList<TEntity>> GetAsync(SearchInfo<TEntity> sarchInfo = null, PageInfo pageInfo = null);
        Task<TEntity> GetAsync(object id);
        void Remove(TEntity entity);
        void Update(TEntity entity);
        void RemoveRange(TEntity[] arr);
        void UpdateRange(TEntity[] arr);
    }
}
