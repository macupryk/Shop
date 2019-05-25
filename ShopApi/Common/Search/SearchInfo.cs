using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ShopApi.Common
{/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
    public class SearchInfo<T>
    {
        public Expression<Func<T, T>> SelectPredicate { get; set; }
        public Expression<Func<T, bool>> SearchPredicate { get; set; }
        public IList<SortInfo> SortFields { get; set; }

        public List<string> listOfInclude { get; set; }

    }

}
