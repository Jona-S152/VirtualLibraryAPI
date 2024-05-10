using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibrary.Models;
using VirtualLibrary.Models.DTOs;

namespace VirtualLibrary.BLL.Services
{
    public interface IAuthService
    {
        public Usuario Login(UserLoginDTO userLogin);
        public string GenerateToken(Usuario usuario, string rol);
        public string GetRole(Usuario usuario);
        //public Task<bool> Logout();

    }
}
