 
using ShopApi.Entities;
using ShopApi.Models;
using System.Threading.Tasks;
using ShopApi.Common;
using ShopApi.Services;

namespace ShopApi.Logic
{
   public class ItemLogic: BaseLogic<Item, ItemModel>,IItemLogic
    {
        IBaseService<Item> _service;
         public ItemLogic(IBaseService<Item> service  )
            : base(service  )
        {
            _service = service;
         }

        public async Task<ItemModel> SaveAsync(ItemModel model )
        {
            SearchInfo< Item> searchInfo = new SearchInfo<Item>();
            searchInfo.SearchPredicate = (x => x.Name == model.Name);

            var resultExist = await _service.GetAsync(searchInfo);
            if(resultExist.Count>0)
            {
                throw new AlreadyExistException( );
            }
            else
            {
               var result= await this.AddAsync(model);
               return result;
            }
        }
    }
}
