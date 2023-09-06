using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Paquigroup.Ecommerce.Application.Interface;
using Paquigroup.Ecommerce.Application.Main;
using Paquigroup.Ecommerce.Domain.Core;
using Paquigroup.Ecommerce.Domain.Interface;
using Paquigroup.Ecommerce.Infrastructura.Data;
using Paquigroup.Ecommerce.Infrastructura.Interface;
using Paquigroup.Ecommerce.Infrastructura.Repository;
using Paquigroup.Ecommerce.Transversal.Common;
using Paquigroup.Ecommerce.Transversal.Logging;
using Paquigroup.Ecommerce.Transversal.Mapper;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "The API",
        Version = "v1",
        Description = "A simple example ASP.NET Core web API"
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line
    c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
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

//builder.Services.AddSingleton<IConfigurationBuilder>(config);
builder.Services.AddAutoMapper(x => x.AddProfile(new MappingsProfile()));
builder.Services.AddScoped<IConnectionFactory, ConnectionFactory>();
builder.Services.AddScoped<ICustomersDomain, CustomerDomain>();
builder.Services.AddScoped<ICustomerApplication, CustomerApplication>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserDomain, UserDomain>();
builder.Services.AddScoped<IUserApplication, UserApplication>();
builder.Services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
//<IAppLogger<CustomerApplication>, IAppLogger<CustomerApplication>>();


builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(x =>
{
    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var userId = int.Parse(context.Principal.Identity.Name);
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
    var Configuration = builder.Configuration;
    var secret = builder.Configuration["Config:Secret"];
    var Issuer = builder.Configuration["Config:Issuer"];
    var Audience = builder.Configuration["Config:Audience"];
    var key = Encoding.UTF8.GetBytes(secret);

    x.RequireHttpsMetadata = false;
    x.SaveToken = false;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = Issuer,
        ValidateAudience = true,
        ValidAudience = Audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

});

var app = builder.Build();
//JWT Configuration


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
app.UseAuthentication();

app.MapControllers();

app.Run();
