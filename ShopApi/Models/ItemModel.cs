using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Models
{
    public class ItemModel:BaseModel
    {
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}
