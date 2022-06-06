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