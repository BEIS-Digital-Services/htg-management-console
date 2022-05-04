using Beis.Htg.VendorSme.Database;
using Beis.Htg.VendorSme.Database.Models;
using Beis.ManagementConsole.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly HtgVendorSmeDbContext _context;

        public ProductRepository(HtgVendorSmeDbContext context)
        {
            _context = context;
        }

        public async Task UpdateProductCapabilities(List<product_capability> productCapabilities)
        {
            foreach (var filter in productCapabilities)
            {
                _context.product_capabilities.Update(filter);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductFilters(List<product_filter> productFilters)
        {
            foreach (var filter in productFilters)
            {
                _context.product_filters.Update(filter);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(product product)
        {
            _context.products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<product_filter>> GetproductFilters(long id)
        {
            return await _context.product_filters.Where(pf => pf.product_id == id).ToListAsync();
        }

        public async Task<List<product_capability>> GetProductCapabilities(long id)
        {
            return await _context.product_capabilities.Where(pc => pc.product_id == id).ToListAsync();
        }

        public async Task<product> GetProductSingle(long id)
        {
            return await _context.products.FirstOrDefaultAsync(t => t.product_id == id);
        }

        public async Task<List<product_status>> GetProductStatuses()
        {
            return await _context.product_statuses.ToListAsync();
        }

        public async Task<List<product>> GetVendorProducts(long vendorId)
        {
            return await _context.products.Where(x=> x.vendor_id == vendorId).ToListAsync();
        }
    }
}