using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApi.Models
{
    public interface IBaseModel
    {
        Int64 Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
        string CreatedUserName { get; set; }
        string ModifiedUserName { get; set; }
    }
}
