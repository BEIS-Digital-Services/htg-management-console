﻿namespace Beis.HelpToGrow.Core.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Beis.HelpToGrow.Core.Repositories.Interface;
    using Beis.Htg.VendorSme.Database;
    using Beis.Htg.VendorSme.Database.Models;
    using Microsoft.EntityFrameworkCore;

    public class SettingsProductCapabilitiesRepository : ISettingsProductCapabilitiesRepository
    {
        private readonly HtgVendorSmeDbContext _context;

        public SettingsProductCapabilitiesRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<settings_product_capability>> GetSettingsProductCapabilities()
        {
            return await _context.settings_product_capabilities.ToListAsync();
        }
    }
}