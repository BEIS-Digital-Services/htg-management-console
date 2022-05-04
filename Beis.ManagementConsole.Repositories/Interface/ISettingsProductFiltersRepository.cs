using Beis.Htg.VendorSme.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface ISettingsProductFiltersRepository
    {
        Task<List<settings_product_filter>> GetSettingsProductFilters(long filterType);
    }
}