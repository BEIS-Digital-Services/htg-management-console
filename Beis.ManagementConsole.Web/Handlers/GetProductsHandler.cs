namespace Beis.ManagementConsole.Web.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsHandler.Context, VendorProductViewModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IVendorCompanyRepository _vendorCompanyRepository;
        private readonly IMapper _mapper;

        public GetProductsHandler(
            IProductRepository productRepository,
            IVendorCompanyRepository vendorCompanyRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _vendorCompanyRepository = vendorCompanyRepository;
            _mapper = mapper;
        }

        public async Task<VendorProductViewModel> Handle(Context request, CancellationToken cancellationToken)
        {
            var vendorCompany = await _vendorCompanyRepository.GetVendorCompanySingle(request.VendorId);
            var vendorProductViewModel = _mapper.Map<VendorProductViewModel>(vendorCompany);

            var vendorProducts = await _productRepository.GetVendorProducts(request.VendorId);
            if (vendorProducts.Count > 0)
            {
                var productDetails = _mapper.Map<List<ProductDetailViewModel>>(vendorProducts);
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