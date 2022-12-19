using CIS341_checkpoint3.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using CIS341_checkpoint3.Areas.Identity.Data;
using CIS341_checkpoint3.Middleware;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ??
                       throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

// https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity-configuration?view=aspnetcore-6.0#password
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 24;
    options.Password.RequiredUniqueChars = 12;
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SqliteContext>();
// "Register the dbcontext before you configure the Identity store"
// https://stackoverflow.com/a/63104481
builder.Services.AddDbContext<IdentityContext>();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityContext>();

// https://learn.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-6.0#require-authenticated-users
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddScoped<IdentityMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Setup the database
// https://stackoverflow.com/a/38418080
// https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.sqlitedbcontextoptionsbuilderextensions.usesqlite?view=efcore-6.0
// https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.dbcontextoptionsbuilder?view=efcore-6.0
using (var _context = new SqliteContext(Microsoft.EntityFrameworkCore.SqliteDbContextOptionsBuilderExtensions
           .UseSqlite(new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<SqliteContext>()).Options))
{
    var tdg = new TestDataGenerator();

    // not the best way to go about this, but works for testing
    try
    {
        tdg.generate(_context);
        Console.WriteLine("database generated");
    }
    catch (Exception e)
    {
        // probably because we already have the data, but
        // also could be some other error so we print it out
        // blanket handling exceptions like this is bad practice
        Console.WriteLine(e.StackTrace);
        Console.WriteLine("This is fine ;)");
    }
}

using (var _context = new IdentityContext(Microsoft.EntityFrameworkCore.SqliteDbContextOptionsBuilderExtensions
           .UseSqlite(new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<IdentityContext>()).Options))
{
    var dig = new DefaultIdentitiesGenerator();

    // not the best way to go about this, but works for testing
    try
    {
        dig.generate(_context);
        Console.WriteLine("identity database generated");
    }
    catch (Exception e)
    {
        // probably because we already have the data, but
        // also could be some other error so we print it out
        // blanket handling exceptions like this is bad practice
        Console.WriteLine(e.StackTrace);
        Console.WriteLine("<Identity> This is fine ;)");
    }
}


// app.UseHttpsRedirection(); // nahh
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
app.UseMiddleware<IdentityMiddleware>();

// Disable registration (and other account endpoints we don't want)
// based on:
// https://stackoverflow.com/a/65476351
app.UseEndpoints(endpoints =>
{
    Action<String> disableAccountIdentityAction = (actionName) =>
    {
        endpoints.MapGet($"/Identity/Account/{actionName}",
            context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
        endpoints.MapPost($"/Identity/Account/{actionName}",
            context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true, true)));
    };

    disableAccountIdentityAction("Register");
    disableAccountIdentityAction("ForgotPassword");
    disableAccountIdentityAction("ResendEmailConfirmation");
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();