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
    public class EditorialRepository: IGenericRepository<Editorial>
    {
        private readonly VirtualLibraryDbContext _context;

        public EditorialRepository(VirtualLibraryDbContext context)
        {
            _context = context;
        }

        public List<Editorial> GetAll()
        {
            var editorials = _context.Editorials.ToList();

            return editorials;
        }

        public Editorial GetById(int id)
        {
            return _context.Editorials.FirstOrDefault(a => a.EditorialId == id);
        }

        public bool Insert(Editorial model)
        {
            try
            {
                _context.Editorials.Add(model);

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool Update(Editorial model, int id)
        {
            try
            {
                var editorial = _context.Editorials.FirstOrDefault(a => a.EditorialId == id);

                if (editorial != null)
                {
                    editorial.Nombre = model.Nombre;
                    editorial.Pais = model.Pais;
                    editorial.AñoFundacion = model.AñoFundacion;

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

        public bool Delete(int id)
        {
            try
            {
                var editorial = _context.Editorials.FirstOrDefault(a => a.EditorialId == id);

                if (editorial != null)
                {
                    _context.Editorials.Remove(editorial);

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
