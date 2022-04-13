namespace Beis.HelpToGrow.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Beis.HelpToGrow.Core.Repositories.Interface;
    using Beis.Htg.VendorSme.Database;
    using Beis.Htg.VendorSme.Database.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProductCapabilitiesRepository : IProductCapabilitiesRepository

    {
        private readonly HtgVendorSmeDbContext _context;

        public ProductCapabilitiesRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<product_capability>> GetProductCapabilitiesFilters(long productId)
        {
            return await _context.product_capabilities.Where(x => x.product_id == productId).ToListAsync();
        }
    }
}