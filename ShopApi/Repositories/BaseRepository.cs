using ShopApi.Common;
using ShopApi.Data;
using ShopApi.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
            where TEntity : BaseEntity
    {
        public IApplicationContext _context { get; set; }

        public BaseRepository(IApplicationContext context)
        {
            this._context = context;
        }


        #region CRUD
        public async Task AddAsync(TEntity entity)
        {
            await this._context.AddAsync(entity);

        }
        public void Update(TEntity entity)
        {
            this._context.Update(entity);

        }
        public void Remove(TEntity entity)
        {
            this._context.Remove<TEntity>(entity);
        }

        public void UpdateRange(TEntity[] arr)
        {
            this._context.UpdateRange(arr);

        }
        public void RemoveRange(TEntity[] arr)
        {
            this._context.RemoveRange<TEntity>(arr);
        }
        public async Task<TEntity> GetAsync(object id)
        {
            return await this._context.GetAsync<TEntity>(id);
        }
        public async Task<PaginatedList<TEntity>> GetAsync(SearchInfo<TEntity> sarchInfo = null, PageInfo pageInfo = null)
        {
            return await _context.GetAsync<TEntity>(sarchInfo, pageInfo);
        }

        public IQueryable<TEntity> EntitySet()
        {
            return _context.EntitySet<TEntity>();
        }


        #endregion


    }
}
