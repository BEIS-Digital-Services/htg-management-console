using AutoMapper;
using Beis.ManagementConsole.Repositories.Interface;
using Beis.ManagementConsole.Web.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Web.Handlers
{
    public class GetUserHandler : IRequestHandler<GetUserHandler.Context, VendorCompanyUserViewModel>
    {
        private readonly IVendorCompanyRepository _vendorCompanyRepository;
        private readonly IVendorCompanyUserRepository _vendorCompanyUserRepository;
        private readonly IMapper _mapper;

        public GetUserHandler(
            IVendorCompanyRepository vendorCompanyRepository,
            IVendorCompanyUserRepository vendorCompanyUserRepository,
            IMapper mapper)
        {
            _vendorCompanyRepository = vendorCompanyRepository;
            _vendorCompanyUserRepository = vendorCompanyUserRepository;
            _mapper = mapper;
        }

        public async Task<VendorCompanyUserViewModel> Handle(Context request, CancellationToken cancellationToken)
        {
            var vendorCompany = await _vendorCompanyRepository.GetVendorCompanySingle(request.Id);
            var vendorCompanyUserViewModel = _mapper.Map<VendorCompanyUserViewModel>(vendorCompany);

            var companyUsers = await _vendorCompanyUserRepository.GetVendorCompanyUsersByCompanyId(request.Id);
            var userViewModel = _mapper.Map<List<UserViewModel>>(companyUsers);
            vendorCompanyUserViewModel.Users = userViewModel.OrderByDescending(u => u.PrimaryContact).ThenBy(u => u.FullName);

            return vendorCompanyUserViewModel;
        }

        public struct Context : IRequest<VendorCompanyUserViewModel>
        {
            public long Id { get; internal set; }
        }
    }
}