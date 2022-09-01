namespace Beis.ManagementConsole.Web.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserHandler.Context, VendorCompanyUserViewModel>
    {
        private readonly IVendorCompanyRepository _vendorCompanyRepository;
        private readonly IVendorCompanyUserRepository _vendorCompanyUserRepository;

        public GetUserHandler(
            IVendorCompanyRepository vendorCompanyRepository,
            IVendorCompanyUserRepository vendorCompanyUserRepository)
        {
            _vendorCompanyRepository = vendorCompanyRepository;
            _vendorCompanyUserRepository = vendorCompanyUserRepository;
        }

        public async Task<VendorCompanyUserViewModel> Handle(Context request, CancellationToken cancellationToken)
        {
            var vendorCompany = await _vendorCompanyRepository.GetVendorCompanySingle(request.Id);
            var vendorCompanyUserViewModel = VendorMapper.MapUserDetails(vendorCompany);

            var companyUsers = await _vendorCompanyUserRepository.GetVendorCompanyUsersByCompanyId(request.Id);
            var userViewModel = VendorMapper.Map(companyUsers);
            vendorCompanyUserViewModel.Users = userViewModel.OrderByDescending(u => u.PrimaryContact).ThenBy(u => u.FullName);

            return vendorCompanyUserViewModel;
        }

        public struct Context : IRequest<VendorCompanyUserViewModel>
        {
            public long Id { get; internal set; }
        }
    }
}