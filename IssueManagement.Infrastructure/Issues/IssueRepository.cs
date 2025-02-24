using IssueManagement.Application.Issues.interfaces;
using IssueManagement.Domain.Enums;
using IssueManagement.Domain.Models;
using IssueManagement.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace IssueManagement.Infrastructure.Issues
{
    public class IssueRepository : BaseRepository<Issue>, IIssueRepository
    {
        public IssueRepository(IssueManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateIssue(Issue issue, CancellationToken cancellationToken)
        {
            await base.CreateAsync(cancellationToken, issue);
        }

        public async Task DeleteIssueById(Issue issue, CancellationToken cancellationToken)
        {
            await base.DeleteAsync(cancellationToken, issue);
        }

        public async Task<Issue?> GetIssueById(int id, CancellationToken cancellationToken)
        {
            return await base.GetAsync(id, cancellationToken);
        }

        public async Task<List<Issue>?> GetIssueByTitle(string title, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(x => x.Title != null && x.Title.Contains(title))
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Issue>?> GetIssuesAscendedByPriority(CancellationToken cancellationToken)
        {
           return await _dbSet
                .OrderBy(issue => issue.Priority)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Issue>?> GetIssuesByPriority(PriorityTypes priority, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(x => x.PriorityID == (int)priority)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Issue>?> GetIssuesByStatus(StatusTypes status, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(x => x.StatusId == (int)status)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Issue>?> GetIssuesByUserId(int userId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Issue>?> GetUserIssues(int userId, CancellationToken cancellationToken)
        {
            return await _dbSet
                .Where(x => x.UserId == userId)
                .ToListAsync(cancellationToken);
        }

        public async Task UpdateIssue(Issue issue, CancellationToken cancellationToken)
        {
            await base.UpdateAsync(cancellationToken, issue);   
        }
    }
}
