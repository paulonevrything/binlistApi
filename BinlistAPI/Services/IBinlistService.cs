using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinlistAPI.Services
{
    public interface IBinlistService
    {
        Task<object> GetBinDetails(string cardIin);
    }
}
