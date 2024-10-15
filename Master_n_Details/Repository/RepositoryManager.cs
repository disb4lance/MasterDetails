using Contracts;


namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IMasterRepository>_masterRepository;
        private readonly Lazy<IDetailRepository> _detailRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _masterRepository = new Lazy<IMasterRepository>(() => new
            MasterRepository(repositoryContext));
            _detailRepository = new Lazy<IDetailRepository>(() => new
            DetailRepository(repositoryContext));
        }
        public IMasterRepository Master => _masterRepository.Value;
        public IDetailRepository Detail => _detailRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}

