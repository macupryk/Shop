 using ShopApi.Common;
using ShopApi.Entities;
using ShopApi.Repositories;
using System;
using System.Threading.Tasks;

namespace ShopApi.Services
{
    public   class BaseService<TEntity> : IBaseService<TEntity>
         where TEntity : BaseEntity

    {

        readonly IBaseRepository<TEntity> entityRepository;
        readonly IUnitOfWork unitOfWork;

        public BaseService(IBaseRepository<TEntity> entityRepository,
             IUnitOfWork unitOfWork)
        {
            this.entityRepository = entityRepository;
            this.unitOfWork = unitOfWork;
        }
        #region CRUD
        public async Task<TEntity> GetAsync(object entityId)
        {
            TEntity entity = await this.entityRepository.GetAsync(entityId);
            return entity;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await this.entityRepository.AddAsync(entity);
            await this.unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            this.entityRepository.Update(entity);
            await this.unitOfWork.CommitAsync();
            return entity;
        }
        public async Task<Boolean> RemoveAsync(TEntity entity)
        {
            if (entity != null)
            {
                this.entityRepository.Remove(entity);
                await this.unitOfWork.CommitAsync();
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<Boolean> RemoveAsync(object entityId)
        {
            TEntity originalentity = await this.entityRepository.GetAsync(entityId);
            if (originalentity != null)
            {
                this.entityRepository.Remove(originalentity);
                await this.unitOfWork.CommitAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<bool> UpdateRangeAsync(TEntity[] arr)
        {
            this.entityRepository.UpdateRange(arr);
            await this.unitOfWork.CommitAsync();
            return true;

        }
        public async Task<bool> RemoveRangeAsync(TEntity[] arr)
        {
            this.entityRepository.RemoveRange(arr);
            await this.unitOfWork.CommitAsync();
            return true;

        }


        public async Task<PaginatedList<TEntity>> GetAsync(SearchInfo<TEntity> searchInfo = null, PageInfo pageInfo = null)
        {
            PaginatedList<TEntity> entityList = await this.entityRepository.GetAsync(searchInfo, pageInfo);
            return entityList;
        }

        #endregion
    }
}
