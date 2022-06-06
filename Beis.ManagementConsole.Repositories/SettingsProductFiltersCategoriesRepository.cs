namespace Beis.ManagementConsole.Repositories
{
    public class SettingsProductFiltersCategoriesRepository : ISettingsProductFiltersCategoriesRepository

    {
        private readonly HtgVendorSmeDbContext _context;

        public SettingsProductFiltersCategoriesRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<settings_product_filters_category>> GetSettingsProductFiltersCategories()
        {
            return await _context.settings_product_filters_categories.ToListAsync();
        }
    }
}