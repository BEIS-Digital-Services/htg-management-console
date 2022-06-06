namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface IVendorCompanyUserRepository
    {
        Task<List<vendor_company_user>> GetVendorCompanyUsersByCompanyId(long id);
    }
}