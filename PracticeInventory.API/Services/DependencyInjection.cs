using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PracticeInventory.API.Services.Authentication;
using PracticeInventory.Application.Account.Validator;
using PracticeInventory.Application.Category.Queries;
using PracticeInventory.Domain.Enums;
using PracticeInventory.Domain.Interfaces;
using PracticeInventory.Infrastucture.Authentication;
using PracticeInventory.Persistence;
using PracticeInventory.Persistence.Repositories;
using PracticeInventory.Persistence.Seeds;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PracticeInventory.API.Services;

public static class DependencyInjection
{
    public static IServiceCollection AddSwaggerService(this IServiceCollection services)
    {
        if (services is null)
        {
            throw new ArgumentNullException(nameof(services));
        }
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Clean Architecture API",
                Description = "--"
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }});
            options.CustomSchemaIds(x => x.FullName);
        });
        return services;
    }
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<RegisterCommandValidator>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetCategoriesQuery).GetTypeInfo().Assembly));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
    public static IServiceCollection AddAuthorizationPolicy(this IServiceCollection services)
    {
        services.AddControllers(opt =>
        {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("InventoryPolicy", policy => policy.RequireRole(DefaultUserRole.Admin, DefaultUserRole.RegularUser));
        });
        return services;
    }
    public static IServiceCollection AddJWTAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer();
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        return services;
    }
    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }
    public static IServiceCollection AddDBContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(option => {
            option.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
        return services;
    }
    public static IServiceCollection AddDapperConnection(this IServiceCollection services)
    {
        services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
        return services;
    }
    public static async Task<IApplicationBuilder> AddDbMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        await SeedCategories.SeedData(scope.ServiceProvider);
        await SeedRoles.SeedData(scope.ServiceProvider);
        await SeedUsers.SeedData(scope.ServiceProvider);
        return app;
    }
}