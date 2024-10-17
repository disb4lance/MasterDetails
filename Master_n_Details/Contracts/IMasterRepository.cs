using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contracts
{
    public interface IMasterRepository
    {
        Task<bool> IsDocumentNumberUniqueAsync(string documentNumber, bool trackchange);

        Task<Master> GetMasterByNumberAsync(string number, bool trackChanges);
        Task<IEnumerable<Master>> GetAllMastersAsync(bool trackChanges);
        Task<Master> GetMasterAsync(Guid masterId, bool trackChanges);
        void CreateMaster(Master master);
        void DeleteMaster(Master master);
    }
}
