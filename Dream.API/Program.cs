using Dream.API.Services;
using Microsoft.AspNetCore.StaticFiles;
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
builder.Services.AddScoped<LocalMailService>();



//---------------------------------------------------
var app = builder.Build();


#region PipeLine

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("Error!!!");
    });
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

#endregion


app.Run();
