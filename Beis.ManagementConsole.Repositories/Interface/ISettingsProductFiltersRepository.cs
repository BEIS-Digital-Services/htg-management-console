namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface ISettingsProductFiltersRepository
    {
        Task<List<settings_product_filter>> GetSettingsProductFilters(long filterType);
    }
}