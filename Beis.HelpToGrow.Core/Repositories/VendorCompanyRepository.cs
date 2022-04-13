namespace Beis.HelpToGrow.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Beis.HelpToGrow.Core.Repositories.Interface;
    using Beis.Htg.VendorSme.Database;
    using Beis.Htg.VendorSme.Database.Models;
    using Microsoft.EntityFrameworkCore;

    public class VendorCompanyRepository : IVendorCompanyRepository
    {
        private readonly HtgVendorSmeDbContext _context;

        public VendorCompanyRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }
        
        public async Task UpdateVendorCompany(long vendorId, int applicationStatus)
        {
            _context.vendor_companies.First(r => r.vendorid == vendorId).application_status = applicationStatus;
            await _context.SaveChangesAsync();
        }

        public async Task<vendor_company> GetVendorCompanySingle(long id)
        {
            return await _context.vendor_companies.FirstOrDefaultAsync(t => t.vendorid == id);
        }

        public async Task<List<vendor_company>> GetVendorCompanies()
        {
            return await _context.vendor_companies.ToListAsync();
        }
    }
}