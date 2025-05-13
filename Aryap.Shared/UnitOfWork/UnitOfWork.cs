using Aryap.Shared.Repositories.Implementation;
using Aryap.Shared.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Aryap.Shared.Repositories;
//using Shared.Repositories.Interface;
//using Aryap.Shared.UnitOfWork;
using Aryap.Shared.Repositories.Interface;


namespace Aryap.Shared.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories[typeof(T)] = new Repository<T>(_context);
            }

            return (IRepository<T>)_repositories[typeof(T)];
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}