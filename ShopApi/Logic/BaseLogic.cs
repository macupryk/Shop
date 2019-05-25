 using ShopApi.Common;
using ShopApi.Entities;
using ShopApi.Models;
using ShopApi.Services;
using System;
 
using System.Threading.Tasks;

namespace ShopApi.Logic 
{
    public   class BaseLogic<TEntity, TModel> : IBaseLogic<TModel>
         where TModel : BaseModel
         where TEntity : BaseEntity
    {
        readonly IBaseService<TEntity> _service;
        public BaseLogic(IBaseService<TEntity> service)
        {
            this._service = service;

        }
        public async Task<TModel> AddAsync(TModel entityModel)
        {
            TEntity entity = AutoMapper.Mapper.Map<TModel, TEntity>(entityModel);
            TEntity newEntity  =await this._service.AddAsync(entity);

            var  result= AutoMapper.Mapper.Map<TEntity, TModel>(entity);
            return result;
        }

        public async Task<PaginatedList<TModel>> GetAsync(SearchInfo<TModel> searchInfoModel = null, PageInfo pageInfo = null)
        {
            PaginatedList < TModel> result = default(PaginatedList<TModel>);

            SearchInfo<TEntity> searchInfoEntity = AutoMapper.Mapper.Map<SearchInfo<TModel>, SearchInfo<TEntity>>(searchInfoModel);

            PaginatedList<TEntity> entityList = await this._service.GetAsync(searchInfoEntity, pageInfo);
            if (entityList != null)
            {
                  result = AutoMapper.Mapper.Map<PaginatedList<TEntity >, PaginatedList<TModel>>(entityList);
                   result.PageInfo = entityList.PageInfo;
            }
            return result;
        }

        public async Task<TModel> GetAsync(object entityId)
        {
            TEntity entity = await this._service.GetAsync(entityId);
            TModel Model = default(TModel);

            if (entity != null)
            {
                Model = AutoMapper.Mapper.Map<TEntity, TModel>(entity);

            }
            return Model;

        }

        public async Task<bool> RemoveAsync(object entityId)
        {
            TEntity originalEntity = await this._service.GetAsync(entityId);
            var result = await this._service.RemoveAsync(originalEntity);
            return result;
        }

        public async Task<bool> RemoveAsync(TModel entityModel)
        {
            var entity = AutoMapper.Mapper.Map<TModel, TEntity>(entityModel);
            var result = await this._service.RemoveAsync(entity);
            return result;
        }

        

        public  async Task<TModel> UpdateAsync(TModel entityModel)
        {
            TEntity originalEntity = await this._service.GetAsync(entityModel.Id);
            if(originalEntity==null)
            {
                throw new Exception("This item is not exist");
            }
            TEntity newEntity  = AutoMapper.Mapper.Map<TModel, TEntity>(entityModel, originalEntity);
            newEntity= await this._service.UpdateAsync(newEntity );
            var result= AutoMapper.Mapper.Map<TEntity , TModel>(newEntity);
            return result;
        }

        
    }
}
