namespace Beis.HelpToGrow.Console.Web
{
    using System.Collections.Generic;

    using AutoMapper;
    using Beis.HelpToGrow.Console.Web.Models;
    using Beis.Htg.VendorSme.Database.Models;

    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<VendorCompanyAccountHomeViewModel, vendor_company>().ReverseMap()
                .ForMember(d => d.VendorCompanyName, source => source.MapFrom(s => s.vendor_company_name))
                .ForMember(d => d.ApplicationStatus, source => source.MapFrom(s => s.application_status))
                .ForMember(d => d.VendorId, source => source.MapFrom(s => s.vendorid));

            CreateMap<VendorCompanyViewModel, vendor_company>().ReverseMap()
                .ForMember(d => d.VendorId, source => source.MapFrom(s => s.vendorid))
                .ForMember(d => d.VendorCompanyName, source => source.MapFrom(s => s.vendor_company_name))
                .ForMember(d => d.VendorCompanyHouseRegNo, source => source.MapFrom(s => s.vendor_company_house_reg_no))
                .ForMember(d => d.VendorCompanyAddress1, source => source.MapFrom(s => s.vendor_company_address_1))
                .ForMember(d => d.VendorCompanyAddress2, source => source.MapFrom(s => s.vendor_company_address_2))
                .ForMember(d => d.VendorCompanyCity, source => source.MapFrom(s => s.vendor_company_city))
                .ForMember(d => d.VendorCompanyPostcode, source => source.MapFrom(s => s.vendor_company_postcode))
                .ForMember(d => d.VendorNotificationEmail, source => source.MapFrom(s => s.vendor_notification_email))
                .ForMember(d => d.VendorWebsiteUrl, source => source.MapFrom(s => s.vendor_website_url))
                .ForMember(d => d.VendorCompanyProfile, source => source.MapFrom(s => s.vendor_company_profile))
                .ForMember(d => d.ApplicationStatusId, source => source.MapFrom(s => s.application_status))
                .ForMember(d => d.LockedBy, source => source.MapFrom(s => s.locked_by))
                .ForMember(d => d.EncryptionCode, source => source.MapFrom(s => s.encryption_code))
                .ForMember(d => d.AccessSecret, source => source.MapFrom(s => s.access_secret))
                .ForMember(d => d.EditLog, source => source.MapFrom(s => s.edit_log));

            CreateMap<ProductDetailViewModel, product>().ReverseMap()
                .ForMember(d => d.ProductId, source => source.MapFrom(s => s.product_id))
                .ForMember(d => d.ProductName, source => source.MapFrom(s => s.product_name));

            CreateMap<VendorProductViewModel, vendor_company>().ReverseMap()
                .ForMember(d => d.VendorId, source => source.MapFrom(s => s.vendorid))
                .ForMember(d => d.VendorCompanyHouseRegNo, source => source.MapFrom(s => s.vendor_company_house_reg_no))
                .ForMember(d => d.VendorCompanyName, source => source.MapFrom(s => s.vendor_company_name))
                .ForMember(d => d.ProductDetails, source => source.MapFrom(s => new List<VendorProductViewModel>()));

            CreateMap<ProductStatusViewModel, product_status>().ReverseMap()
                .ForMember(d => d.Id, source => source.MapFrom(s => s.id))
                .ForMember(d => d.Description, source => source.MapFrom(s => s.status_description));

            CreateMap<ProductViewModel, product>().ReverseMap()
                .ForMember(d => d.ProductId, source => source.MapFrom(s => s.product_id))
                .ForMember(d => d.ProductSku, source => source.MapFrom(s => s.product_SKU))
                .ForMember(d => d.ProductType, source => source.MapFrom(s => s.product_type))
                .ForMember(d => d.ProductName, source => source.MapFrom(s => s.product_name))
                .ForMember(d => d.ProductLogo, source => source.MapFrom(s => s.product_logo))
                .ForMember(d => d.ProductVersion, source => source.MapFrom(s => s.product_version))
                .ForMember(d => d.WebsiteUrl, source => source.MapFrom(s => s.website_url))
                .ForMember(d => d.DraftProductDescription, source => source.MapFrom(s => s.draft_product_description))
                .ForMember(d => d.CyberCompliance, source => source.MapFrom(s => s.cyber_complance))
                .ForMember(d => d.MinimumSoftwareRequirements,
                    source => source.MapFrom(s => s.minimum_software_requirements))
                .ForMember(d => d.Price, source => source.MapFrom(s => s.price))
                .ForMember(d => d.SalesDiscount, source => source.MapFrom(s => s.sales_discount))
                .ForMember(d => d.SmeSupport, source => source.MapFrom(s => s.sme_support))
                .ForMember(d => d.TargetCustomer, source => source.MapFrom(s => s.target_customer))
                .ForMember(d => d.Ratings, source => source.MapFrom(s => s.ratings))
                .ForMember(d => d.RetentionRate, source => source.MapFrom(s => s.retention_rate))
                .ForMember(d => d.CustomerBase, source => source.MapFrom(s => s.customer_base))
                .ForMember(d => d.RedemptionUrl, source => source.MapFrom(s => s.redemption_url))
                .ForMember(d => d.OtherCompatibility, source => source.MapFrom(s => s.other_compatability))
                .ForMember(d => d.ReviewUrl, source => source.MapFrom(s => s.review_url))
                .ForMember(d => d.VendorId, source => source.MapFrom(s => s.vendor_id));

            CreateMap<VendorCompanyUserViewModel, vendor_company>().ReverseMap()
                .ForMember(d => d.CompanyId, source => source.MapFrom(s => s.vendorid))
                .ForMember(d => d.CompanyName, source => source.MapFrom(s => s.vendor_company_name))
                .ForMember(d => d.CompanyRegistrationNumber, source => source.MapFrom(s => s.vendor_company_house_reg_no));

            CreateMap<UserViewModel, vendor_company_user>().ReverseMap()
                .ForMember(d => d.Email, source => source.MapFrom(s => s.email))
                .ForMember(d => d.FullName, source => source.MapFrom(s => s.full_name))
                .ForMember(d => d.PrimaryContact, source => source.MapFrom(s => s.primary_contact));

        }
    }
}