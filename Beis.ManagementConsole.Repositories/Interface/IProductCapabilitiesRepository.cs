using Beis.Htg.VendorSme.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface IProductCapabilitiesRepository
    {
        Task<List<product_capability>> GetProductCapabilitiesFilters(long productId = 0);
    }
}