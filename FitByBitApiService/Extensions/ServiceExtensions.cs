using System.Text;
using FitByBitService.Configuration;
using FitByBitService.Data;
using FitByBitService.Entities.Models;
using FitByBitService.EventHandlers;
using FitByBitService.Handlers;
using FitByBitService.Mappings;
using FitByBitService.Repositories;
using FitByBitService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace FitByBitService.Extensions;

public static class IdentityConfigurationExtension
{
    // Cors Configuration
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
        });
        return services;
    }

    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.OperationFilter<SecurityRequirementsOperationFilter>();

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FitByBit Apis",
                Version = "v1",
                Description = "This is the directory of the APIs for FitByBit service",
                Contact = new OpenApiContact
                {
                    Name = "FitByBit API",
                    Url = new Uri("https://moola.com")
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter your valid token in the text input below. Example: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpJ9",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
        });
        return services;
    }


    // Identity configuration.
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        var maxFailedAccessAttempts = int.Parse(configuration["MaxFailedAccessAttempts"]);
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 0;
            options.SignIn.RequireConfirmedEmail = false;
            options.SignIn.RequireConfirmedAccount = false;
            options.Lockout.AllowedForNewUsers = false;
            options.Lockout.MaxFailedAccessAttempts = maxFailedAccessAttempts;
            options.Lockout.DefaultLockoutTimeSpan = DateTime.Now.AddYears(100) - DateTime.Now;
        })
        .AddTokenProvider<DataProtectorTokenProvider<User>>("ApplicationDbContext")
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
        return services;
    }

    // Jwt Configuration
    public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings"!);
        var jwtUserSecret = jwtSettings.GetSection("SecretKey").Value;

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.GetSection("ValidIssuer").Value,
                ValidAudience = jwtSettings.GetSection("ValidAudience").Value,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtUserSecret)),
            };
        });
    }

    public static WebApplication UseCustomHeaders(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
            context.Response.Headers.Add("X-Frame-Options", "DENY");
            context.Response.Headers.Add("Cache-control", "no-store");
            context.Response.Headers.Add("Pragma", "no-cache");
            context.Response.Headers.Add("Referrer-Policy", "no-referrer-when-downgrade");
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000;includeSubDomains;");
            context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
            context.Response.Headers.Add("Content-Security-Policy", "unsafe-inline 'self'");
            context.Response.Headers.Add("Feature-Policy", "accelerometer 'none'; camera 'none'; geolocation 'none'; gyroscope 'none'; magnetometer 'none'; microphone 'none';");
            await next();
        });
        return app;
    }

    public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        var smtpSettings = services.Configure<SmtpConfiguration>(configuration.GetSection("SmtpConfigurationSettings"));
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<SendMailNotificationEventHandler>());

        // automapper config
        services.AddAutoMapper(typeof(AutoMapperProfiles));

        // App Repositories and Implementations.
        services.AddTransient<IAuthRepository, AuthService>();
        services.AddTransient<IWorkOutRepository, WorkOutService>();
        services.AddTransient<IMealRepository, MealService>();

        // other services.
        services.AddTransient<IMailingHandler, MailingHandler>();
        services.AddTransient<IGenerateOtpHandler, GenerateOtpHandler>();

        // Identity, jwt & Swagger and cors configuration.
        services.ConfigureIdentity(configuration);
        services.ConfigureJwt(configuration);
        services.ConfigureSwagger();
        services.ConfigureCors();
        services.AddSingleton(smtpSettings);
        return services;
    }
}
