using AutoMapper;
using Beis.Htg.VendorSme.Database.Models;
using Beis.ManagementConsole.Repositories.Interface;
using Beis.ManagementConsole.Web.Models;
using Beis.ManagementConsole.Web.Models.Enums;
using Beis.ManagementConsole.Web.Options;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Web.Handlers
{
    public class GetProductHandler : IRequestHandler<GetProductHandler.Context, ProductViewModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly ISettingsProductTypesRepository _settingsProductTypesRepository;
        private readonly ISettingsProductFiltersCategoriesRepository _settingsProductFiltersCategoriesRepository;
        private readonly ISettingsProductFiltersRepository _settingsProductFiltersRepository;
        private readonly IProductFiltersRepository _productFiltersRepository;
        private readonly IProductCapabilitiesRepository _productCapabilitiesRepository;
        private readonly ISettingsProductCapabilitiesRepository _settingsProductCapabilitiesRepository;
        private readonly IVendorCompanyRepository _vendorCompanyRepository;
        private readonly LogoInformationOption _logoInformation;
        private readonly IMapper _mapper;

        public GetProductHandler(
            IProductRepository productRepository,
            ISettingsProductTypesRepository settingsProductTypesRepository,
            ISettingsProductFiltersCategoriesRepository settingsProductFiltersCategoriesRepository,
            ISettingsProductFiltersRepository settingsProductFiltersRepository,
            IProductFiltersRepository productFiltersRepository,
            IProductCapabilitiesRepository productCapabilitiesRepository,
            ISettingsProductCapabilitiesRepository settingsProductCapabilitiesRepository,
            IVendorCompanyRepository vendorCompanyRepository,
            IOptions<LogoInformationOption> logoInformationOption,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _settingsProductTypesRepository = settingsProductTypesRepository;
            _settingsProductFiltersCategoriesRepository = settingsProductFiltersCategoriesRepository;
            _settingsProductFiltersRepository = settingsProductFiltersRepository;
            _productFiltersRepository = productFiltersRepository;
            _productCapabilitiesRepository = productCapabilitiesRepository;
            _settingsProductCapabilitiesRepository = settingsProductCapabilitiesRepository;
            _vendorCompanyRepository = vendorCompanyRepository;
            _logoInformation = logoInformationOption.Value;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> Handle(Context request, CancellationToken cancellationToken)
        {
            var product = await this.GetProduct(request.ProductId);

            if (!string.IsNullOrWhiteSpace(product.ProductLogo) && product.ProductLogo.Contains("."))
            {
                product.ProductLogo = $"{_logoInformation.ProductLogoUrl}{_logoInformation.Path}{product.ProductLogo.Substring(product.ProductLogo.LastIndexOf(@"\", StringComparison.InvariantCultureIgnoreCase) + 1)}";
            }
            
            var vendorCompany = await this.GetVendorCompany(request.VendorId);
            product.VendorCompanyHouseRegNo = vendorCompany.vendor_company_house_reg_no;
            product.VendorCompanyName = vendorCompany.vendor_company_name;
            
            return product;
        }

        private async Task<ProductViewModel> GetProduct(long productId)
        {
            var product = await _productRepository.GetProductSingle(productId);

            var allProductTypes = await _settingsProductTypesRepository.GetSettingsProductTypes();
            var productCapabilities = await this.GetProductCapabilities(productId);
            var allCapabilities = await this.GetSettingsProductCapabilities();
            var capabilities = new List<string>();

            if (productCapabilities.Count > 0)
            {
                foreach (var productCapability in productCapabilities)
                {
                    capabilities.Add(allCapabilities.Find(x => x.capability_id == productCapability.capability_id)?.capability_name);
                }
            }

            if (!string.IsNullOrWhiteSpace(product.draft_other_capabilities))
            {
                capabilities.Add(product.draft_other_capabilities);
            }

            var productViewModel = _mapper.Map<ProductViewModel>(product);
            productViewModel.ProductCapabilities = capabilities;
            await AddOtherItems(productViewModel, productId);
            
            var productStatuses = await GetProductStatuses();
            if (productViewModel.Status != null)
            {
                productViewModel.StatusId = Convert.ToInt32(productViewModel.Status);
                productViewModel.Status = productStatuses.Single(x => x.Id == Convert.ToInt32(productViewModel.Status)).Description;
            }

            productViewModel.ProductType = allProductTypes.Find(x => x.id == Convert.ToInt64(productViewModel.ProductType))?.item_name;
            productViewModel.ProductStatuses = productStatuses;

            return productViewModel;
        }

        private async Task<vendor_company> GetVendorCompany(long vendorCompanyId)
        {
            return await _vendorCompanyRepository.GetVendorCompanySingle(vendorCompanyId);
        }

        private async Task<List<ProductStatusViewModel>> GetProductStatuses()
        {
            var productStatuses = await _productRepository.GetProductStatuses();
            return _mapper.Map<List<ProductStatusViewModel>>(productStatuses);
        }

        private async Task AddOtherItems(ProductViewModel productDetails, long productId)
        {
            var filterTypes = new List<long> { (int)ProductFilterCategories.Support,
                                                (int)ProductFilterCategories.Training,
                                                (int)ProductFilterCategories.Platform };

            var settingsProductFiltersCategories = (await _settingsProductFiltersCategoriesRepository.GetSettingsProductFiltersCategories())
                .Where(x => filterTypes.Contains(x.id)).OrderBy(x => x.id).ToList();
            var settingsProductFilters = await _settingsProductFiltersRepository.GetSettingsProductFilters(0);
            var productFilters = await _productFiltersRepository.GetProductFilters(productId);

            foreach (var settingsProductFiltersCategory in settingsProductFiltersCategories)
            {
                var temp = settingsProductFilters.Where(x => x.filter_type == settingsProductFiltersCategory.id).ToList();
                var items = temp.Select(x => new SelectListItem { Text = x.filter_name, Value = x.filter_id.ToString() });

                var lstItems = items.ToList();
                foreach (var t2 in from productFilter in productFilters
                         from t2 in lstItems where t2.Value == productFilter.filter_id.ToString() select t2)
                {
                    switch (settingsProductFiltersCategory.id)
                    {
                        case (int)ProductFilterCategories.Support:
                            productDetails.SupportItems ??= new List<string>();
                            productDetails.SupportItems.Add(t2.Text);
                            break;
                        case (int)ProductFilterCategories.Training:
                            productDetails.TrainingItems ??= new List<string>();
                            productDetails.TrainingItems.Add(t2.Text);
                            break;
                        case (int)ProductFilterCategories.Platform:
                            productDetails.PlatformItems ??= new List<string>();
                            productDetails.PlatformItems.Add(t2.Text);
                            break;
                    }
                }
            }
        }

        private async Task<List<product_capability>> GetProductCapabilities(long productId)
        {
            return  await _productCapabilitiesRepository.GetProductCapabilitiesFilters(productId);
        }

        private async Task<List<settings_product_capability>> GetSettingsProductCapabilities()
        {
            return await _settingsProductCapabilitiesRepository.GetSettingsProductCapabilities();
        }

        public struct Context : IRequest<ProductViewModel>
        {
            public long ProductId { get; internal set; }

            public long VendorId { get; internal set; }
        }
    }
}