using FluentValidation;
using FluentValidation.AspNetCore;
using IssueManagement.Application.Accounts;
using IssueManagement.Application.Accounts.Interfaces;
using IssueManagement.Application.Issues;
using IssueManagement.Application.Issues.interfaces;
using IssueManagement.Application.Users;
using IssueManagement.Application.Users.Interfaces;
using IssueManagement.Common.CustomTokenProviders;
using IssueManagement.Domain.Models;
using IssueManagement.Infrastructure.Issues;
using IssueManagement.Infrastructure.Users;
using IssueManagement.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
namespace IssueManagement.Extensions
{
    public static class ServiceExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IIssueService, IssueService>();  
        }

        public static void UseSwaggerConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opts =>
            {
                opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Description = "Authorization"
                });

                opts.AddSecurityRequirement(new OpenApiSecurityRequirement
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

                opts.SwaggerDoc("v1", new OpenApiInfo() { Title = "Task Management API", Version = "1.0" });
            });
        }

        public static void AddIdentityService(this WebApplicationBuilder builder)
        {

            builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Password = new PasswordOptions
                {
                    RequireDigit = true,
                    RequireLowercase = true,
                    RequireNonAlphanumeric = true,
                    RequireUppercase = true,
                    RequiredLength = 8,
                    RequiredUniqueChars = 3,
                };
            })
            .AddEntityFrameworkStores<IssueManagementContext>()
            .AddTokenProvider<ResetPasswordTokenProvider<User>>("ResetPassword")
            .AddDefaultTokenProviders();
        }

        public static void AddDbConnection(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<IssueManagementContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });
        }

        public static void AddCustomValidations(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom
                .Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog(); 
            return builder;
        }
    }
}
