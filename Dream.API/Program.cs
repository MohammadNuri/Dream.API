using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

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
