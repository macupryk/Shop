using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShopApi.Entities;
using ShopApi.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ShopApi.Data
{
    public class BaseApplicationContext : IdentityDbContext,     IBaseApplicationContext
    {
        public BaseApplicationContext(DbContextOptions<ApplicationContext> options)
           : base(options)
        {
        }

        #region CRUD
        public Task<EntityEntry<T>> AddAsync<T>(T obj) where T : BaseEntity
        {
            return base.Set<T>().AddAsync(obj);
        }

        public new EntityEntry<T> Update<T>(T obj) where T : BaseEntity
        {
            return base.Set<T>().Update(obj);
        }
        public void UpdateRange<T>(T[] arr) where T : BaseEntity
        {
            base.Set<T>().UpdateRange(arr);
        }

        public new EntityEntry<T> Remove<T>(T obj) where T : BaseEntity
        {
            return base.Set<T>().Remove(obj);
        }
        public void RemoveRange<T>(T[] arr) where T : BaseEntity
        {
            base.Set<T>().RemoveRange(arr);
        }

        public Task<int> SaveAsync()
        {
            return base.SaveChangesAsync();
        }

        public Task<TEntity> GetAsync<TEntity>(object id) where TEntity : BaseEntity
        {
            return base.Set<TEntity>().FindAsync(id);
        }

        public async Task<PaginatedList<TEntity>> GetAsync<TEntity>(SearchInfo<TEntity> searchInfo = null, PageInfo pageInfo = null) where TEntity : BaseEntity
        {
            try
            {


                var query = EntitySet<TEntity>().AsNoTracking();
                //get count of total items in database 
                if (pageInfo != null) {
                    pageInfo.TotalItemsCount = query.Count();
                    pageInfo.TotalFilteredItemsCount = pageInfo.TotalItemsCount;
                }
                if (searchInfo != null)
                {
                    //1-filter date 
                    if (searchInfo.SearchPredicate != null)
                    {
                        query = query.Where(searchInfo.SearchPredicate);
                        if (pageInfo != null) pageInfo.TotalFilteredItemsCount = query.Count();
                    }
                    if (searchInfo.SelectPredicate != null)
                    {
                        query = query.Select(searchInfo.SelectPredicate);
                    }
                    if (searchInfo.listOfInclude != null)
                    {
                        foreach (string path in searchInfo.listOfInclude)
                        {
                            query = query.Include(path);
                        }
                    }
                    //sort data
                    if (searchInfo.SortFields != null && searchInfo.SortFields.Count > 0)
                    {
                        StringBuilder orderSB = new StringBuilder();
                        foreach (var sortField in searchInfo.SortFields)
                        {
                            if (orderSB.Length > 0)
                            {
                                orderSB.Append(",");
                            }
                            if (sortField.SortType == SortTypes.DESC)
                            {
                                orderSB.Append(sortField.FieldName + " " + SortTypes.ASC.ToString()); ;
                            }
                            else
                            {
                                orderSB.Append(sortField.FieldName + " " + SortTypes.DESC.ToString()); ;
                            }

                        }
                        query = query.OrderBy(orderSB.ToString());
                    }
                }

                if (pageInfo != null
                    && pageInfo.PageIndex > 0
                    && pageInfo.PageSize > 0)
                {
                    query = query.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize);
                }
              
                var result = await query.ToListAsync();
                var pagingResult = new PaginatedList<TEntity>(result, pageInfo);
                return pagingResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public IQueryable<TEntity> EntitySet<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        #endregion




    }
}
