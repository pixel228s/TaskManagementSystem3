using IssueManagement.Application.Issues.interfaces;
using IssueManagement.Application.Issues.requests;
using IssueManagement.Application.Issues.responses;
using IssueManagement.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IssueManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IssueController : ControllerBase
    {
        private readonly IIssueService _issueService;

        public IssueController(IIssueService issueService)
        {
            _issueService = issueService;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<IssueResponse>> GetIssueById(int id, CancellationToken cancellationToken)
        {
            var issue = await _issueService.FindIssueById(id, cancellationToken);
            return Ok(issue);
        }

        [HttpGet("prioritized-issues")]
        public async Task<ActionResult<IssueResponse>> GetIssuesOrderedByPriority(CancellationToken cancellationToken)
        {
            var issues = await _issueService.GetIssuesAscendingOrdByPriority(cancellationToken);
            return Ok(issues);
        }

        [HttpPost("create-issue")]
        public async Task<ActionResult<IssueResponse>> CreateIssue([FromBody]CreateIssueRequest request,CancellationToken cancellationToken)
        {
            var issue = await _issueService.CreateIssue(request, cancellationToken);
            return Ok(issue);   
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IssueResponse>> GetIssuesByStatus(StatusTypes status, CancellationToken cancellationToken)
        {
            var issue = await _issueService.GetIssuesByStatus(status, cancellationToken);
            return Ok(issue);
        }

        [HttpGet("priority/{priority}")]
        public async Task<ActionResult<IssueResponse>> GetIssuesByPriority(PriorityTypes priority, CancellationToken cancellationToken)
        {
            var issue = await _issueService.GetIssuesByPriority(priority, cancellationToken);   
            return Ok(issue);
        }

        [HttpPut("update-issue")]
        public async Task<ActionResult<IssueResponse>> UpdateIssueById(int id, IssuePutRequest request, CancellationToken cancellationToken)
        {
            var issue =  await _issueService.UpdateIssueById(id, request, cancellationToken);
            return Ok(issue);
        }

        [HttpGet("include/{title}")]
        public async Task<ActionResult<IssueResponse>> FindIssuesByTitle(string title, CancellationToken cancellationToken)
        {
            var issues = await _issueService.GetIssuesByTitle(title, cancellationToken);
            return Ok(issues);
        }

        [HttpGet("user-issues/{userId}")]
        public async Task<ActionResult<IssueResponse>> FindIssuesByUserId(int userId, CancellationToken cancellationToken)
        {
            var issues = await _issueService.FindIssuesByUserId(userId, cancellationToken);
            return Ok(issues);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteIssueById(int id, CancellationToken cancellationToken)
        {
            await _issueService.DeleteIssue(id, cancellationToken);
            return Ok();
        }
    }
}
