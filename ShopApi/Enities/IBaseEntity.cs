using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApi.Entities
{
    public interface IBaseEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
        string CreatedUserName { get; set; }
        string ModifiedUserName { get; set; }
    }
}
