using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IDetailRepository
    {
        Task<IEnumerable<Detail>> GetDetailsAsync(Guid masterId, bool trackChanges);
        Task<Detail> GetDetailAsync(Guid masterId, Guid id, bool trackChanges);
        void CreateDetailForMaster(Guid masterId, Detail detail);
        void DeleteDetail(Detail detail);
    }
}
