using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApi.Common
{
    public class PageInfo
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public long TotalFilteredItemsCount { get; set; }
        public long TotalItemsCount { get; set; }
        public long TotalPagesCount { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPagesCount);
            }
        }
        public long FirstItemOnPage
        {

            get { return (PageIndex - 1) * PageSize + 1; }
        }

        public long LastItemOnPage
        {
            get { return Math.Min(PageIndex * PageSize, TotalItemsCount); }
        }
    }
}
