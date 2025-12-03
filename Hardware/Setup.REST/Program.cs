using Microsoft.EntityFrameworkCore;
using Setup.Infrastructure;
using Setup.Infrastructure.Repositories;
using Setup.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextFactory<SetupContext>(options =>
{
    var dbPath = @"D:\lb1 net\NUPP_NET_2025_402_TN_Storozhuk_Lab\Hardware\Hardware.Console\bin\Debug\net8.0\SetupDatabase.db";
    options.UseSqlite($"Data Source={dbPath}");
});




builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(ICrudServiceAsyncDB<>), typeof(CrudService<>));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
