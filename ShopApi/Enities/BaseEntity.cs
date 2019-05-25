using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApi.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CreatedUserName { get; set; }
        public string ModifiedUserName { get; set; }

    }
}
