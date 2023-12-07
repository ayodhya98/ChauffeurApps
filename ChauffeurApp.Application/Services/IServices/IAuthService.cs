using ChauffeurApp.Application.DTOs;
using ChauffeurApp.Core.Entities;

namespace ChauffeurApp.Application.Services.IServices
{
    public interface IAuthService
    {
        Task<Result<bool>> RegisterUser(CreateUserDTO createUserDTO);
        Task<Result<LoginResponseDTO>> Login(LoginDTO loginDTO);
    }
}
