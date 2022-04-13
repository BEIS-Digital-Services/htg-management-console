namespace Beis.HelpToGrow.Console.Web.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;
    using Beis.HelpToGrow.Console.Web.Models;
    using Beis.HelpToGrow.Core.Repositories.Interface;
    using MediatR;

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