using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SimpleStorageService.Factory;
using SimpleStorageService.Models;
using SimpleStorageService.Services;
using SimpleStorageService.Strategy.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add a singleton service for the storage logic
builder.Services.AddSingleton<StorageServices>();
builder.Services.AddSingleton<StorageFactory>();//Singleton? based on shared class 
builder.Services.AddScoped<IStorage>(serviceProvider =>//scoped? different configurations based on runtime conditions 
{
    var factory = serviceProvider.GetRequiredService<StorageFactory>();
    return factory.CreateStorage();
});
builder.Services.Configure<StorageSettings>(builder.Configuration.GetSection("StorageSettings"));
//// Configure authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "SimpleDriveIssuer",
            ValidAudience = "SimpleDriveAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecureSecretKey"))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();

app.MapControllers();

app.Run();

