using Application.Users;
using Domain.Teams;
using Domain.Users;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Teams;
using Infrastructure.Persistence.Users;
using Infrastructure.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =======================
// 1. Load configuration
// =======================
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// =======================
// 2. Bind JwtSettings
// =======================
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>()!;

// =======================
// 3. Configure CORS
// =======================
builder.Services.AddCors(options =>
{
  options.AddPolicy("ReactAppPolicy", policy =>
  {
    policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials();
  });
});

// =======================
// 4. Register DbContext
// =======================
builder.Services.AddDbContext<VolleyballDbContext>(options =>
{
  var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
  options.UseSqlServer(connectionString);
});

// =======================
// 5. Register application services
// =======================
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITeamRepository, TeamRepository>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<ITokenService>(sp =>
{
  return new JwtTokenService(jwtSettings.Secret, jwtSettings.ExpiryMinutes);
});

// =======================
// 6. Controllers & Swagger
// =======================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =======================
// 7. Middleware pipeline
// =======================
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors("ReactAppPolicy");
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// =======================
// 8. Run app
// =======================
app.Run();
