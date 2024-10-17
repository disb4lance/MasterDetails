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

        // Метод проверки на уникальность номера документа
        public async Task<bool> IsDocumentNumberUniqueAsync(string documentNumber, bool trackchange)
        {
            return !await FindByCondition(m => m.Number.Equals(documentNumber), trackchange).AnyAsync();
        }

        public async Task<Master> GetMasterByNumberAsync(string number, bool trackChanges) =>
            await FindByCondition(c => c.Number.Equals(number), trackChanges)
            .SingleOrDefaultAsync();
        

        public async Task<IEnumerable<Master>> GetAllMastersAsync(bool trackChanges) =>
        await FindAll(trackChanges)
        .OrderBy(c => c.Number)
            .ToListAsync();


        public async Task<Master> GetMasterAsync(Guid masterId, bool trackChanges) =>
           await FindByCondition(c => c.Id.Equals(masterId), trackChanges)
            .SingleOrDefaultAsync();
        public void CreateMaster(Master master)
        {
            master.Date = DateTime.Now;
            master.SumPrices = 0;
            Create(master);
        }


        public void DeleteMaster(Master master) =>
            Delete(master);
    }
}
