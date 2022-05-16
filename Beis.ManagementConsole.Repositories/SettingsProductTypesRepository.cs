using Beis.Htg.VendorSme.Database;
using Beis.Htg.VendorSme.Database.Models;
using Beis.ManagementConsole.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Repositories
{
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