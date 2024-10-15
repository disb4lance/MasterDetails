using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Repository
{
    public class MasterRepository : RepositoryBase<Master>, IMasterRepository
    {
        public MasterRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public async Task<IEnumerable<Master>> GetAllMastersAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .OrderBy(c => c.Number)
            .ToListAsync();

        public async Task<Master> GetMasterAsync(Guid masterId, bool trackChanges) =>
           await FindByCondition(c => c.Id.Equals(masterId), trackChanges)
            .SingleOrDefaultAsync();
        public void CreateCompany(Master master) => Create(master);


        public void DeleteCompany(Master master) =>
            Delete(master);
    }
}
