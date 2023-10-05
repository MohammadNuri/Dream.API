using Dream.API;
using Dream.API.DbContext;
using Dream.API.Services;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
    

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container---------------------
//--MVC Controller
builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
//--Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();
//--Custom Services
#if DEBUG
builder.Services.AddScoped<IMailService, LocalMailService>();
#else
builder.Services.AddScoped<IMailService, ReleaseMailService>();
#endif
builder.Services.AddSingleton<CitiesDataStore>();
builder.Services.AddDbContext<DreamApiDbContext>(option =>
{
    option.UseSqlite(
        builder.Configuration["ConnectionStrings:SqliteDreamApi"]
        );
});



//---------------------------------------------------
var app = builder.Build();


#region PipeLine

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();


app.MapControllers();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

#endregion

app.Run();