using Beis.HelpToGrow.Console.Web.Attributes;
using Beis.HelpToGrow.Console.Web.Options;
using Beis.HelpToGrow.Core.Repositories;
using Beis.HelpToGrow.Core.Repositories.Interface;
using Beis.Htg.VendorSme.Database;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using System.Net.Mime;
using System.Reflection;

namespace Beis.HelpToGrow.Console.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(options => { options.AddConsole(); });

            services.AddApplicationInsightsTelemetry(_configuration["AzureMonitorInstrumentationKey"]);

            services.AddMvc();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                // Handling SameSite cookie according to https://docs.microsoft.com/en-us/aspnet/core/security/samesite?view=aspnetcore-3.1
                options.HandleSameSiteCookieCompatibility();
            });

            services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
            .AddMicrosoftIdentityWebApp(options => _configuration.Bind("AzureAdB2C", options));

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
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>();
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

            services.AddDbContext<HtgVendorSmeDbContext>(options => options.UseNpgsql(_configuration["HelpToGrowDbConnectionString"]));
            services.AddDataProtection().PersistKeysToDbContext<HtgVendorSmeDbContext>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductFiltersRepository, ProductFiltersRepository>();
            services.AddScoped<ISettingsProductFiltersCategoriesRepository, SettingsProductFiltersCategoriesRepository>();
            services.AddScoped<ISettingsProductFiltersRepository, SettingsProductFiltersRepository>();
            services.AddScoped<ISettingsProductTypesRepository, SettingsProductTypesRepository>();
            services.AddScoped<ISettingsProductCapabilitiesRepository, SettingsProductCapabilitiesRepository>();
            services.AddScoped<IProductCapabilitiesRepository, ProductCapabilitiesRepository>();
            services.AddScoped<IVendorCompanyRepository, VendorCompanyRepository>();
            services.AddScoped<IVendorCompanyUserRepository, VendorCompanyUserRepository>();
            services.AddScoped<IVendorCompanyStatusRepository, VendorCompanyStatusRepository>();
            services.Configure<LogoInformationOption>(_configuration.GetSection(LogoInformationOption.LogoInformation));

            services.AddRazorPages();

            services.AddOptions();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.Secure = CookieSecurePolicy.Always;
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto |
                    ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedHost;
                options.ForwardedHostHeaderName = "X-Original-Host";
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddAutoMapper(c => c.AddProfile<AutoMap>(), typeof(Startup));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Vendor}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}