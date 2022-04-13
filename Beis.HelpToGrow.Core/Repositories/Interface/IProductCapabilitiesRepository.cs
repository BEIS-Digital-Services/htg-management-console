namespace Beis.HelpToGrow.Core.Repositories.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Beis.Htg.VendorSme.Database.Models;

    public interface IProductCapabilitiesRepository
    {
        Task<List<product_capability>> GetProductCapabilitiesFilters(long productId = 0);
    }
}