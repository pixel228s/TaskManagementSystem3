using IssueManagement.Domain.Enums;
using IssueManagement.Domain.Models;

namespace IssueManagement.Application.Issues.interfaces
{
    public interface IIssueRepository
    {
        Task<List<Issue>?> GetIssuesByUserId(int userId, CancellationToken cancellationToken);
        Task<List<Issue>?> GetIssuesByPriority(PriorityTypes priority, CancellationToken cancellationToken);
        Task<List<Issue>?> GetIssuesByStatus(StatusTypes status, CancellationToken cancellationToken);
        Task<List<Issue>?> GetIssuesAscendedByPriority(CancellationToken cancellationToken);
        Task<List<Issue>?> GetIssueByTitle(string title, CancellationToken cancellationToken);
        Task<Issue?> GetIssueById(int id, CancellationToken cancellationToken);
        Task DeleteIssueById(Issue issue, CancellationToken cancellationToken);  
        Task CreateIssue(Issue issue, CancellationToken cancellationToken);
        Task UpdateIssue(Issue issue, CancellationToken cancellationToken);
        Task<List<Issue>?> GetUserIssues(int userId, CancellationToken cancellationToken);  
    }
}
