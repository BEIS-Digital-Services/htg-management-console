using Beis.Htg.VendorSme.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface ISettingsProductFiltersCategoriesRepository
    {
        Task<List<settings_product_filters_category>> GetSettingsProductFiltersCategories();
    }
}