using ChauffeurApp.Application.DTOs;
using ChauffeurApp.Application.Services.IServices;
using ChauffeurApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChauffeurApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<Result<LoginResponseDTO>> Login(LoginDTO loginDTO)
        {
            var identityUser = await _userManager.Users.Where(x => x.PhoneNumber == loginDTO.PhoneNumber).FirstOrDefaultAsync();

            if (identityUser == null)
            {
                return Result<LoginResponseDTO>.Failure("A user with this phone number does not exist");
            };

            var response = await _userManager.CheckPasswordAsync(identityUser, loginDTO.Password);

            if (response)
            {
                var roles = await _userManager.GetRolesAsync(identityUser);
                var token = await _tokenService.CreateToken(identityUser, roles);
                var newRefreshToken = await _tokenService.CreateNewRefreshToken(identityUser.Id.ToString(), Guid.NewGuid().ToString());

                LoginResponseDTO responseDto = new LoginResponseDTO()
                {
                    FullName = identityUser.FullName,
                    Email = identityUser.Email,
                    Token = token.Value,
                    RefreshToken = newRefreshToken.Value
                };
                return Result<LoginResponseDTO>.Success(responseDto);
            }
            else
            {
                return Result<LoginResponseDTO>.Failure("Wrong password for this user");
            }
        }

        public async Task<Result<bool>> RegisterUser(CreateUserDTO createUserDTO)
        {
            var email = await _userManager.FindByEmailAsync(createUserDTO.Email);
            if (email != null)
            {
                return Result<bool>.Failure("This email already exists!");
            }

            var phoneNumber = await _userManager.Users.Where(x => x.PhoneNumber == createUserDTO.PhoneNumber).FirstOrDefaultAsync();
            if (phoneNumber != null)
            {
                return Result<bool>.Failure("A user with this phone number already exists");
            };

            var identityUser = new ApplicationUser()
            {
                FullName = createUserDTO.FullName,
                PhoneNumber = createUserDTO.PhoneNumber,
                Gender = createUserDTO.Gender,
                UserName = createUserDTO.Email,
                Email = createUserDTO.Email,
            };

            var result = await _userManager.CreateAsync(identityUser, createUserDTO.Password);
            if (!result.Succeeded)
            {
                return Result<bool>.Failure("Error in creating the user");
            }
            
            var role = await _userManager.AddToRoleAsync(identityUser, createUserDTO.Role.ToString());
            if (!role.Succeeded)
            {
                return Result<bool>.Failure("Error in assigning role to user");
            }
            return Result<bool>.Success(true);
        }
    }
}
