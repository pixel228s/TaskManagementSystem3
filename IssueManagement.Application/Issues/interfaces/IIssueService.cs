using IssueManagement.Application.Issues.requests;
using IssueManagement.Application.Issues.responses;
using IssueManagement.Domain.Enums;

namespace IssueManagement.Application.Issues.interfaces
{
    public interface IIssueService
    {
        Task<IssueResponse> CreateIssue(CreateIssueRequest request, CancellationToken cancellationToken);
        Task<IssueResponse> UpdateIssueById(int id, IssuePutRequest request, CancellationToken cancellationToken);
        Task DeleteIssue(int id, CancellationToken cancellationToken);
        Task<IssueResponse> GetIssue(int id, CancellationToken cancellationToken);
        Task<List<IssueResponse?>?> GetIssuesByTitle(string titleSubstring, CancellationToken cancellationToken);
        Task<List<IssueResponse?>?> GetIssuesByPriority(PriorityTypes priority, CancellationToken cancellationToken);
        Task<List<IssueResponse?>?> GetIssuesByStatus(StatusTypes status, CancellationToken cancellationToken);
        Task<List<IssueResponse?>?> GetIssuesAscendingOrdByPriority(CancellationToken cancellationToken);
        Task<IssueResponse?> FindIssueById(int id, CancellationToken cancellationToken);  
        Task<List<IssueResponse>?> FindIssuesByUserId(int userId, CancellationToken cancellationToken);
    }
}
