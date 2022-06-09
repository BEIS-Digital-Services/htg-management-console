namespace Beis.ManagementConsole.Web.Models
{
    public class VendorCompanyUserViewModel
    {
        public long CompanyId { get; internal set; }

        public string CompanyRegistrationNumber { get; internal set; }

        public string CompanyName { get; internal set; }

        public IOrderedEnumerable<UserViewModel> Users { get; internal set; }
    }

    public class UserViewModel
    {
        public string Email { get; internal set; }

        public string FullName { get; internal set; }

        public bool PrimaryContact { get; internal set; }
    }
}