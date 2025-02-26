using IssueManagement.Application.Exceptions;
using IssueManagement.Application.Users.Interfaces;
using IssueManagement.Application.Users.Requests;
using IssueManagement.Application.Users.Responses;
using IssueManagement.Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
namespace IssueManagement.Application.Users
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        public UserService(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if(user == null)
            {
                throw new UserNotFoundException();
            }
            user.IsDeleted = true;

            await _userRepository.UpdateUserAsync(user, cancellationToken);
        }

        public async Task<UserResponseModel> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(email, cancellationToken);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user.Adapt<UserResponseModel>();
        }

        public async Task<UserResponseModel> GetByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(username, cancellationToken); 

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user.Adapt<UserResponseModel>();
        }

        public async Task<UserResponseModel> UpdateAsync(UserUpdateModel updateModel, int id, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetByIdAsync(id, cancellationToken);

            if (updateModel.UserName != null)
            {
                var existingUser = await _userRepository.GetByUsernameAsync(updateModel.UserName, cancellationToken);
                if (existingUser != null)
                {
                    throw new UsernameAlreadyExistsException();
                }

                user.UserName = updateModel.UserName;
            }

            if (updateModel.Email != null)
            {
                var existingUser = await _userRepository.GetByEmailAsync(updateModel.Email, cancellationToken);
                if (existingUser != null)
                {
                    throw new EmailAlreadyExistsException();
                }

                user.Email = updateModel.Email;
            }

            await _userRepository.UpdateUserAsync(user, cancellationToken);
            return user.Adapt<UserResponseModel>();
        }
    }
}
