using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibrary.DAL.DataContext;

namespace VirtualLibrary.BLL.Services
{
    public class ValidationsService : IValidationsService
    {
        private readonly VirtualLibraryDbContext _dbContext;

        public ValidationsService(VirtualLibraryDbContext context)
        {
            _dbContext = context;
        }

        public bool AutorExists(int? id)
        {
            var autor = _dbContext.Autors.FirstOrDefault(a => a.AutorId == id);

            if (autor != null)
            {
                return true;
            }

            return false;
        }

        public bool EditorialExists(int? id)
        {
            var editorial = _dbContext.Editorials.FirstOrDefault(e => e.EditorialId == id);

            if (editorial != null)
            {
                return true;
            }

            return false;
        }

        public bool GeneroExists(int? id)
        {
            var genero = _dbContext.Generos.FirstOrDefault(g => g.GeneroId == id);

            if(genero != null)
            {
                return true;
            }

            return false;
        }

        public bool RolExists(int? id)
        {
            var rol = _dbContext.Rols.FirstOrDefault(r => r.IdRol == id && r.Nombre != null);

            if (rol != null)
            {
                return true;
            }

            return false;
        }

        //public bool LibroExists(int? id)
        //{
        //    var libro = _dbContext.Libros.FirstOrDefault(l => l.LibroId == id);

        //    if (libro != null)
        //    {
        //        return true;
        //    }

        //    return false;
        //}
    }
}
