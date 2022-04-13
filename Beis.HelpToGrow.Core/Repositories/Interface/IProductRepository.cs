﻿namespace Beis.HelpToGrow.Core.Repositories.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Beis.Htg.VendorSme.Database.Models;

    public interface IProductRepository
    {
        Task UpdateProduct(product product);

        Task UpdateProductCapabilities(List<product_capability> productCapabilities);
        
        Task UpdateProductFilters(List<product_filter> productFilters);
        
        Task<product> GetProductSingle(long id);
        
        Task<List<product_capability>> GetProductCapabilities(long id);
        
        Task<List<product_filter>> GetproductFilters(long id);
        
        Task<List<product_status>> GetProductStatuses();
        
        Task<List<product>> GetVendorProducts(long vendorId);
    }
}