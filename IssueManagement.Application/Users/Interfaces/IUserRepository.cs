using IssueManagement.Domain.Models;


namespace IssueManagement.Application.Users.Interfaces
{
    public interface IUserRepository
    {
        Task<int> GetUserIssueCountAsync(int userId, CancellationToken cancellationToken);
        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task UpdateUserAsync(User user, CancellationToken cancellationToken);
    }
}
