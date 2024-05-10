using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualLibrary.BLL.Services;
using VirtualLibrary.Models;
using VirtualLibrary.Models.DTOs;

namespace VirtualLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login([FromBody] UserLoginDTO userLogin)
        {
            Usuario usuario = _authService.Login(userLogin);

            if (usuario != null)
            {
                string rol = _authService.GetRole(usuario);

                string token = _authService.GenerateToken(usuario, rol);

                return Ok(new
                {
                    Token = token
                });
            }

            return NotFound("Usuario no encontrado");
        }
    }
}
