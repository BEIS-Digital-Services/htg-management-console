namespace Beis.ManagementConsole.Web.Models
{
    public class VendorCompanyViewModel
    {
        public VendorCompanyViewModel()
        {
            this.VendorStatuses = new List<VendorStatusViewModel>();
        }

        public long VendorId { get; internal set; }
    
        public string VendorCompanyName { get; internal set; }
        
        public string VendorCompanyHouseRegNo { get; internal set; }
        
        public string VendorCompanyAddress1 { get; internal set; }
        
        public string VendorCompanyAddress2 { get; internal set; }
        
        public string VendorCompanyCity{ get; internal set; }
        
        public string VendorCompanyPostcode { get; internal set; }
        
        public string VendorNotificationEmail { get; internal set; }
        
        public string VendorWebsiteUrl { get; internal set; }
        
        public string VendorCompanyProfile{ get; internal set; }
        
        public string ApplicationStatus { get; internal set; }
        
        public int ApplicationStatusId { get; internal set; }
        
        public long LockedBy { get; internal set; }
        
        public string EncryptionCode { get; internal set; }
        
        public string AccessSecret { get; internal set; }
        
        
        public string IpAddresses { get; internal set; }
        
        public string EditLog { get; internal set; }

        public IList<VendorStatusViewModel> VendorStatuses { get; internal set; }
    }
}