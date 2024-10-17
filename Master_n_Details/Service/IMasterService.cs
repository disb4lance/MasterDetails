
using Shared.DataTransferObjects;

namespace Service
{
    public interface IMasterService
    {
        Task<IEnumerable<MasterDto>> GetAllMastersAsync(bool trackChanges);
        Task<MasterDto> GetMasterAsync(Guid masterId, bool trackChanges);

        Task<MasterDto> GetMasterByName(string number, bool trackChanges);
        Task<MasterDto> CreateMasterAsync(MasterForCreatingDto master);
        Task DeleteMasterAsync(Guid masterId, bool trackChanges);
        Task UpdateMasterAsync(Guid masterId, MasterForUpdateDto masterForUpdate, bool trackChanges);
    }
}
