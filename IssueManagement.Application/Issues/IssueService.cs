using IssueManagement.Application.Exceptions;
using IssueManagement.Application.Issues.interfaces;
using IssueManagement.Application.Issues.requests;
using IssueManagement.Application.Issues.responses;
using IssueManagement.Application.Users.Interfaces;
using IssueManagement.Domain.Enums;
using IssueManagement.Domain.Models;
using Mapster;

namespace IssueManagement.Application.Issues
{
    public class IssueService : IIssueService
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IUserRepository _userRepository;

        public IssueService(IIssueRepository issueRepository, IUserRepository userRepository)
        {
            _issueRepository = issueRepository;
            _userRepository = userRepository;
        }

        public async Task<IssueResponse> CreateIssue(CreateIssueRequest request, CancellationToken cancellationToken)
        {
            var issue = new Issue
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                PriorityID = request.PriorityId,
                DueDate = request.DueDate ?? DateTime.MaxValue,
            };

            await _issueRepository.CreateIssue(issue, cancellationToken);
            return issue.Adapt<IssueResponse>();
        }

        public async Task DeleteIssue(int id, CancellationToken cancellationToken)
        {
            var issue = await _issueRepository.GetIssueById(id, cancellationToken);
            if(issue == null)
            {
                throw new IssueNonExistentException();
            }

            await _issueRepository.DeleteIssueById(issue, cancellationToken);
        }

        public async Task<IssueResponse?> FindIssueById(int id, CancellationToken cancellationToken)
        {
            var issue = await _issueRepository.GetIssueById(id, cancellationToken);
            if(issue == null)
            {
                throw new IssueNonExistentException();
            }
            return issue.Adapt<IssueResponse>();
        }

        public async Task<List<IssueResponse>?> FindIssuesByUserId(int userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            if(user == null)
            {
                throw new UserNotFoundException();
            }

            var issues = await _issueRepository.GetUserIssues(userId, cancellationToken);
            return issues.Adapt<List<IssueResponse>?>(); 
        }

        public async Task<IssueResponse> GetIssue(int id, CancellationToken cancellationToken)
        {
            var issue = await _issueRepository.GetIssueById(id, cancellationToken);
            if (issue == null)
            {
                throw new IssueNonExistentException();
            }
            return issue.Adapt<IssueResponse>();
        }

        public async Task<List<IssueResponse?>?> GetIssuesAscendingOrdByPriority(CancellationToken cancellationToken)
        {
            var issues = await _issueRepository.GetIssuesAscendedByPriority(cancellationToken);
            return issues.Adapt<List<IssueResponse?>?>();
        }

        public async Task<List<IssueResponse?>?> GetIssuesByPriority(PriorityTypes priority, CancellationToken cancellationToken)
        {
            var issues = await _issueRepository.GetIssuesByPriority(priority, cancellationToken);
            return issues.Adapt<List<IssueResponse?>?>();
        }

        public async Task<List<IssueResponse?>?> GetIssuesByStatus(StatusTypes status, CancellationToken cancellationToken)
        {
            var issues = await _issueRepository.GetIssuesByStatus(status, cancellationToken);
            return issues.Adapt<List<IssueResponse?>?>();
        }

        public async Task<List<IssueResponse?>?> GetIssuesByTitle(string titleSubstring, CancellationToken cancellationToken)
        {
            var issues = await _issueRepository.GetIssueByTitle(titleSubstring, cancellationToken);
            return issues.Adapt<List<IssueResponse?>?>();
        }

        public async Task<IssueResponse> UpdateIssueById(int id, IssuePutRequest request, CancellationToken cancellationToken)
        {
            var issue = await _issueRepository.GetIssueById(id, cancellationToken);

            if (issue == null)
            {
                throw new IssueNonExistentException();
            }

            if (request.Status != null)
            {
                if (issue.StatusId ==(int)StatusTypes.ToDo && request.Status == StatusTypes.InProgress)
                {
                    issue.StatusId = (int)request.Status;
                }
                else if (issue.StatusId == (int)StatusTypes.InProgress && request.Status == StatusTypes.Completed)
                {
                    issue.StatusId = (int)request.Status;
                    issue.CompletionDate = DateTime.UtcNow;
                }
                else
                {
                    throw new StatusException();
                }
            }

            if(request.Priority != null)
            {
                issue.PriorityID = (int)request.Priority;
            }


            issue.Title = request.Title ?? issue.Title;
            issue.Description = request.Description ?? issue.Description;
            issue.DueDate = request.DueDate;           
            issue.UserId = request.UserId ?? issue.UserId;

            await _issueRepository.UpdateIssue(issue, cancellationToken);
            
            return issue.Adapt<IssueResponse>();
        }
    }
}
