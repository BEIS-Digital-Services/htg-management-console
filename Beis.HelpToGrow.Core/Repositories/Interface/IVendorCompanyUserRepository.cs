namespace Beis.HelpToGrow.Core.Repositories.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Beis.Htg.VendorSme.Database.Models;

    public interface IVendorCompanyUserRepository
    {
        Task<List<vendor_company_user>> GetVendorCompanyUsersByCompanyId(long id);
    }
}