using JobTracker.Core.Interfaces;
using JobTracker.Data.Data;
using JobTracker.Data.Services;
using JobTracker.Web.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add EF Core with PostgreSQL
builder.Services.AddDbContext<JobTrackerDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();
builder.Services.AddScoped<ICvProfileService, CvProfileService>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. 
    // Todo:
    // Read if this needs changingfor production scenarios at a later date, see https://aka.ms/aspnetcore-hsts.
    // Security feature to read up on and consider implementing in the future: https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-8.0&tabs=visual-studio


    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
