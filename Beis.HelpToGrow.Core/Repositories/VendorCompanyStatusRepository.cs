namespace Beis.HelpToGrow.Core.Repositories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Beis.HelpToGrow.Core.Repositories.Interface;
    using Beis.Htg.VendorSme.Database;
    using Beis.Htg.VendorSme.Database.Models;
    using Microsoft.EntityFrameworkCore;

    public class VendorCompanyStatusRepository : IVendorCompanyStatusRepository
    {
        private readonly HtgVendorSmeDbContext _context;

        public VendorCompanyStatusRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<vendor_status>> GetVendorCompaniesStatuses()
        {
            return await _context.vendor_statuses.ToListAsync();
        }
    }
}