using AutoFixture;
using Beis.Htg.VendorSme.Database;
using Beis.ManagementConsole.Web.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MockQueryable.Moq;
using System.Linq;
using System.Threading.Tasks;

namespace Beis.ManagementConsole.Web.UnitTests
{
    public class VendorControllerTests
    {
        private const int TestCompanyId = 12345;
        private const string TestRegistrationId = "12345";
        private const int TestApplicationStatus = 241;
        private const string TestAccessSecret = "12345";
        private const string TestIpAddresses = "192.168.1.1;192.168.1.10";
        private readonly VendorController _sut;
        private readonly Mock<HtgVendorSmeDbContext> _mockHtgVendorSmeDbContext;
        private readonly Fixture _autoFixture;

        public VendorControllerTests()
        {
            _mockHtgVendorSmeDbContext = new Mock<HtgVendorSmeDbContext>();
            _autoFixture = new Fixture();
            _autoFixture.Behaviors.Add(new OmitOnRecursionBehavior());

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    {"LogoInformation:Path", "/uploads/"},
                    {"LogoInformation:ProductLogoUrl", "https://localhost:8158"}
                })
                .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.RegisterAllServices(configuration);
            serviceCollection.AddScoped(options => _mockHtgVendorSmeDbContext.Object);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            _sut = new VendorController(serviceProvider.GetService<IMediator>());
        }

        [Fact]
        public async Task ShouldGetCompaniesForAccountHome()
        {
            // Arrange
            SetupVendorStatuses();
            SetupVendorCompanies();

            // Act
            var result = await _sut.Index() as ViewResult;

            // Assert
            Assert.True(result != null);
            var model = result.Model as List<VendorCompanyAccountHomeViewModel>;
            Assert.NotNull(model);
            Assert.True(model.Any());
            Assert.True(model.All(r => !string.IsNullOrWhiteSpace(r.VendorCompanyName) && r.VendorId > 0));
        }

        [Fact]
        public async Task ShouldGetCompanyDetails()
        {
            // Arrange
            SetupVendorStatuses();
            SetupVendorCompanies();

            // Act
            var result = await _sut.CompanyDetails(TestCompanyId) as ViewResult;

            // Assert
            Assert.True(result != null);
            var model = result.Model as VendorCompanyViewModel;
            Assert.NotNull(model);
            Assert.NotNull(model.VendorCompanyName);
            Assert.NotNull(model.VendorCompanyHouseRegNo);
            Assert.NotNull(model.VendorCompanyAddress1);
            Assert.NotNull(model.VendorCompanyAddress2);
            Assert.NotNull(model.VendorCompanyCity);
            Assert.NotNull(model.VendorCompanyPostcode);
            Assert.NotNull(model.VendorNotificationEmail);
            Assert.NotNull(model.VendorWebsiteUrl);
            Assert.NotNull(model.VendorCompanyProfile);
            Assert.True(model.LockedBy > 0);
            Assert.NotNull(model.EncryptionCode);
            Assert.NotNull(model.AccessSecret);
            Assert.NotNull(model.EditLog);
            Assert.Equal(TestCompanyId, model.VendorId);
            Assert.True(!string.IsNullOrWhiteSpace(model.ApplicationStatus));
            Assert.True(model.ApplicationStatusId > 0);
            Assert.True(!string.IsNullOrWhiteSpace(model.IpAddresses));
            Assert.True(model.VendorStatuses.Any());
            Assert.True(model.VendorStatuses.All(r => r.Id > 0 && !string.IsNullOrWhiteSpace(r.Description)));

        }

        [Fact]
        public async Task ShouldUpdateCompanyDetails()
        {
            // Arrange
            SetupVendorStatuses();
            SetupVendorCompanies();

            // Act
            var result = await _sut.CompanyDetails(TestCompanyId, TestApplicationStatus) as RedirectToRouteResult;

            // Assert
            Assert.True(result != null);
            Assert.Equal("VendorIndexGet", result.RouteName);
            _mockHtgVendorSmeDbContext.Verify(r => r.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task ShouldGetProducts()
        {
            // Arrange
            SetupVendorStatuses();
            SetupVendorCompanies();
            SetupProductStatuses();
            SetupProducts();

            // Act
            var result = await _sut.Products(TestCompanyId) as ViewResult;

            // Assert
            Assert.True(result != null);
            var model = result.Model as VendorProductViewModel;
            Assert.NotNull(model);
            Assert.True(model.VendorId > 0);
            Assert.NotNull(model.VendorCompanyName);
            Assert.NotNull(model.VendorCompanyHouseRegNo);
            Assert.True(model.ProductDetails.Any());
            Assert.True(model.ProductDetails.All(r => r.ProductId > 0 && !string.IsNullOrWhiteSpace(r.ProductName)));
        }

        [Fact]
        public async Task ShouldGetProduct()
        {
            // Arrange
            SetupVendorStatuses();
            SetupVendorCompanies();
            SetupProductStatuses();
            SetupProducts("logo.jpg");
            SetupSettingsProductTypes();
            SetupSettingsProductCapabilities();
            SetupProductCapabilities();
            SetupSettingsProductFiltersCategories();
            SetupSettingsProductFilters();
            SetupProductFilters();

            // Act
            var result = await _sut.Product(1, TestCompanyId) as ViewResult;

            // Assert
            Assert.True(result != null);
            var model = result.Model as ProductViewModel;
            Assert.NotNull(model);
            Assert.True(model.VendorId> 0);
            Assert.NotNull(model.VendorCompanyName);
            Assert.NotNull(model.VendorCompanyHouseRegNo);
            Assert.NotNull(model.ProductName);
            Assert.NotNull(model.DraftProductDescription);
            Assert.NotNull(model.ProductLogo);
            Assert.NotNull(model.RedemptionUrl);
            Assert.True(model.ProductCapabilities.Any());
            Assert.True(model.SupportItems.Any());
            Assert.True(model.TrainingItems.Any());
            Assert.True(model.PlatformItems.Any());
            Assert.NotNull(model.OtherCompatibility);
            Assert.NotNull(model.ReviewUrl);
            Assert.True(model.ProductId > 0);
            Assert.NotNull(model.ProductSku);
            Assert.NotNull(model.ProductType);
            Assert.NotNull(model.ProductVersion);
            Assert.NotNull(model.WebsiteUrl);
            Assert.NotNull(model.CyberCompliance);
            Assert.NotNull(model.MinimumSoftwareRequirements);
            Assert.NotNull(model.Ratings);
            Assert.NotNull(model.Price);
            Assert.NotNull(model.SalesDiscount);
            Assert.NotNull(model.SmeSupport);
            Assert.NotNull(model.TargetCustomer);
            Assert.NotNull(model.RetentionRate);
            Assert.NotNull(model.CustomerBase);
            Assert.True(model.StatusId > 0);
            Assert.True(model.ProductStatuses.Any());
            Assert.True(model.ProductStatuses.All(r => r.Id > 0 && !string.IsNullOrWhiteSpace(r.Description)));
        }

        [Fact]
        public async Task ShouldUpdateProduct()
        {
            // Arrange
            SetupProductStatuses(50);
            SetupProducts();
            SetupProductCapabilities();
            SetupProductFilters();

            // Act
            var result = await _sut.Product(1, 50, TestCompanyId) as RedirectToRouteResult;

            // Assert
            Assert.True(result != null);
            Assert.Equal("VendorProductsGet", result.RouteName);
            Assert.True(result.RouteValues.Count > 0);
            _mockHtgVendorSmeDbContext.Verify(c =>
                    c.products.Update(It.Is<product>(e =>
                        e.product_id == 1)),
                Moq.Times.Once);
            _mockHtgVendorSmeDbContext.Verify(r => r.SaveChangesAsync(default), Moq.Times.Exactly(3));
        }

        [Fact]
        public async Task ShouldGetUsers()
        {
            // Arrange
            SetupVendorStatuses();
            SetupVendorCompanies();
            SetupVendorCompanyUser();
            
            // Act
            var result = await _sut.Users(TestCompanyId) as ViewResult;

            // Assert
            Assert.True(result != null);
            var model = result.Model as VendorCompanyUserViewModel;
            Assert.NotNull(model);
            Assert.NotNull(model.CompanyName);
            Assert.NotNull(model.CompanyRegistrationNumber);
            Assert.True(model.CompanyId > 0);
            Assert.True(model.Users.Any());
            Assert.True(model.Users.All(r => !string.IsNullOrWhiteSpace(r.Email) && !string.IsNullOrWhiteSpace(r.FullName) && r.PrimaryContact));
        }

        private void SetupVendorStatuses()
        {
            var vendorStatuses = new List<vendor_status>
            {
                _autoFixture.Build<vendor_status>()
                    .With(x => x.id, TestApplicationStatus)
                    .With(x => x.status_description, "status description").Create()
            };

            var vendorStatusesDbSet = vendorStatuses.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.vendor_statuses).Returns(vendorStatusesDbSet.Object);
        }

        private void SetupVendorCompanies()
        {
            
            var vendorCompanies = new List<vendor_company>
            {
                _autoFixture.Build<vendor_company>()
                    .With(x => x.vendorid, TestCompanyId)
                    .With(x => x.registration_id, TestRegistrationId)
                    .With(x => x.access_secret, TestAccessSecret)
                    .With(x => x.application_status, TestApplicationStatus)
                    .With(x => x.ipaddresses, TestIpAddresses)
                    .With(x => x.vendor_company_users, new List<vendor_company_user>
                    {
                        _autoFixture.Build<vendor_company_user>()
                            .With(x => x.companyid, TestCompanyId)
                            .With(x => x.primary_contact, true)
                            .Create()
                    }).Create()
            };
            var vendorCompaniesDbSet = vendorCompanies.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.vendor_companies).Returns(vendorCompaniesDbSet.Object);
        }

        private void SetupVendorCompanyUser()
        {
            var vendorCompanyUsers = new List<vendor_company_user>
            {
                _autoFixture.Build<vendor_company_user>()
                    .With(x => x.companyid, TestCompanyId).Create()
            };
            var vendorCompanyUsersDbSet = vendorCompanyUsers.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.vendor_company_users).Returns(vendorCompanyUsersDbSet.Object);
        }

        private void SetupProductStatuses(long id = 1)
        {
            var productStatuses = new List<product_status>
            {
                _autoFixture.Build<product_status>()
                    .With(x => x.id, id)
                    .With(x => x.status_description, "product status").Create()
            };
            var productStatusesDbSet = productStatuses.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.product_statuses).Returns(productStatusesDbSet.Object);
        }

        private void SetupProducts(string logo = "logo")
        {
            var products = new List<product>
            {
                _autoFixture.Build<product>()
                    .With(x => x.product_id, 1)
                    .With(x => x.vendor_id, TestCompanyId)
                    .With(x => x.status, 1)
                    .With(x=> x.product_type, 1)
                    .With(x => x.product_logo, logo).Create()
            };
            var productsDbSet = products.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.products).Returns(productsDbSet.Object);
        }

        private void SetupSettingsProductTypes()
        {
            var settingsProductTypes = new List<settings_product_type>
            {
                _autoFixture.Build<settings_product_type>()
                    .With(x => x.id, 1)
                    .With(x => x.item_name, "item name").Create()
            };
            var settingsProductTypesDbSet = settingsProductTypes.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.settings_product_types).Returns(settingsProductTypesDbSet.Object);
        }

        private void SetupSettingsProductCapabilities()
        {
            var settingsProductcapabilities = new List<settings_product_capability>
            {
                _autoFixture.Build<settings_product_capability>()
                    .With(x => x.capability_id, 1)
                    .With(x => x.capability_name, "capability name").Create()
            };
            var settingsProductcapabilitiesDbSet = settingsProductcapabilities.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.settings_product_capabilities).Returns(settingsProductcapabilitiesDbSet.Object);
        }

        private void SetupProductCapabilities()
        {
            var productcapabilities = new List<product_capability>
            {
                _autoFixture.Build<product_capability>()
                    .With(x => x.capability_id, 1)
                    .With(x => x.product_id, 1).Create()
            };
            var productcapabilitiesDbSet = productcapabilities.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.product_capabilities).Returns(productcapabilitiesDbSet.Object);
        }

        private void SetupSettingsProductFiltersCategories()
        {
            var settingsProductFiltersCategories = new List<settings_product_filters_category>
            {
                _autoFixture.Build<settings_product_filters_category>()
                    .With(x => x.id, 1)
                    .With(x => x.item_name, "Support").Create(),
                _autoFixture.Build<settings_product_filters_category>()
                    .With(x => x.id, 2)
                    .With(x => x.item_name, "Training").Create(),
                _autoFixture.Build<settings_product_filters_category>()
                    .With(x => x.id, 3)
                    .With(x => x.item_name, "Platform").Create()
            };
            var settingsProductFiltersCategoriesDbSet = settingsProductFiltersCategories.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.settings_product_filters_categories).Returns(settingsProductFiltersCategoriesDbSet.Object);
        }

        private void SetupProductFilters()
        {
            var productFilters = new List<product_filter>
            {
                _autoFixture.Build<product_filter>()
                .With(x => x.Id, 1)
                    .With(x => x.product_id, 1)
                    .With(x => x.filter_id, 1).Create(),
                _autoFixture.Build<product_filter>()
                .With(x => x.Id, 2)
                    .With(x => x.product_id, 1)
                    .With(x => x.filter_id, 2).Create(),
                _autoFixture.Build<product_filter>()
                .With(x => x.Id, 3)
                    .With(x => x.product_id, 1)
                    .With(x => x.filter_id, 3).Create()
            };
            var productFiltersDbSet = productFilters.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.product_filters).Returns(productFiltersDbSet.Object);
        }

        private void SetupSettingsProductFilters()
        {
            var settingsProductFiltersFilters = new List<settings_product_filter>
            {
                _autoFixture.Build<settings_product_filter>()
                    .With(x => x.filter_id, 1)
                    .With(x => x.filter_name, "filter name")
                    .With(x => x.filter_type, 1).Create(),
                _autoFixture.Build<settings_product_filter>()
                    .With(x => x.filter_id, 2)
                    .With(x => x.filter_name, "filter name")
                    .With(x => x.filter_type, 2).Create(),
                _autoFixture.Build<settings_product_filter>()
                    .With(x => x.filter_id, 3)
                    .With(x => x.filter_name, "filter name")
                    .With(x => x.filter_type, 3).Create()
            };
            var settingsProductFiltersFiltersDbSet = settingsProductFiltersFilters.AsQueryable().BuildMockDbSet();
            _mockHtgVendorSmeDbContext.Setup(context => context.settings_product_filters).Returns(settingsProductFiltersFiltersDbSet.Object);
        }
    }
}