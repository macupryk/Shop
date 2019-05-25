using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShopApi.Repositories
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
