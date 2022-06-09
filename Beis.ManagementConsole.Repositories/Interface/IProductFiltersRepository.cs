namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface IProductFiltersRepository
    {
        Task<List<product_filter>> GetProductFilters(long productId);
    }
}