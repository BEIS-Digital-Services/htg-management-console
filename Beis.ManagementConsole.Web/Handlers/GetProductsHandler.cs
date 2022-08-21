namespace Beis.ManagementConsole.Web.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsHandler.Context, VendorProductViewModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IVendorCompanyRepository _vendorCompanyRepository;

        public GetProductsHandler(
            IProductRepository productRepository,
            IVendorCompanyRepository vendorCompanyRepository)
        {
            _productRepository = productRepository;
            _vendorCompanyRepository = vendorCompanyRepository;
        }

        public async Task<VendorProductViewModel> Handle(Context request, CancellationToken cancellationToken)
        {
            var vendorCompany = await _vendorCompanyRepository.GetVendorCompanySingle(request.VendorId);
            var vendorProductViewModel = VendorMapper.MapProductDetails(vendorCompany);

            var vendorProducts = await _productRepository.GetVendorProducts(request.VendorId);
            if (vendorProducts.Count > 0)
            {
                var productDetails = ProductMapper.MapProductDetails(vendorProducts);
                var productStatuses = await _productRepository.GetProductStatuses();
                foreach (var productDetail in productDetails)
                {
                    productDetail.Status = productStatuses.Single(x => x.id == int.Parse(productDetail.Status)).status_description;
                }

                vendorProductViewModel.ProductDetails = productDetails;
            }

            return vendorProductViewModel;
        }

        public struct Context : IRequest<VendorProductViewModel>
        {
            public long VendorId { get; internal set; }
        }
    }
}