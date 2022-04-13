﻿namespace Beis.HelpToGrow.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Beis.HelpToGrow.Core.Repositories.Interface;
    using Beis.Htg.VendorSme.Database;
    using Beis.Htg.VendorSme.Database.Models;
    using Microsoft.EntityFrameworkCore;

    public class VendorCompanyUserRepository : IVendorCompanyUserRepository
    {
        private readonly HtgVendorSmeDbContext _context;

        public VendorCompanyUserRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<vendor_company_user>> GetVendorCompanyUsersByCompanyId(long id)
        {
            return await _context.vendor_company_users.Where(u => u.companyid == id).ToListAsync();
        }
    }
}