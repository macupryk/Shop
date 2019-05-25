using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApi.Models
{
    public class BaseModel : IBaseModel
    {
        public BaseModel()
        {
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }
    }
}
