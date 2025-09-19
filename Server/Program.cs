using EFModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<MachineVirtuelleService>();
builder.Services.AddScoped<SalleDeFormationService>();
builder.Services.AddScoped<SecurityService>();
builder.Services.AddScoped<UtilisateurService>();
builder.Services.AddControllers();
builder.Services.AddDbContext<EClassRoomDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClient",
        policy =>
        {
            policy.WithOrigins("http://localhost:5132", "https://localhost:5001") // URLs du client Blazor
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

var secret = builder.Configuration["JwtCredentials:Secret"];
var secret2 = builder.Configuration.GetSection("JwtCredentials")["Secret"];
var audience = builder.Configuration.GetSection("JwtCredentials")["Audience"];
var key = builder.Configuration.GetSection("JwtCredentials")["Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false, // Enable/disable issuer validation
            //ValidIssuer = issuer,  // Set your expected issuer

            ValidateAudience = true, // Enable/disable audience validation
            ValidAudience = audience, // Set your expected audience

            ValidateLifetime = true, // Enable/disable expiration check

            ValidateIssuerSigningKey = true, // Enable signature validation
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)) //,

            // Optional: Custom clock skew (default is 5 min)
            //ClockSkew = TimeSpan.FromMinutes(2)
        };
    });
var app = builder.Build();

app.UseCors("AllowClient"); // Placez ceci AVANT UseAuthentication et UseAuthorization
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();
