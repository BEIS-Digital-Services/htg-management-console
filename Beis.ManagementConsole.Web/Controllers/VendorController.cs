using Beis.ManagementConsole.Web.Constants;
using Beis.ManagementConsole.Web.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Web.Controllers
{
    [Authorize]
    public class VendorController : Controller
    {
        private readonly IMediator _handler;

        public VendorController(IMediator handler)
        {
            _handler = handler;
        }

        [HttpGet]
        [Route("")]
        [Route("Vendor/Index", Name = RouteNameConstants.VendorIndexGet)]
        public async Task<IActionResult> Index() => this.View(await _handler.Send(new VendorIndexHandler.Context()));

        [Route("Vendor/CompanyDetails", Name = RouteNameConstants.VendorCompanyDetailsGet)]
        public async Task<IActionResult> CompanyDetails(long id) => this.View(await _handler.Send(new GetCompanyDetailsHandler.Context { CompanyId = id }));

        [HttpPost]
        [Route("Vendor/CompanyDetails", Name = RouteNameConstants.VendorCompanyDetailsPost)]
        public async Task<IActionResult> CompanyDetails(long id, int applicationStatusId)
        {
            await _handler.Send(new UpdateVendorStatusHandler.Context { CompanyId = id, ApplicationStatusId = applicationStatusId });
            return this.RedirectToRoute(RouteNameConstants.VendorIndexGet);
        }

        [HttpGet]
        [Route("Vendor/Products", Name = RouteNameConstants.VendorProductsGet)]
        public async Task<IActionResult> Products(long id) => this.View(await _handler.Send(new GetProductsHandler.Context { VendorId = id }));

        [HttpGet]
        [Route("Vendor/Product", Name = RouteNameConstants.VendorProductGet)]
        public async Task<IActionResult> Product(long id, long vid)
        {
            var product = await _handler.Send(new GetProductHandler.Context
            {
                ProductId = id,
                VendorId = vid
            });

            return this.View(product);
        }

        [HttpPost]
        [Route("Vendor/Product", Name = RouteNameConstants.VendorProductPost)]
        public async Task<IActionResult> Product(long id, int statusId, long vendorId)
        {
            await _handler.Send(new UpdateProductStatusHandler.Context { ProductId = id, ProductStatusId = statusId });
            return this.RedirectToRoute(RouteNameConstants.VendorProductsGet, new { id = vendorId });
        }

        [HttpGet]
        [Route("Vendor/Users", Name = RouteNameConstants.VendorUsersGet)]
        public async Task<IActionResult> Users(long id) => this.View(await _handler.Send(new GetUserHandler.Context { Id = id }));
    }
}