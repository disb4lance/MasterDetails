using Contracts;
using Microsoft.EntityFrameworkCore;


namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IMasterRepository>_masterRepository;
        private readonly Lazy<IDetailRepository> _detailRepository;
        private readonly ILogRepository _logRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _masterRepository = new Lazy<IMasterRepository>(() => new
            MasterRepository(repositoryContext));
            _detailRepository = new Lazy<IDetailRepository>(() => new
            DetailRepository(repositoryContext));
            _logRepository = new LogRepository(repositoryContext);
        }
        public IMasterRepository Master => _masterRepository.Value;
        public IDetailRepository Detail => _detailRepository.Value;
        public ILogRepository Log => _logRepository;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

        public async Task BeginTransactionAsync()
        {
            await _repositoryContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            await _repositoryContext.Database.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _repositoryContext.Database.RollbackTransactionAsync();
        }
    }
}

