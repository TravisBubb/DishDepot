using BSS.DishDepot.Application.Cqrs.Users;
using BSS.DishDepot.Application.Mappers;
using BSS.DishDepot.Infrastructure.Dal;
using BSS.DishDepot.Presentation.Interfaces;
using BSS.DishDepot.Presentation.Services;
using DryIoc.Microsoft.DependencyInjection;
using Example;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new DryIocServiceProviderFactory());

builder.Services.AddLogging();
builder.Services.AddMvc()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var connectionString = builder.Configuration.GetConnectionString("DishDepotDatabase")
           ?? throw new InvalidOperationException("Database connection string not found.");

builder.Services.AddDbContext<DishDepotDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddReadOnlyUnitOfWork<DishDepotDbContext>();
builder.Services.AddSqlUnitOfWork<DishDepotDbContext>();

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(PostUserCommandHandler).Assembly));

builder.Services.AddControllers();

builder.Services.AddSingleton<IApiResultBuilder, ApiResultBuilder>();

var config = new TypeAdapterConfig();
config.Default.Settings.IgnoreNullValues = true;
config.Default.Settings.PreserveReference = true;

config.Scan(typeof(UserMapper).Assembly);

builder.Services.AddSingleton(config);
builder.Services.AddScoped<IMapper, Mapper>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
