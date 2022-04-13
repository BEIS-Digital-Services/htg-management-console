namespace Beis.HelpToGrow.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Beis.HelpToGrow.Core.Repositories.Interface;
    using Beis.Htg.VendorSme.Database;
    using Beis.Htg.VendorSme.Database.Models;
    using Microsoft.EntityFrameworkCore;

    public class SettingsProductFiltersRepository : ISettingsProductFiltersRepository
    {
        private readonly HtgVendorSmeDbContext _context;

        public SettingsProductFiltersRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<settings_product_filter>> GetSettingsProductFilters(long filterType)
        {
            return filterType > 0
                ? await _context.settings_product_filters.Where(x => x.filter_type == filterType).OrderBy(x=> x.sort_order).ToListAsync()
                : await _context.settings_product_filters.OrderBy(x => x.sort_order).ToListAsync();
        }
    }
}