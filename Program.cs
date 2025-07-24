using DragerBackendTemplate.SOLID.Helpers;
using DragerBackendTemplate.SOLID.Interfaces;
using DragerBackendTemplate.SOLID.Models;
using DragerBackendTemplate.SOLID.Repositores;
using DragerBackendTemplate.SOLID.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// 1. Config MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("MongoDbSettings:ConnectionString")));

// 2. Inyección de dependencias (DI) para SOLID
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

// 3. Configurar CORS para Unity/WebGL
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUnity", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// 4. Controladores, Swagger, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5. Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS debe ir antes de autorización y endpoints
app.UseCors("AllowUnity");

app.UseAuthorization();

app.MapControllers();

app.Run();