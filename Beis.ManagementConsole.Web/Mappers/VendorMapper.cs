namespace Beis.ManagementConsole.Web.Mappers
{
    internal static class VendorMapper
    {
        internal static VendorCompanyViewModel Map(vendor_company vendorCompany)
        {
            return new VendorCompanyViewModel
            {
                AccessSecret = vendorCompany.access_secret,
                ApplicationStatusId = vendorCompany.application_status,
                EditLog = vendorCompany.edit_log,
                EncryptionCode = vendorCompany.encryption_code,
                IpAddresses = vendorCompany.ipaddresses,
                LockedBy = vendorCompany.locked_by,
                VendorCompanyAddress1 = vendorCompany.vendor_company_address_1,
                VendorCompanyAddress2 = vendorCompany.vendor_company_address_2,
                VendorCompanyCity = vendorCompany.vendor_company_city,
                VendorCompanyHouseRegNo = vendorCompany.vendor_company_house_reg_no,
                VendorCompanyName = vendorCompany.vendor_company_name,
                VendorCompanyPostcode = vendorCompany.vendor_company_postcode,
                VendorCompanyProfile = vendorCompany.vendor_company_profile,
                VendorId = vendorCompany.vendorid,
                VendorNotificationEmail = vendorCompany.vendor_notification_email,
                VendorWebsiteUrl = vendorCompany.vendor_website_url,
            };
        }

        internal static IList<VendorCompanyAccountHomeViewModel> MapAccountDetails(IList<vendor_company> vendorCompanies)
        {
            return vendorCompanies?.Select(vendorCompany => new VendorCompanyAccountHomeViewModel
            {
                ApplicationStatus = vendorCompany.application_status.ToString(),
                VendorCompanyName = vendorCompany.vendor_company_name,
                VendorId = vendorCompany.vendorid
            }).ToList();
        }
        
        internal static VendorCompanyUserViewModel MapUserDetails(vendor_company vendorCompany)
        {
            return new VendorCompanyUserViewModel
            {
                CompanyId = vendorCompany.vendorid,
                CompanyName = vendorCompany.vendor_company_name,
                CompanyRegistrationNumber = vendorCompany.vendor_company_house_reg_no
            };
        }

        internal static VendorProductViewModel MapProductDetails(vendor_company vendorCompany)
        {
            return new VendorProductViewModel
            {
                VendorCompanyHouseRegNo = vendorCompany.vendor_company_house_reg_no,
                VendorCompanyName = vendorCompany.vendor_company_name,
                VendorId = vendorCompany.vendorid,
                ProductDetails = new List<ProductDetailViewModel>()
            };
        }

        internal static IList<UserViewModel> Map(IList<vendor_company_user> vendorCompanyUsers)
        {
            return vendorCompanyUsers?.Select(vendorCompanyUser => new UserViewModel
            {
                Email = vendorCompanyUser.email,
                FullName = vendorCompanyUser.full_name,
                PrimaryContact = vendorCompanyUser.primary_contact
            }).ToList();
        }
    }
}