using Microsoft.EntityFrameworkCore;
using Cep.Domain.Interfaces.Gateway;
using Cep.Domain.Interfaces.Service;
using Cep.Infra.Gateway;
using Cep.Service.Service;
using Cep.Infra.Context;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Cep.Domain.Interfaces.Repository;
using Cep.Infra.Repository;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("CepDb");
var versionDb = ServerVersion.Create(new Version("8.0"), ServerType.MySql);
builder.Services.AddDbContext<CepContext>(options => 
    options.UseMySql(connectionString, versionDb, b=> b.MigrationsAssembly("Cep.Api").EnableRetryOnFailure()));


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<INeighborhoodRepository, NeighborhoodRepository>();
builder.Services.AddScoped<IStreetRepository, StreetRepository>();


builder.Services.AddHttpClient<IBrasilApiGateway, BrasilApiGateway>().ConfigurePrimaryHttpMessageHandler(_ => {
    return new HttpClientHandler();
});

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.MapControllers();

app.Run();
