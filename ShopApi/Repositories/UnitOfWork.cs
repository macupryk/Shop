using ShopApi.Data;
using System.Threading.Tasks;

namespace ShopApi.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationContext context;
        public UnitOfWork(IApplicationContext _context)
        {
            context = _context;
        }
        public async Task<int> CommitAsync()
        {
            return await context.SaveAsync();
        }

    }
}
