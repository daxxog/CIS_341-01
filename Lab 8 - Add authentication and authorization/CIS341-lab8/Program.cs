using CIS341_lab8.Data;
using CIS341_lab8.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CIS341_lab8.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("IdentityContextConnection") ??
                       throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SqliteContext>();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<IdentityContext>();

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

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
app.Use(async (context, next) =>
{
    var mode = 1;

    // Do work that can write to the Response.
    switch (mode)
    {
        case 1:
            context.Items.Add("AuthorizationStatus", new AuthorizationStatus
            {
                UserId = 1,
                IsContentManager = true,
            });
            break;
        case 2:
            context.Items.Add("AuthorizationStatus", new AuthorizationStatus
            {
                UserId = 2,
                IsContentManager = false,
            });
            break;
    }

    await next.Invoke();
});

// app.UseHttpsRedirection(); // nahh
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();