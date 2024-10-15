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
        Task<IEnumerable<Master>> GetAllMastersAsync(bool trackChanges);
        Task<Master> GetMasterAsync(Guid masterId, bool trackChanges);
        void CreateCompany(Master master);
        void DeleteCompany(Master master);
    }
}
