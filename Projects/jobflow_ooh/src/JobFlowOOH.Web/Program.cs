using JobFlowOOH.Application.Abstractions;
using Microsoft.Identity.Web;
using JobFlowOOH.Application.Options;
using JobFlowOOH.Infrastructure;
using JobFlowOOH.Infrastructure.Portal;
using JobFlowOOH.JobFlow;

var builder = WebApplication.CreateBuilder(args);

PortalDatabaseGuard.EnsureSeparate(
    builder.Configuration.GetConnectionString("JobFlowReadOnly"),
    builder.Configuration.GetConnectionString("PortalDatabase"));
PortalDatabaseGuard.EnsureSeparate(
    builder.Configuration.GetConnectionString("JobFlowExperimental"),
    builder.Configuration.GetConnectionString("PortalDatabase"));

var entraConfigured = !string.IsNullOrWhiteSpace(builder.Configuration["AzureAd:ClientId"]);
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromHours(8);
});
builder.Services.AddRazorPages();
if (entraConfigured)
{
    builder.Services
        .AddAuthentication(Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectDefaults.AuthenticationScheme)
        .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
    builder.Services.AddAuthorization();
}
else if (!builder.Environment.IsDevelopment())
{
    throw new InvalidOperationException("AzureAd:ClientId is required outside Development.");
}

builder.Services.AddJobFlow(builder.Configuration, builder.Environment.IsDevelopment());
builder.Services.AddPortalInfrastructure(builder.Configuration);

var app = builder.Build();

if (string.Equals(app.Configuration["JobFlowWrites:Mode"], JobFlowWriteModes.ExperimentalDirectSql, StringComparison.OrdinalIgnoreCase))
{
    await app.Services.GetRequiredService<PortalDatabaseInitializer>().EnsureAsync();
}

var writer = app.Services.GetRequiredService<IJobFlowJobWriter>();
app.Logger.LogInformation(
    "JobFlow write mode is {Mode}. Registered writer is {Writer}. Experimental writes are not a production or supplier-supported integration.",
    app.Configuration["JobFlowWrites:Mode"] ?? "Disabled",
    writer.GetType().Name);

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
if (entraConfigured)
{
    app.UseAuthentication();
    app.UseAuthorization();
}

app.MapRazorPages();
app.MapGet("/health", () => Results.Text("ok"));
app.Run();

public partial class Program;
