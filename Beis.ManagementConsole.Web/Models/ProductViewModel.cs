using System.Collections.Generic;

namespace Beis.ManagementConsole.Web.Models
{
    public class ProductViewModel
    {
        public long ProductId { get; internal set; }

        public string ProductSku { get; internal set; }

        public string ProductType { get; internal set; }

        public string ProductName { get; internal set; }

        public string ProductVersion { get; internal set; }

        public string WebsiteUrl { get; internal set; }

        public string DraftProductDescription { get; internal set; }

        public string CyberCompliance { get; internal set; }

        public string MinimumSoftwareRequirements { get; internal set; }

        public string Price { get; internal set; }

        public string SalesDiscount { get; internal set; }

        public string SmeSupport { get; internal set; }

        public string TargetCustomer { get; internal set; }

        public string Ratings { get; internal set; }

        public string RetentionRate { get; internal set; }

        public string CustomerBase { get; internal set; }

        public string Status { get; internal set; }

        public int StatusId { get; internal set; }

        public string ProductLogo { get; internal set; }

        public string RedemptionUrl { get; internal set; }

        public string OtherCompatibility { get; internal set; }

        public string ReviewUrl { get; internal set; }

        public List<string> ProductCapabilities { get; internal set; }

        public List<string> SupportItems { get; internal set; }

        public List<string> TrainingItems { get; internal set; }

        public List<string> PlatformItems { get; internal set; }

        public IList<ProductStatusViewModel> ProductStatuses { get; internal set; }

        public long VendorId { get; internal set; }

        public string VendorCompanyHouseRegNo { get; internal set; }

        public string VendorCompanyName { get; internal set; }
    }
}