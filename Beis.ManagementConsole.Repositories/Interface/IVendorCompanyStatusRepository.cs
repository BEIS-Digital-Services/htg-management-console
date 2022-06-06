namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface IVendorCompanyStatusRepository
    {
        Task<List<vendor_status>> GetVendorCompaniesStatuses();
    }
}