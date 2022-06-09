namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface IVendorCompanyRepository
    {
        Task UpdateVendorCompany(long vendorId, int applicationStatus);

        Task<vendor_company> GetVendorCompanySingle(long id);
        
        Task<List<vendor_company>> GetVendorCompanies();
    }
}