using System.Collections.Generic;

namespace Beis.ManagementConsole.Web.Models
{
    public class VendorProductViewModel
    {
        public long VendorId { get; internal set; }

        public string VendorCompanyHouseRegNo { get; internal set; }
        
        public string VendorCompanyName { get; internal set; }
        
        public IList<ProductDetailViewModel> ProductDetails { get; internal set; }
    }

    public class ProductDetailViewModel
    {
        public long ProductId { get; internal set; }
    
        public string ProductName { get; internal set; }
        
        public string Status { get; internal set; }
    }
}