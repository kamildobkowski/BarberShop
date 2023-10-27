using System.Text;
using BarberShop;
using BarberShop.Data;
using BarberShop.Middleware;
using BarberShop.Services;
using BarberShop.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

builder.Services.AddAuthentication(option =>
{
	option.DefaultAuthenticateScheme = "Bearer";
	option.DefaultScheme = "Bearer";
	option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
	cfg.RequireHttpsMetadata = false;
	cfg.SaveToken = true;
	cfg.TokenValidationParameters = new TokenValidationParameters
	{
		ValidIssuer = authenticationSettings.JwtIssuer,
		ValidAudience = authenticationSettings.JwtIssuer,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
	};
});
// Add services to the container.
builder.Services.AddDbContext<BarberShopDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IShopService, ShopService>();
builder.Services.AddSingleton<ILocationService, LocationService>();
builder.Services.AddScoped<IServicesService, ServicesService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();