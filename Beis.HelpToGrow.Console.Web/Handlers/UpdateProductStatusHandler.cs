namespace Beis.HelpToGrow.Console.Web.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;

    using Beis.HelpToGrow.Console.Web.Models;
    using Beis.HelpToGrow.Core.Repositories.Interface;
    using MediatR;

    public class UpdateProductStatusHandler : IRequestHandler<UpdateProductStatusHandler.Context, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductStatusHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(Context request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductSingle(request.ProductId);

            if (product != null)
            {
                product.status = request.ProductStatusId;

                // if the status is Approved
                if (request.ProductStatusId == (int)ProductStatuses.Approved)
                {
                    // Product summary data
                    product.product_description = product.draft_product_description;
                    product.other_capabilities = product.draft_other_capabilities;

                    var productCapabilities = await _productRepository.GetProductCapabilities(request.ProductId);
                    productCapabilities.ForEach(pc => pc.draft_capability = string.Empty);

                    // Product support details
                    var productFilters = await _productRepository.GetproductFilters(request.ProductId);
                    productFilters.ForEach(pc => pc.draft_filter = false);

                    // Deployment platform, compatibility and reviews
                    product.other_compatability = product.draft_other_compatability;
                    product.review_url = product.draft_review_url;

                    await _productRepository.UpdateProductCapabilities(productCapabilities);
                    await _productRepository.UpdateProductFilters(productFilters);
                }
            }

            await _productRepository.UpdateProduct(product);

            return true;
        }

        public struct Context : IRequest<bool>
        {
            public long ProductId { get; internal set; }

            public int ProductStatusId { get; internal set; }
        }
    }
}