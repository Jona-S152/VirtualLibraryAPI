using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibrary.DAL.DataContext;
using VirtualLibrary.Models;

namespace VirtualLibrary.DAL.Repositories
{
    public class RolUsuarioRepository : IRolUsuarioRepository
    {
        private readonly VirtualLibraryDbContext _dbContext;

        public RolUsuarioRepository(VirtualLibraryDbContext context)
        {
            _dbContext = context;
        }

        public bool Delete(int id)
        {
            var rolesUsuarios = _dbContext.RolUsuarios.Where(ru => ru.UsuarioIdUsuario == id).ToList();

            if (rolesUsuarios.Count <= 0)
            {
                return false;
            }

            foreach (var rolUsuario in rolesUsuarios)
            {
                _dbContext.RolUsuarios.Remove(rolUsuario);
            }

            _dbContext.SaveChanges();

            return true;
        }

        public int GetLastUser()
        {
            var user = _dbContext.Usuarios.OrderByDescending(u => u.IdUsuario).FirstOrDefault();

            if (user == null)
            {
                return 0;
            }

            return user.IdUsuario;
        }

        public bool Insert(RolUsuario rolUsuario)
        {
            try
            {
                _dbContext.Add(rolUsuario);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
    }
}
