namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface ISettingsProductFiltersCategoriesRepository
    {
        Task<List<settings_product_filters_category>> GetSettingsProductFiltersCategories();
    }
}