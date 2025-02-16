using IssueManagement.Application.Issues.responses;
using IssueManagement.Application.Users.Responses;
using IssueManagement.Domain.Models;
using Mapster;

namespace IssueManagement.Infrastructure.Mappings
{
    public static class MapsterConfigurations
    {
        public static void RegisterMaps(this IServiceCollection services)
        {

            TypeAdapterConfig<User, UserResponseModel>
             .NewConfig()
             .TwoWays();

            TypeAdapterConfig<Issue, IssueResponse>
             .NewConfig()
             .TwoWays();

        }
    }
}
