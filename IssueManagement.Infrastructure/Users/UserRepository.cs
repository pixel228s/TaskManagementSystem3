using IssueManagement.Application.Users.Interfaces;
using IssueManagement.Domain.Models;
using IssueManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace IssueManagement.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IssueManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Include(x => x.Issues)
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await base.GetAsync(id, cancellationToken);
        }

        public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Include(x => x.Issues)
                .FirstOrDefaultAsync(x => x.UserName == username, cancellationToken);
        }

        public async Task<int> GetUserIssueCountAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(x => x.Id == userId)
                .Select(x => x.Issues.Count())
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateUserAsync(User user, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(cancellationToken, user);
        }
    }
}
