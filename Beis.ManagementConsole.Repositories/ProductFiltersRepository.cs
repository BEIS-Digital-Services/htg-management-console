using Beis.Htg.VendorSme.Database;
using Beis.Htg.VendorSme.Database.Models;
using Beis.ManagementConsole.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Repositories
{
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