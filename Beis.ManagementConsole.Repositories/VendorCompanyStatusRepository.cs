using Beis.Htg.VendorSme.Database;
using Beis.Htg.VendorSme.Database.Models;
using Beis.ManagementConsole.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Repositories
{
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