using BSS.DishDepot.Application.Cqrs.Users;
using BSS.DishDepot.Application.Mappers;
using BSS.DishDepot.Domain.Interfaces;
using BSS.DishDepot.Domain.Services;
using BSS.DishDepot.Infrastructure.Dal;
using BSS.DishDepot.Presentation.Interfaces;
using BSS.DishDepot.Presentation.Middleware;
using BSS.DishDepot.Presentation.Services;
using DryIoc.Microsoft.DependencyInjection;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new DryIocServiceProviderFactory());

builder.Services.AddHttpLogging(options => { });
builder.Services.AddLogging();
builder.Services.AddMvc()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = builder.Configuration.GetValue<string>("Token:Key");
    ArgumentException.ThrowIfNullOrWhiteSpace(key);

    var audience = builder.Configuration.GetValue<string>("Token:Audience");
    ArgumentException.ThrowIfNullOrWhiteSpace(audience);

    var issuer = builder.Configuration.GetValue<string>("Token:Issuer");
    ArgumentException.ThrowIfNullOrWhiteSpace(issuer);

    var keyBytes = Encoding.UTF8.GetBytes(key);
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidAudience = audience,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true
    };
});

builder.Services.AddAuthorization();

var connectionString = builder.Configuration.GetConnectionString("DishDepotDatabase")
           ?? throw new InvalidOperationException("Database connection string not found.");

builder.Services.AddDbContext<DishDepotDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddReadOnlyUnitOfWork<DishDepotDbContext>();
builder.Services.AddSqlUnitOfWork<DishDepotDbContext>();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(PostUserCommandHandler).Assembly));

builder.Services.AddControllers();

builder.Services.AddSingleton<IApiResultBuilder, ApiResultBuilder>();

builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddScoped<IIdentityContextAccessor, IdentityContextAccessor>();

var config = new TypeAdapterConfig();
config.Default.Settings.IgnoreNullValues = true;
config.Default.Settings.PreserveReference = true;

config.Scan(typeof(UserMapper).Assembly);

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, Mapper>();

var app = builder.Build();

app.UseHttpLogging();

app.UseAuthentication();
app.UseMiddleware<IdentityContextMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
