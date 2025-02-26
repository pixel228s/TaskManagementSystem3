using FluentAssertions;
using IssueManagement.Application.Exceptions;
using IssueManagement.Application.Issues;
using IssueManagement.Application.Issues.interfaces;
using IssueManagement.Application.Issues.requests;
using IssueManagement.Application.Users.Interfaces;
using IssueManagement.Domain.Enums;
using IssueManagement.Domain.Models;
using Moq;
using Xunit;

namespace IssueManagement.Tests
{
    public class IssueServiceTests
    {
        private readonly IssueService _sut;
        private readonly Mock<IIssueRepository> _issueMock = new();
        private readonly Mock<IUserRepository> _userMock = new();

        public IssueServiceTests()
        {
            _sut = new IssueService(_issueMock.Object, _userMock.Object);
        }

        [Theory]
        [InlineData(StatusTypes.ToDo)]
        [InlineData(StatusTypes.InProgress)]
        [InlineData(StatusTypes.Completed)]
        public async Task CheckIssuesByStatus(StatusTypes types)
        {
            var dbResult = _issueMock
                .Setup(x => x.GetIssuesByStatus(types, CancellationToken.None))
                .ReturnsAsync(new List<Issue>());

            var result = await _sut.GetIssuesByStatus(types, CancellationToken.None);

            Assert.NotNull(result);
            result.ForEach(x => x?.StatusId.Should().Be((int)types));
        }

        [Theory]
        [InlineData(PriorityTypes.Low)]
        [InlineData(PriorityTypes.Medium)]
        [InlineData(PriorityTypes.High)]
        public async Task CheckIssuesByPriority(PriorityTypes types)
        {
            var dbResult = _issueMock
                .Setup(x => x.GetIssuesByPriority(types, CancellationToken.None))
                .ReturnsAsync(new List<Issue>());

            var result = await _sut.GetIssuesByPriority(types, CancellationToken.None);

            Assert.NotNull(result);
            result.ForEach(x => x?.StatusId.Should().Be((int)types));
        }

        [Fact]
        public async Task GetnonExistentIssue()
        {
            var updateResult = async () => await _sut.FindIssueById(0, CancellationToken.None);

            await updateResult.Should().ThrowAsync<IssueNonExistentException>();
        }

        [Fact]
        public async Task SearchIssuesbyTitle()
        {
            string wordToCheck = "task"; 

            var mockedRepo = _issueMock
                .Setup(x => x.GetIssueByTitle(wordToCheck, CancellationToken.None))
                .ReturnsAsync(new List<Issue>());

            var result = await _sut.GetIssuesByTitle(wordToCheck, CancellationToken.None);

            Assert.NotNull(result);
            result.ForEach(x => x.Title.Should().Contain(wordToCheck));
        }

        [Fact]
        public async Task AttemptingToDeleteNonExistentIssue()
        {
             var result = async() => await _sut.DeleteIssue(0, CancellationToken.None);

             await result.Should().ThrowAsync<IssueNonExistentException>();
        }

        [Fact]
        public async Task AttemptingToGetIssueWithNonExistentUserId()
        {
            var result = async() => await _sut.FindIssuesByUserId(-1, CancellationToken.None);

            await result.Should().ThrowAsync<UserNotFoundException>();
        }
    }
}
