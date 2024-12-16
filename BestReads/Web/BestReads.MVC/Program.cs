using BestReads.Data;
using BestReads.Data.Common;
using BestReads.Data.Common.Repositories;
using BestReads.Data.Models;
using BestReads.Data.Repositories;
using BestReads.Data.Seeding;
using BestReads.Services;
using BestReads.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;


// Add services to the container.
services.AddControllersWithViews();
services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
services.AddIdentity<ApplicationUser, ApplicationRole>(options => IdentityOptionsProvider.GetIdentityOptions(options))
    .AddRoles<ApplicationRole>()
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddRoleValidator<RoleValidator<ApplicationRole>>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

services.AddSingleton(configuration);


services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
services.AddScoped<IDbQueryRunner, DbQueryRunner>();

services.AddTransient<IGenreService, GenreService>();
services.AddTransient<IBookService, BookService>();
services.AddTransient<IBookEditionService, BookEditionService>();


services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AllowAnonymousToPage("/auth/login");
    });

services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
});
//.AddRazorOptions();

services.AddAuthentication();
services.AddAuthorization();


var app = builder.Build();


/*
 services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        // options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
        options.Conventions.AllowAnonymousToPage("/login");


        // options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "/login");
    });

services.AddSingleton<TimeProvider>(TimeProvider.System);

services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
 * */

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
    await new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceProvider);

    var environment = app.Environment.EnvironmentName;
    var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(Program));
    logger.LogInformation($"Environment: {environment}");

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
