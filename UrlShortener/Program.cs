using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using UrlShortener.DataAccess.DbContexts;
using UrlShortener.DataAccess.Repositories;
using UrlShortener.Interfaces;
using UrlShortener.Middlewares;
using UrlShortener.Policies.UrlOwnerPolicy;
using UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UrlShortenerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DataBaseConnection"]);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Host.UseSerilog((context, configuration) => 
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUrlRepository, UrlRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUrlService, UrlService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = builder.Configuration["JwtTokenConfiguration:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtTokenConfiguration:AccessTokenKey"] ?? string.Empty))
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IsUrlOwnerOrAdmin", policy =>
        policy.AddRequirements(new UrlOwnerRequirement()));
});

builder.Services.AddScoped<IAuthorizationHandler, UrlOwnerHandler>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(options =>
{
    options.GetLevel = (httpContext, elapsed, ex) => 
        httpContext.Response.StatusCode == 200 ? 
            LogEventLevel.Verbose : LogEventLevel.Information;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();