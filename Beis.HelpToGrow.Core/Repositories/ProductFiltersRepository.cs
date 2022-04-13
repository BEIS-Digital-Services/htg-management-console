namespace Beis.HelpToGrow.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Beis.HelpToGrow.Core.Repositories.Interface;
    using Beis.Htg.VendorSme.Database;
    using Beis.Htg.VendorSme.Database.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProductFiltersRepository : IProductFiltersRepository
    {
        private readonly HtgVendorSmeDbContext _context;

        public ProductFiltersRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task<List<product_filter>> GetProductFilters(long productId)
        {
            return await _context.product_filters.Where(pf => pf.product_id == productId).ToListAsync();
        }
    }
}