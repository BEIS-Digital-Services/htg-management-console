namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface ISettingsProductCapabilitiesRepository
    {
        Task<List<settings_product_capability>> GetSettingsProductCapabilities();
    }
}