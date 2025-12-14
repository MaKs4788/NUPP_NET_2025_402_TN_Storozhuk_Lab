using Microsoft.EntityFrameworkCore;
using Setup.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Додаємо контекст БД
builder.Services.AddDbContext<SetupContext>(options =>
{
    var dbPath = @"D:\lb1 net\NUPP_NET_2025_402_TN_Storozhuk_Lab\Hardware\Hardware.Console\bin\Debug\net8.0\SetupDatabase.db";
    options.UseSqlite($"Data Source={dbPath}");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllers();
app.MapRazorPages();
app.MapBlazorHub();

app.Run();
