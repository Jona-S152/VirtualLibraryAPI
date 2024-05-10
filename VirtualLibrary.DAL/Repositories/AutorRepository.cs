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
    public class AutorRepository : IGenericRepository<Autor>
    {
        private readonly VirtualLibraryDbContext _context;

        public AutorRepository(VirtualLibraryDbContext context)
        {
            _context = context;
        }

        public List<Autor> GetAll()
        {
            var autors = _context.Autors.ToList();

            return autors;
        }

        public Autor GetById(int id)
        {
            return _context.Autors.FirstOrDefault(a => a.AutorId == id);
        }

        public bool Insert(Autor model)
        {
            try
            {
                _context.Autors.Add(model);

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool Update(Autor model, int id)
        {
            try
            {
                var autor = _context.Autors.FirstOrDefault(a => a.AutorId == id);

                if (autor != null)
                {
                    autor.Nombre = model.Nombre;
                    autor.Nacionalidad = model.Nacionalidad;
                    autor.AñoNacimiento = model.AñoNacimiento;

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
                var autor = _context.Autors.FirstOrDefault(a => a.AutorId == id);

                if (autor != null)
                {
                    _context.Autors.Remove(autor);

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
