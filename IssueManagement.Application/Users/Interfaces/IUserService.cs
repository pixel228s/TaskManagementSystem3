using IssueManagement.Application.Users.Requests;
using IssueManagement.Application.Users.Responses;

namespace IssueManagement.Application.Users.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseModel> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<UserResponseModel> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<UserResponseModel> UpdateAsync(UserUpdateModel updateModel, int id, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
