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
    public class LibroRepository : IGenericRepository<Libro>
    {

        private readonly VirtualLibraryDbContext _context;

        public LibroRepository(VirtualLibraryDbContext context)
        {
            _context = context;
        }

        public List<Libro> GetAll()
        {
            var books = _context.Libros.ToList();

            return books;
        }

        public Libro GetById(int id)
        {
            return _context.Libros.FirstOrDefault(l => l.LibroId == id);
        }
        
        public bool Insert(Libro model)
        {
            try
            {
                _context.Libros.Add(model);

                _context.SaveChanges();

            }catch (Exception ex)
            {
                return false;
            }
            
            return true;
        }

        public bool Update(Libro model, int id)
        {
            try
            {
                var book = _context.Libros.FirstOrDefault(l => l.LibroId == id);

                if (book != null)
                {
                    book.Titulo = model.Titulo;
                    book.AutorIdAutor = model.AutorIdAutor;
                    book.GeneroIdGenero = model.GeneroIdGenero;
                    book.EditorialIdEditorial = model.EditorialIdEditorial;
                    book.AñoPublicacion = model.AñoPublicacion;
                    book.Descripcion = model.Descripcion;

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
                var book = _context.Libros.FirstOrDefault(l => l.LibroId == id);

                if (book != null)
                {
                    _context.Libros.Remove(book);

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
