namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface ISettingsProductTypesRepository
    {
        Task<List<settings_product_type>> GetSettingsProductTypes();
    }
}