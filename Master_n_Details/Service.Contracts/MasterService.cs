using AutoMapper;
using Contracts;
using Entities.Models;
using Shared.DataTransferObjects;



namespace Service
{
    internal sealed class MasterService : IMasterService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public MasterService(IRepositoryManager repositoryManager, IMapper mapper) { 
            _repository = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MasterDto>> GetAllMastersAsync(bool trackChanges) {
            var masters = await _repository.Master.GetAllMastersAsync(trackChanges);
            var mastersDto = _mapper.Map<IEnumerable<MasterDto>>(masters);
            return mastersDto;
        }
        public async Task<MasterDto> GetMasterAsync(Guid masterId, bool trackChanges){
            var master = await GetMasterAndCheckIfItExists(masterId, trackChanges);
            var masterDto = _mapper.Map<MasterDto>(master);
            return masterDto;
        }
        public async Task<MasterDto> CreateMasterAsync(MasterForCreatingDto master) {
            var masterEntity = _mapper.Map<Master>(master);
            _repository.Master.CreateCompany(masterEntity);
            await _repository.SaveAsync();
            var masterToReturn = _mapper.Map<MasterDto>(masterEntity);
            return masterToReturn;
        }
        public async Task DeleteMasterAsync(Guid masterId, bool trackChanges) {
            var master = await GetMasterAndCheckIfItExists(masterId, trackChanges);
            _repository.Master.DeleteCompany(master);
            await _repository.SaveAsync();
        }
        public async Task UpdateMasterAsync(Guid masterId, MasterForUpdateDto masterForUpdate, bool trackChanges) {
            var masterEntity = await GetMasterAndCheckIfItExists(masterId, trackChanges);
            _mapper.Map(masterForUpdate, masterEntity);
            await _repository.SaveAsync();
        }

        private async Task<Master> GetMasterAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var master = await _repository.Master.GetMasterAsync(id, trackChanges);
            if (master is null)
            {
                //throw new CompanyNotFoundException(id);
            }
            return master;
        }
    }
}
