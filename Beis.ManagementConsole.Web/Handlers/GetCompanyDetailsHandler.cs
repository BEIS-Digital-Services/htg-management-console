namespace Beis.ManagementConsole.Web.Handlers
{
    public class GetCompanyDetailsHandler : IRequestHandler<GetCompanyDetailsHandler.Context, VendorCompanyViewModel>
    {
        private readonly IVendorCompanyRepository _vendorCompanyRepository;
        private readonly IVendorCompanyStatusRepository _vendorCompanyStatusRepository;
        
        public GetCompanyDetailsHandler(
            IVendorCompanyRepository vendorCompanyRepository,
            IVendorCompanyStatusRepository vendorCompanyStatusRepository)
        {
            _vendorCompanyRepository = vendorCompanyRepository;
            _vendorCompanyStatusRepository = vendorCompanyStatusRepository;
        }

        public async Task<VendorCompanyViewModel> Handle(Context request, CancellationToken cancellationToken)
        {
            var vendorCompany = await _vendorCompanyRepository.GetVendorCompanySingle(request.CompanyId);
            var vendorCompanyViewModel = VendorMapper.Map(vendorCompany);

            if (vendorCompanyViewModel.ApplicationStatusId > 0)
            {
                var vendorStatuses = await _vendorCompanyStatusRepository.GetVendorCompaniesStatuses();
                vendorCompanyViewModel.ApplicationStatus = vendorStatuses.Find(x => x.id == vendorCompanyViewModel.ApplicationStatusId)?.status_description;
                vendorStatuses.ForEach(r =>
                {
                    vendorCompanyViewModel.VendorStatuses.Add(new VendorStatusViewModel { Id = r.id, Description = r.status_description });
                });
            }

            return vendorCompanyViewModel;
        }

        public struct Context : IRequest<VendorCompanyViewModel>
        {
            public long CompanyId { get; internal set; }
        }
    }
}