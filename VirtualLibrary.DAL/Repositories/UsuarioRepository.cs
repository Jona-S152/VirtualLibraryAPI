using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibrary.DAL.DataContext;
using VirtualLibrary.Models;

namespace VirtualLibrary.DAL.Repositories
{
    public class UsuarioRepository: IGenericRepository<Usuario>
    {
        private readonly VirtualLibraryDbContext _context;

        public UsuarioRepository(VirtualLibraryDbContext context)
        {
            _context = context;
        }

        public List<Usuario> GetAll()
        {
            var usuarios = _context.Usuarios.ToList();

            return usuarios;
        }

        public Usuario GetById(int id)
        {
            return _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);
        }

        public bool Insert(Usuario model)
        {
            try
            {
                _context.Usuarios.Add(model);

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public bool Update(Usuario model, int id)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

                if (usuario != null)
                {
                    usuario.Nombre = model.Nombre;
                    usuario.CorreoElectronico = model.CorreoElectronico;
                    usuario.Contraseña = model.Contraseña;

                    _context.SaveChanges();
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.IdUsuario == id);

                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);

                    _context.SaveChanges();
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
