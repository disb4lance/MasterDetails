using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public class DetailNotFound : NotFoundException
    {
        public DetailNotFound(Guid detailId)
            : base($"The detail with id: {detailId} doesn't exist in the database.")
        { }
    }
}
