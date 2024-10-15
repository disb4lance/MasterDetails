using Shared.DataTransferObjects;


namespace Service
{
    public interface IDetailService
    {
        Task<IEnumerable<DetailDto>> GetDetailsAsync(Guid masterId,  bool trackChanges);
        Task<DetailDto> GetDetailAsync(Guid masterId, Guid id, bool trackChanges);
        Task<DetailDto> CreateDetailForMasterAsync(Guid masterId, DetailForCreatingDto detailForCreation, bool trackChanges);
        Task DeleteDetailForMasterAsync(Guid masterId, Guid id, bool trackChanges);
        Task UpdateDetailForMasterAsync(Guid masterId, Guid id, DetailForUpdateDto detailForUpdate, bool masterTrackChanges, bool detailTrackChanges);

    }
}
