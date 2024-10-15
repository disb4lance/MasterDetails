using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Repository
{
    public class DetailRepository : RepositoryBase<Detail>, IDetailRepository
    {
        public DetailRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Detail>> GetDetailsAsync(Guid masterId, bool trackChanges)
        {
            var details = await FindByCondition(e => e.MasterId.Equals(masterId), trackChanges)
                                                 .OrderBy(e => e.Name).ToListAsync();

            return details;
        }

        public async Task<Detail> GetDetailAsync(Guid masterId, Guid id, bool trackChanges) =>
           await FindByCondition(e => e.MasterId.Equals(masterId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateDetailForMaster(Guid masterId, Detail detail)
        {
            detail.MasterId = masterId;
            Create(detail);
        }

        public void DeleteDetail(Detail detail) =>
            Delete(detail);
    }
}
