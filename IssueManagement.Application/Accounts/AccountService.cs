using IssueManagement.Application.Accounts.Interfaces;
using IssueManagement.Application.Accounts.Requests;
using IssueManagement.Application.EmailSender.interfaces;
using IssueManagement.Application.Exceptions;
using IssueManagement.Application.Users.Responses;
using IssueManagement.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace IssueManagement.Application.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AccountService> _logger;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        public async Task<UserResponseModel> AuthenticateAsync(LoginRequestModel request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            var isCorrect = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!isCorrect.Succeeded)
            {
                _logger.LogWarning("login failed. result: {@Result}", isCorrect);
                throw new AuthException();   
            }

            return user.Adapt<UserResponseModel>();
        }

        public async Task ResendCode(ResendCodeRequest request)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                throw new UserNotFoundException("Inavlid Email.");
            }

            if (_userManager.Options.SignIn.RequireConfirmedAccount)
            {
                _logger.LogInformation("Token generation started for user {Email}", user.Email);
                var code = _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                await _emailSender.SendEmailAsync(request.Email, subject: "Otp", code.Result);
            }
        }

        public async Task ConfirmEmail(ConfirmEmailRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (user.EmailConfirmed)
            {
                return;
            }

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user,TokenOptions.DefaultEmailProvider,
                request.Otp);

            if (!isValid)
            {
                _logger.LogWarning("Invalid OTP for user: {Email}", request.Email);
                throw new ConfirmationException();
            }

            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogError("Failed to update user after email confirmation: {Errors}",
                    string.Join(", ", result.Errors.Select(e => e.Description)));
                throw new Exception("Failed to confirm email");
            }
        }

        public async Task<UserResponseModel> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UserNotFoundException();
            }

            if (!await _userManager.CheckPasswordAsync(user, request.CurrentPassword))
            {
                throw new AuthException();
            }

            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return user.Adapt<UserResponseModel>(); 
            }
            throw new AuthException();
        }

        public async Task<UserResponseModel> RegisterAsync(RegisterRequestModel request)
        {
            if(await _userManager.FindByEmailAsync(request.Email) != null)
            {
                throw new EmailAlreadyExistsException();
            }   

            if(await _userManager.FindByNameAsync(request.UserName) != null)
            {
                throw new UsernameAlreadyExistsException();
            }

            var user = new User
            {
                Email = request.Email,
                UserName = request.UserName,
                CreatedOn = DateTime.UtcNow,
                IsDeleted = false,
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    _logger.LogInformation("Token generation started for user {Email}", user.Email);
                    var code = _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                    await _emailSender.SendEmailAsync(request.Email, subject: "Otp", code.Result);
                    return user.Adapt<UserResponseModel>();
                }
            }
            
            var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new RegistrationException(errorMessages);          
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new AuthException();
            }

            var otp = await _userManager.GenerateTwoFactorTokenAsync(user, "ResetPassword");
            await _emailSender.SendEmailAsync(request.Email, subject: "Otp", otp);
        }

        public async Task<UserResponseModel> NewPasswordAsync(NewPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new AuthException();
            }

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "ResetPassword", request.Otp);
            if (!isValid)
            {
                throw new AuthException();
            }

            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                if (!removePasswordResult.Succeeded)
                {
                    throw new ChangePasswordException();
                }
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, request.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                throw new ChangePasswordException();
            }

            return user.Adapt<UserResponseModel>(); 
        }
    }
}
