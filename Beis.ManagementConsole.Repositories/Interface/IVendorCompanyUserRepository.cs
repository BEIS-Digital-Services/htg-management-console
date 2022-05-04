using Beis.Htg.VendorSme.Database.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Repositories.Interface
{
    public interface IVendorCompanyUserRepository
    {
        Task<List<vendor_company_user>> GetVendorCompanyUsersByCompanyId(long id);
    }
}