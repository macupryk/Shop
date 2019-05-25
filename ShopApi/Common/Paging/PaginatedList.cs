using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ShopApi.Common
{
    public class PaginatedList<T> : List<T>
    {
        public PageInfo PageInfo { get;   set; }
        public PaginatedList()
        {
        }
        public PaginatedList(List<T> source, PageInfo pageInfo)
        {
            PageInfo = pageInfo;
            if (source != null)
            {
                if (pageInfo != null)
                {
                    PageInfo.TotalItemsCount = pageInfo.TotalItemsCount;
                    PageInfo.TotalPagesCount = (int)Math.Ceiling(PageInfo.TotalItemsCount / (double)PageInfo.PageSize);
                }
                this.AddRange(source);
            }

        }

    }

}
