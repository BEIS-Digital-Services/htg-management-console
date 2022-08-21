namespace Beis.ManagementConsole.Web.Mappers
{
    internal static class ProductMapper
    {
        internal static IList<ProductDetailViewModel> MapProductDetails(IList<product> products)
        {
            return products?.Select(product => new ProductDetailViewModel
            {
                ProductId = product.product_id,
                ProductName = product.product_name,
                Status = product.status.ToString()
            }).ToList();
        }

        internal static IList<ProductStatusViewModel> Map(IList<product_status> productStatuses)
        {
            return productStatuses?.Select(productStatus => new ProductStatusViewModel
            {
                Id = productStatus.id,
                Description = productStatus.status_description
            }).ToList();
        }

        internal static ProductViewModel Map(product product)
        {
            return new ProductViewModel
            {
                CustomerBase = product.customer_base,
                CyberCompliance = product.cyber_complance,
                DraftProductDescription = product.draft_product_description,
                MinimumSoftwareRequirements = product.minimum_software_requirements,
                OtherCompatibility = product.other_compatability,
                Price = product.price,
                ProductId = product.product_id,
                ProductLogo = product.product_logo,
                ProductName = product.product_name,
                ProductSku = product.product_SKU,
                ProductType = product.product_type.ToString(),
                ProductVersion = product.product_version,
                Ratings = product.ratings,
                RedemptionUrl = product.redemption_url,
                RetentionRate = product.retention_rate,
                ReviewUrl = product.review_url,
                SalesDiscount = product.sales_discount,
                SmeSupport = product.sme_support,
                Status = product.status.ToString(),
                TargetCustomer = product.target_customer,
                VendorId = product.vendor_id,
                WebsiteUrl = product.website_url
            };
        }
    }
}