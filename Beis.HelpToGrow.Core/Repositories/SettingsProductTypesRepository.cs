namespace Beis.HelpToGrow.Core.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Beis.HelpToGrow.Core.Repositories.Interface;
    using Beis.Htg.VendorSme.Database;
    using Beis.Htg.VendorSme.Database.Models;
    using Microsoft.EntityFrameworkCore;

    public class SettingsProductTypesRepository : ISettingsProductTypesRepository

    {
        private readonly HtgVendorSmeDbContext _context;

        public SettingsProductTypesRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<settings_product_type>> GetSettingsProductTypes()
        {
            return await _context.settings_product_types.ToListAsync();
        }
    }
}