﻿namespace Beis.HelpToGrow.Core.Repositories.Interface
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Beis.Htg.VendorSme.Database.Models;

    public interface IVendorCompanyRepository
    {
        Task UpdateVendorCompany(long vendorId, int applicationStatus);

        Task<vendor_company> GetVendorCompanySingle(long id);
        
        Task<List<vendor_company>> GetVendorCompanies();
    }
}