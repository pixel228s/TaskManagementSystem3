using IssueManagement.Infrastructure.EmailSender.interfaces;
using IssueManagement.Infrastructure.EmailSender.models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IssueManagement.Infrastructure.EmailSender.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddMailSender(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailSender, EmailSender>();
            services.Configure<EmailSenderOptions>(configuration.GetSection(EmailSenderOptions.Key));

            return services;
        }
    }
}
