var builder = WebApplication.CreateBuilder(args);

// needed for basic logging
// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-6.0
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection(); // nahh
app.UseStaticFiles();

app.UseRouting();


// status code redirection middleware
app.UseStatusCodePagesWithRedirects("/StatusCode/{0}");

app.UseAuthorization();

app.MapRazorPages();

app.Run();