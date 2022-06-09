namespace Beis.ManagementConsole.Repositories
{
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