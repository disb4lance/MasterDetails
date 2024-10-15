using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IServiceManager
    {
        IMasterService MasterService { get; }
        IDetailService DetailService { get; }
    }
}
