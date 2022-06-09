namespace Beis.ManagementConsole.Web.Handlers
{
    public class UpdateVendorStatusHandler : IRequestHandler<UpdateVendorStatusHandler.Context>
    {
        private readonly IVendorCompanyRepository _vendorCompanyRepository;

        public UpdateVendorStatusHandler(IVendorCompanyRepository vendorCompanyRepository)
        {
            _vendorCompanyRepository = vendorCompanyRepository;
        }

        public async Task<Unit> Handle(Context request, CancellationToken cancellationToken)
        {
            await _vendorCompanyRepository.UpdateVendorCompany(request.CompanyId, request.ApplicationStatusId);
            return Unit.Value;
        }

        public struct Context : IRequest
        {
            public long CompanyId { get; internal set; }

            public int ApplicationStatusId { get; internal set; }
        }
    }
}