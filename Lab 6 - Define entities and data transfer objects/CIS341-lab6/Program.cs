using CIS341_lab6.Data;
using CIS341_lab6.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SqliteContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-6.0
app.Use(async (context, next) =>
{
    // Do work that can write to the Response.
    context.Items.Add("AuthorizationStatus", new AuthorizationStatus
    {
        UserId = 2,
        IsContentManager = false,
    });
    await next.Invoke();
});

// app.UseHttpsRedirection(); // nahh
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();