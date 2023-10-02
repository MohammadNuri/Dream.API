var builder = WebApplication.CreateBuilder(args);

// Add services to the container---------------------
//--MVC Controller
builder.Services.AddControllers();
//--Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//--Custom Services


//---------------------------------------------------
var app = builder.Build();


#region PipeLine

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("Hi :)");
    });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers(); //  Domain.com/Controller/Action

#endregion


app.Run();
