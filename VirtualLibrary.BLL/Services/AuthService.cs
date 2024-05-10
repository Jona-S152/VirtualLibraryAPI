using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VirtualLibrary.DAL.DataContext;
using VirtualLibrary.Models;
using VirtualLibrary.Models.DTOs;

namespace VirtualLibrary.BLL.Services
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly VirtualLibraryDbContext _dbContext;

        public AuthService(IConfiguration config, VirtualLibraryDbContext context)
        {
            _configuration = config;
            _dbContext = context;
        }

        public string GenerateToken(Usuario usuario, string rol)
        {
            // Credenciales
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Role, rol)
            };

            // Token
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }

        public string GetRole(Usuario usuario)
        {
            // Consulta para obtener el rol del usuario
            var query = from u in _dbContext.Usuarios
                        join ru in _dbContext.RolUsuarios on u.IdUsuario equals ru.UsuarioIdUsuario
                        join r in _dbContext.Rols on ru.RolIdRol equals r.IdRol
                        where u.Nombre == usuario.Nombre && u.Contraseña == usuario.Contraseña
                        select new
                        {
                            u.IdUsuario,
                            Rol = r.Nombre
                        };

            var user = query.FirstOrDefault();

            return user.Rol;
        }

        public Usuario Login(UserLoginDTO userLogin)
        {
            var usuario = _dbContext.Usuarios.FirstOrDefault(
                u => u.CorreoElectronico.ToLower() == userLogin.CorreoElectronico.ToLower() &&
                u.Contraseña.ToLower() == userLogin.Contraseña.ToLower()
                );

            return usuario;
        }
    }
}
