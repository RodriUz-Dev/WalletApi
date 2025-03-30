using WalletApi.Domain.Interfaces;
using WalletApi.Infrastructure.Persistence;
using WalletApi.Infrastructure.Repositories;

namespace WalletApi.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if (_repositories.Keys.Contains(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepository<T> ?? throw new InvalidOperationException($"Repository for type {typeof(T).Name} not found.");
            }
            IRepository<T> repository = new Repository<T>(_context);
            _repositories.Add(typeof(T), repository);
            return repository;
        }



        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
