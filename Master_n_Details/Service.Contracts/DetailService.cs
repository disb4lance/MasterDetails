

using AutoMapper;
using Contracts;
using Entities.Exceptions;
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
            // Шаг 1: Проверяем, существует ли Master
            var master = await CheckIfMasterExists(masterId, trackChanges);

            // Шаг 2: Мапим DTO на сущность Detail
            var detailEntity = _mapper.Map<Detail>(detailForCreation);

            // Шаг 3: Создаем Detail для Master
            _repository.Detail.CreateDetailForMaster(masterId, detailEntity);

            // Шаг 4: Обновляем поле SumPrice у Master, увеличивая его на сумму новой детали
            master.SumPrices += detailForCreation.price;

            // Шаг 5: Сохраняем изменения в базе данных
            await _repository.SaveAsync();

            // Шаг 6: Мапим созданный Detail обратно в DTO и возвращаем результат
            var detailToReturn = _mapper.Map<DetailDto>(detailEntity);

            return detailToReturn;
        }
        public async Task DeleteDetailForMasterAsync(Guid masterId, Guid id, bool trackChanges) {
            var master = await CheckIfMasterExists(masterId, trackChanges);
            
            var detailDb = await GetDetailForMasterAndCheckIfItExists(masterId, id, trackChanges);

            master.SumPrices -= detailDb.Price;
            _repository.Detail.DeleteDetail(detailDb);
            await _repository.SaveAsync();
        }
        public async Task UpdateDetailForMasterAsync(Guid masterId, Guid id, DetailForUpdateDto detailForUpdate, bool masterTrackChanges, bool detailTrackChanges) {
            var master = await CheckIfMasterExists(masterId, masterTrackChanges);
            var detailDb = await GetDetailForMasterAndCheckIfItExists(masterId, id, detailTrackChanges);

            master.SumPrices = master.SumPrices + detailForUpdate.price - detailDb.Price;

            _mapper.Map(detailForUpdate, detailDb);
            await _repository.SaveAsync();

        }

        private async Task<Master> CheckIfMasterExists(Guid masterId, bool trackChanges)
        {
            var master = await _repository.Master.GetMasterAsync(masterId, trackChanges);
            if (master is null)
            {
                throw new MasterNotFound(masterId);
            }
            return master;
        }

        private async Task<Detail> GetDetailForMasterAndCheckIfItExists
            (Guid masterId, Guid id, bool trackChanges)
        {
            var detailDb = await _repository.Detail.GetDetailAsync(masterId, id, trackChanges);
            if (detailDb is null)
            {
                throw new DetailNotFound(id);
            }
            return detailDb;
        }
    }
}
