using IssueManagement.Application.EmailSender.Extensions;
using IssueManagement.Extensions;
using IssueManagement.Infrastructure.Auth;
using IssueManagement.Infrastructure.Mappings;

namespace IssueManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            builder.Services.AddServices();
            builder.Services.AddMailSender(builder.Configuration);
            builder.AddIdentityService();

            builder.Services.AddJwtBearerAuthentication(builder.Configuration);

            builder.Services.AddAuthorization();

            builder.Services.RegisterMaps();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddCustomValidations();

            builder.UseSwaggerConfiguration();
            builder.AddSerilog();
            builder.AddDbConnection();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.ConfigureCustomExceptionMiddleware();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
