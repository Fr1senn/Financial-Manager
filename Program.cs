using financial_manager.Entities;
using financial_manager.Repositories;
using financial_manager.Repositories.Interfaces;
using financial_manager.Services;
using financial_manager.Services.Interfaces;
using financial_manager.Utilities;
using financial_manager.Utilities.Interfaces;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<FinancialManagerContext>();
builder.Services.AddSingleton<IConnectionMultiplexer>(options => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!));

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtUtility, JwtUtility>();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepositoty>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("Cors");

app.Run();
