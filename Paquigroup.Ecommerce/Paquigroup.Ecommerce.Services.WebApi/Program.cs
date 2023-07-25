using Microsoft.OpenApi.Models;
using Paquigroup.Ecommerce.Application.Interface;
using Paquigroup.Ecommerce.Application.Main;
using Paquigroup.Ecommerce.Domain.Core;
using Paquigroup.Ecommerce.Domain.Interface;
using Paquigroup.Ecommerce.Infrastructura.Data;
using Paquigroup.Ecommerce.Infrastructura.Interface;
using Paquigroup.Ecommerce.Infrastructura.Repository;
using Paquigroup.Ecommerce.Transversal.Common;
using Paquigroup.Ecommerce.Transversal.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "The API", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
});
var devCorsPolicy = "devCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(devCorsPolicy, builder =>
    {
        //builder.WithOrigins("http://localhost:800").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost");
        //builder.SetIsOriginAllowed(origin => true);
    });
});

builder.Services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));
builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>();
builder.Services.AddScoped<ICustomersDomain, CustomerDomain>();
builder.Services.AddScoped<ICustomerApplication, CustomerApplication>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI((c =>
    {
        c.SwaggerEndpoint("./v1/swagger.json", "My API V1"); //originally "./swagger/v1/swagger.json"
    }));
    app.UseDeveloperExceptionPage();
    app.UseCors(devCorsPolicy);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
