using Setup.MVC.Components;
using Microsoft.EntityFrameworkCore;
using Setup.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContextFactory<SetupContext>(options =>
{
    var dbPath = @"D:\lb1 net\NUPP_NET_2025_402_TN_Storozhuk_Lab\Hardware\Hardware.Console\bin\Debug\net8.0\SetupDatabase.db";
    options.UseSqlite($"Data Source={dbPath}");
});

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
