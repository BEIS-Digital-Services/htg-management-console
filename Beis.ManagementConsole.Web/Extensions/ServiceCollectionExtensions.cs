using Beis.HelpToGrow.Persistence;
using Beis.ManagementConsole.Repositories;
using Beis.ManagementConsole.Web.Attributes;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Net.Mime;
using System.Reflection;

namespace Beis.ManagementConsole.Web.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddLogging(options => { options.AddConsole(); });

            services.AddApplicationInsightsTelemetry(configuration["AzureMonitorInstrumentationKey"]);

            services.AddMvc();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                // Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1
                options.HandleSameSiteCookieCompatibility();
            });

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(options => configuration.Bind("AzureAdB2C", options));

            services.AddControllersWithViews(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddMicrosoftIdentityUI();

            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionAttribute()))
                .AddFluentValidation(fv =>
                {
                    fv.DisableDataAnnotationsValidation = true;
                    fv.ImplicitlyValidateChildProperties = true;
                    fv.RegisterValidatorsFromAssemblyContaining<Program>();
                })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var result = new BadRequestObjectResult(context.ModelState);

                        result.ContentTypes.Add(MediaTypeNames.Application.Json);
                        result.ContentTypes.Add(MediaTypeNames.Application.Xml);

                        return result;
                    };
                });

            services.AddDbContext<HtgVendorSmeDbContext>(options =>
                options.UseNpgsql(configuration["HelpToGrowDbConnectionString"]));
            services.AddDataProtection().PersistKeysToDbContext<HtgVendorSmeDbContext>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductFiltersRepository, ProductFiltersRepository>();
            services
                .AddScoped<ISettingsProductFiltersCategoriesRepository, SettingsProductFiltersCategoriesRepository>();
            services.AddScoped<ISettingsProductFiltersRepository, SettingsProductFiltersRepository>();
            services.AddScoped<ISettingsProductTypesRepository, SettingsProductTypesRepository>();
            services.AddScoped<ISettingsProductCapabilitiesRepository, SettingsProductCapabilitiesRepository>();
            services.AddScoped<IProductCapabilitiesRepository, ProductCapabilitiesRepository>();
            services.AddScoped<IVendorCompanyRepository, VendorCompanyRepository>();
            services.AddScoped<IVendorCompanyUserRepository, VendorCompanyUserRepository>();
            services.AddScoped<IVendorCompanyStatusRepository, VendorCompanyStatusRepository>();
            services.Configure<Options.ServiceCollectionExtensions>(
                configuration.GetSection(Options.ServiceCollectionExtensions.LogoInformation));

            services.AddRazorPages();

            services.AddOptions();
            services.Configure<CookiePolicyOptions>(options => { options.Secure = CookieSecurePolicy.Always; });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto |
                                           ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedHost;
                options.ForwardedHostHeaderName = "X-Original-Host";
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddAutoMapper(c => c.AddProfile<AutoMap>(), typeof(Program));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}