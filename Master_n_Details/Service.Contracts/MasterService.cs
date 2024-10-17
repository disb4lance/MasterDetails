using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Shared.DataTransferObjects;
using System.Diagnostics.Metrics;



namespace Service
{
    internal sealed class MasterService : IMasterService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly ILogRepository _logRepository;
        public MasterService(IRepositoryManager repositoryManager, ILogRepository logRepository,  IMapper mapper) { 
            _repository = repositoryManager;
            _mapper = mapper;
            _logRepository = logRepository;
        }


        public async Task<MasterDto> GetMasterByName(string number, bool trackChanges) { 
            var master = await _repository.Master.GetMasterByNumberAsync(number, trackChanges);
            var masterDto = _mapper.Map<MasterDto>(master);
            return masterDto;
        }

        private async Task<Master> GetMasterByNumberAndCheckIfItExists(string number, bool trackChanges) {
            var master = await _repository.Master.GetMasterByNumberAsync(number, trackChanges = false);
            if (master is null)
            {
                throw new MasterByNumberNotFound($"документ с номером {number} не найден");
            }
            return master;
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
            try
            {
                await _repository.BeginTransactionAsync();
                // Проверка уникальности номера документа
                var isDocumentNumberUnique = await _repository.Master.IsDocumentNumberUniqueAsync(master.number, trackchange: false);
                if (!isDocumentNumberUnique)
                {
                    throw new BusinessException("Номер документа уже существует.");
                }

                var masterEntity = _mapper.Map<Master>(master);
                _repository.Master.CreateMaster(masterEntity);
                await _repository.SaveAsync();
                await _repository.CommitTransactionAsync();
                var masterToReturn = _mapper.Map<MasterDto>(masterEntity);
                return masterToReturn;
            }
            catch (Exception ex)
            {
                // Откат транзакции
                await _repository.RollbackTransactionAsync();

                // Логирование ошибки
                await _logRepository.LogErrorAsync(ex.Message);

                throw;
            }
        }
        public async Task DeleteMasterAsync(Guid masterId, bool trackChanges) {
            var master = await GetMasterAndCheckIfItExists(masterId, trackChanges);
            _repository.Master.DeleteMaster(master);
            await _repository.SaveAsync();
        }
        public async Task UpdateMasterAsync(Guid masterId, MasterForUpdateDto masterForUpdate, bool trackChanges) {
            try
            {
                await _repository.BeginTransactionAsync();
                var isDocumentNumberUnique = await _repository.Master.IsDocumentNumberUniqueAsync(masterForUpdate.number, trackchange: false);
                if (!isDocumentNumberUnique)
                {
                    throw new BusinessException("Номер документа уже существует.");
                }
                var masterEntity = await GetMasterAndCheckIfItExists(masterId, trackChanges);
                _mapper.Map(masterForUpdate, masterEntity);
                await _repository.SaveAsync();
            }
            catch (Exception ex)
            {
                // Откат транзакции
                await _repository.RollbackTransactionAsync();

                // Логирование ошибки
                await _logRepository.LogErrorAsync(ex.Message);

                throw;
            }
        }

        private async Task<Master> GetMasterAndCheckIfItExists(Guid id, bool trackChanges)
        {
            var master = await _repository.Master.GetMasterAsync(id, trackChanges);
            if (master is null)
            {
                throw new MasterNotFound(id);
            }
            return master;
        }
    }
}
