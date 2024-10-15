

using AutoMapper;
using Contracts;


namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMasterService> _masterService;
        private readonly Lazy<IDetailService> _detailService;

        public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _masterService = new Lazy<IMasterService>(() => new MasterService(repositoryManager, mapper));
            _detailService = new Lazy<IDetailService>(() => new DetailService(repositoryManager, mapper));
        }
        public IMasterService MasterService => _masterService.Value;
        public IDetailService DetailService => _detailService.Value;
    }
}
