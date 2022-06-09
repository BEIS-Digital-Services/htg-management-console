namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface IProductCapabilitiesRepository
    {
        Task<List<product_capability>> GetProductCapabilitiesFilters(long productId = 0);
    }
}