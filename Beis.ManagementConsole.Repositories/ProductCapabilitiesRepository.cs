using Beis.Htg.VendorSme.Database;
using Beis.Htg.VendorSme.Database.Models;
using Beis.ManagementConsole.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Repositories
{
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