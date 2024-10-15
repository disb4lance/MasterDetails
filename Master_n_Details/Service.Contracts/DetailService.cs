

using AutoMapper;
using Contracts;
using Entities.Models;
using Shared.DataTransferObjects;
using System.ComponentModel.Design;

namespace Service
{
    internal sealed class DetailService : IDetailService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public DetailService(IRepositoryManager repositoryManager, IMapper mapper) { 
            _repository = repositoryManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DetailDto>> GetDetailsAsync(Guid masterId, bool trackChanges) {
            await CheckIfMasterExists(masterId, trackChanges);
            var details = await _repository.Detail.GetDetailsAsync(masterId, trackChanges);
            var detailsDto = _mapper.Map<IEnumerable<DetailDto>>(details);
            return detailsDto;
        }
        public async Task<DetailDto> GetDetailAsync(Guid masterId, Guid id, bool trackChanges) {
            await CheckIfMasterExists(masterId, trackChanges);
            var detailDb = await _repository.Detail.GetDetailAsync(masterId, id, trackChanges);
            if (detailDb == null)
            {
                //throw new EmployeeNotFoundException(id);
            }

            var detail = _mapper.Map<DetailDto>(detailDb);
            return detail;
        }
        public async Task<DetailDto> CreateDetailForMasterAsync(Guid masterId, DetailForCreatingDto detailForCreation, bool trackChanges) {
            await CheckIfMasterExists(masterId, trackChanges);
            var datailEntity = _mapper.Map<Detail>(detailForCreation);
            _repository.Detail.CreateDetailForMaster(masterId, datailEntity);
            await _repository.SaveAsync();
            var detailToReturn = _mapper.Map<DetailDto>(datailEntity);
            return detailToReturn;
        }
        public async Task DeleteDetailForMasterAsync(Guid masterId, Guid id, bool trackChanges) {
            await CheckIfMasterExists(masterId, trackChanges);
            var detailDb = await GetDetailForMasterAndCheckIfItExists(masterId, id, trackChanges);
            _repository.Detail.DeleteDetail(detailDb);
            await _repository.SaveAsync();
        }
        public async Task UpdateDetailForMasterAsync(Guid masterId, Guid id, DetailForUpdateDto detailForUpdate, bool masterTrackChanges, bool detailTrackChanges) {
            await CheckIfMasterExists(masterId, masterTrackChanges);
            var detailDb = await GetDetailForMasterAndCheckIfItExists(masterId, id, detailTrackChanges);
            _mapper.Map(detailForUpdate, detailDb);
            await _repository.SaveAsync();
        }

        private async Task CheckIfMasterExists(Guid masterId, bool trackChanges)
        {
            var master = await _repository.Master.GetMasterAsync(masterId, trackChanges);
            if (master is null)
            {
                //throw new CompanyNotFoundException(companyId);
            }
        }

        private async Task<Detail> GetDetailForMasterAndCheckIfItExists
            (Guid masterId, Guid id, bool trackChanges)
        {
            var detailDb = await _repository.Detail.GetDetailAsync(masterId, id, trackChanges);
            if (detailDb is null)
            {
                //throw new EmployeeNotFoundException(id);
            }
            return detailDb;
        }
    }
}
