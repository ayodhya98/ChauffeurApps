using ChauffeurApp.Application.DTOs;
using ChauffeurApp.Application.Services.IServices;
using ChauffeurApp.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseAPIController
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        public AuthController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> CreateUser(CreateUserDTO user)
        {
            return HandleResult(await _authService.RegisterUser(user));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO user)
        {
            return HandleResult(await _authService.Login(user));
        }

        [HttpPost]
        [Route("RefreshAccessToken")]
        public async Task<IActionResult> GetNewTokenFromRefreshToken(TokenDTO tokenDTO)
        {
            return HandleResult(await _tokenService.RefreshAccessToken(tokenDTO));
        }
    }
}

