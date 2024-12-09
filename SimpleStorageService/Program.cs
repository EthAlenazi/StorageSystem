using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleStorageService.Factory;
using SimpleStorageService.Models;
using SimpleStorageService.Services.Helpers;
using SimpleStorageService.Strategy.Implementation;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add settings to DI
var storageTypes =
    builder.Services.Configure<StorageSettings>(
        builder.Configuration.GetSection("StorageSettings"));

// Add specific storage services to DI
builder.Services.AddScoped<S3Storage>();
builder.Services.AddScoped<DatabaseStorage>();
builder.Services.AddScoped<LocalFileStorage>();
builder.Services.AddScoped<FtpStorage>();

// Register the StorageFactory
builder.Services.AddSingleton<StorageFactory>();

// Register StorageHandler with dynamic storages
builder.Services.AddScoped<StorageHandler>(serviceProvider =>
{
    var factory = serviceProvider.GetRequiredService<StorageFactory>();
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    // Fetch storage types from configuration or use a default list
    var storageTypes = configuration.GetSection("StorageSettings:EnabledTypes").Get<string[]>()
                      ?? new[] { "LocalFileSystem" };//"AmazonS3", "Database", 

    // Create storages dynamically
    var storages = factory.CreateStorages(storageTypes);
    return new StorageHandler(storages);
});


// Bind settings from configuration
builder.Services.Configure<AmazonS3Settings>(
    builder.Configuration.GetSection("StorageSettings:AmazonS3"));
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("StorageSettings:Database"));
builder.Services.Configure<FtpSettings>(
    builder.Configuration.GetSection("StorageSettings:FTP"));

// Register strongly-typed settings for direct injection
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<AmazonS3Settings>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<LocalFileSystemSettings>>().Value);
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<FtpSettings>>().Value);
// Configure authentication
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

