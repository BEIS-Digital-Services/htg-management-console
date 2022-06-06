namespace Beis.ManagementConsole.Web.Handlers
{
    public class VendorIndexHandler : IRequestHandler<VendorIndexHandler.Context, IEnumerable<VendorCompanyAccountHomeViewModel>>
    {
        private readonly IVendorCompanyRepository _vendorCompanyRepository;
        private readonly IVendorCompanyStatusRepository _vendorCompanyStatusRepository;
        private readonly IMapper _mapper;

        public VendorIndexHandler(
            IVendorCompanyRepository vendorCompanyRepository,
            IVendorCompanyStatusRepository vendorCompanyStatusRepository,
            IMapper mapper)
        {
            _vendorCompanyRepository = vendorCompanyRepository;
            _vendorCompanyStatusRepository = vendorCompanyStatusRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VendorCompanyAccountHomeViewModel>> Handle(Context request, CancellationToken cancellationToken)
        {
            var vendorCompanies = await _vendorCompanyRepository.GetVendorCompanies();

            var vendorStatuses = await _vendorCompanyStatusRepository.GetVendorCompaniesStatuses();

            var vendorCompaniesVm = _mapper.Map<List<VendorCompanyAccountHomeViewModel>>(vendorCompanies);

            foreach (var item in vendorCompaniesVm)
            {
                item.ApplicationStatus = vendorStatuses.Find(x => x.id == Convert.ToInt32(item.ApplicationStatus))?.status_description;
            }

            return vendorCompaniesVm;
        }

        public struct Context : IRequest<IEnumerable<VendorCompanyAccountHomeViewModel>>
        {
        }
    }
}