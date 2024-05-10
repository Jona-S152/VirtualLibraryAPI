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
    public class GeneroRepository: IGenericRepository<Genero>
    {
        private readonly VirtualLibraryDbContext _context;

        public GeneroRepository(VirtualLibraryDbContext context)
        {
            _context = context;
        }

        public List<Genero> GetAll()
        {
            var generos = _context.Generos.ToList();

            return generos;
        }

        public Genero GetById(int id)
        {
            return _context.Generos.FirstOrDefault(l => l.GeneroId == id);
        }

        public bool Insert(Genero model)
        {
            try
            {
                _context.Generos.Add(model);

                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool Update(Genero model, int id)
        {
            try
            {
                var genero = _context.Generos.FirstOrDefault(l => l.GeneroId == id);

                if (genero != null)
                {
                    genero.Nombre = model.Nombre;
                    genero.Descripcion = model.Descripcion;

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
                var genero = _context.Generos.FirstOrDefault(l => l.GeneroId == id);

                if (genero != null)
                {
                    _context.Generos.Remove(genero);

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
