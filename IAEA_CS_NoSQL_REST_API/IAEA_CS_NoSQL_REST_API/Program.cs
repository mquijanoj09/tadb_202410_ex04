using IAEA_CS_NoSQL_REST_API.DbContexts;
using IAEA_CS_NoSQL_REST_API.Interfaces;
using IAEA_CS_NoSQL_REST_API.Repositories;
using IAEA_CS_NoSQL_REST_API.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Aqui agregamos los servicios requeridos

//El DBContext a utilizar
builder.Services.AddSingleton<MongoDbContext>();

//Los repositorios
builder.Services.AddScoped<IReactorRepository, ReactorRepository>();
builder.Services.AddScoped<ITipoRepository, TipoRepository>();
builder.Services.AddScoped<IUbicacionRepository, UbicacionRepository>();

//Aqui agregamos los servicios asociados para cada EndPoint
builder.Services.AddScoped<ReactorService>();
builder.Services.AddScoped<TipoService>();
builder.Services.AddScoped<UbicacionService>();

// Add services to the container

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Reactores Nucleares Investigación - MongoDB Version",
        Description = "API para la gestión de Reactores Nucleares Investigación"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Modificamos el encabezado de las peticiones para ocultar el web server utilizado
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Server", "ReactoresServer");
    await next();
});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
