using Beis.HelpToGrow.Common.Helpers;
using Beis.HelpToGrow.Common.Services.HealthChecks;
using Beis.HelpToGrow.Persistence;
using Beis.ManagementConsole.Web.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration(configBuilder =>
{
    var connectionString = configBuilder.Build().GetConnectionString("AppConfig");
    if (connectionString != null)
    {
        configBuilder.AddAzureAppConfiguration(connectionString);
    }
});

// Add services to the container.
builder.Services.AddMvc(o => o.EnableEndpointRouting = false);
builder.Services.RegisterAllServices(builder.Configuration);
builder.Services.AddHealthChecks()
    .AddDbContextCheck<HtgVendorSmeDbContext>("VendorSme Database")
    .AddCheck<DependencyInjectionHealthCheckService>("Dependency Injection");

// Configure the HTTP request pipeline.
var app = builder.Build();
app.UseForwardedHeaders();

if (app.Environment.IsDevelopment())
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

app.UseMvc(r => r.MapRoute("default", "{controller=Vendor}/{action=Index}/{id?}"));
app.MapHealthChecks("/healthz", new HealthCheckOptions
{
    ResponseWriter = HealthCheckJsonResponseWriter.Write
});
app.Run();