using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class MasterNotFound : NotFoundException
    {
        public MasterNotFound(Guid masterId)
            : base($"The master with id: {masterId} doesn't exist in the database.")
        { }
    }
}
