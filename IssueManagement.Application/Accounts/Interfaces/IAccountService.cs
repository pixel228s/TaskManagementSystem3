using IssueManagement.Application.Accounts.Requests;
using IssueManagement.Application.Users.Responses;

namespace IssueManagement.Application.Accounts.Interfaces
{
    public interface IAccountService
    {
        Task<UserResponseModel> RegisterAsync(RegisterRequestModel request);
        Task<UserResponseModel> AuthenticateAsync(LoginRequestModel request);
        Task ConfirmEmail(ConfirmEmailRequest request);
        Task<UserResponseModel> ChangePasswordAsync(ChangePasswordRequest request);
        Task ResetPasswordAsync(ResetPasswordRequest request);
        Task<UserResponseModel> NewPasswordAsync(NewPasswordRequest request);
        Task ResendCode(ResendCodeRequest request);
    }
}
