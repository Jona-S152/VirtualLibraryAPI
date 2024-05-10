using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using VirtualLibrary.BLL.Services;
using VirtualLibrary.DAL.Repositories;
using VirtualLibrary.Models;
using VirtualLibrary.Models.DTOs;

namespace VirtualLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LibroController : ControllerBase
    {
        private readonly IGenericRepository<Libro> _repository;
        private readonly IValidationsService _validationsService;

        public LibroController(IGenericRepository<Libro> repo, IValidationsService validations)
        {
            _repository = repo;
            _validationsService = validations;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var libros = _repository.GetAll();

            if (libros.Count <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "No se encontraron libros.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Listado de libros.",
                result = libros
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var libro = _repository.GetById(id);

            if (libro == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Libro no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Libro.",
                result = libro
            });
        }

        [HttpPost]
        public IActionResult Add([FromBody] LibroDTO libro)
        {

            bool existAutor = _validationsService.AutorExists(libro.AutorIdAutor);
            bool existGenero = _validationsService.GeneroExists(libro.GeneroIdGenero);
            bool existEditorial = _validationsService.EditorialExists(libro.EditorialIdEditorial);

            if (!existAutor)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Autor no encontrado.",
                    result = ""
                });
            }

            if (!existGenero)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Genero no encontrado.",
                    result = ""
                });
            }

            if (!existEditorial)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Editorial no encontrado.",
                    result = ""
                });
            }

            Libro book = new Libro();

            book.Titulo = libro.Titulo;
            book.AutorIdAutor = libro.AutorIdAutor;
            book.GeneroIdGenero = libro.GeneroIdGenero;
            book.EditorialIdEditorial = libro.EditorialIdEditorial;
            book.AñoPublicacion = libro.AñoPublicacion;
            book.Descripcion = libro.Descripcion;

            bool isAdd = _repository.Insert(book);

            if (!isAdd)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No se pudo agregar el libro.",
                    result = ""
                });                
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Libro agregado con éxito.",
                result = ""
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] LibroDTO libro, int id)
        {
            bool existAutor = _validationsService.AutorExists(libro.AutorIdAutor);
            bool existGenero = _validationsService.GeneroExists(libro.GeneroIdGenero);
            bool existEditorial = _validationsService.EditorialExists(libro.EditorialIdEditorial);

            if (!existAutor)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Autor no encontrado.",
                    result = ""
                });
            }

            if (!existGenero)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Genero no encontrado.",
                    result = ""
                });
            }

            if (!existEditorial)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Editorial no encontrado.",
                    result = ""
                });
            }

            Libro book = new Libro();

            book.Titulo = libro.Titulo;
            book.AutorIdAutor = libro.AutorIdAutor;
            book.GeneroIdGenero = libro.GeneroIdGenero;
            book.EditorialIdEditorial = libro.EditorialIdEditorial;
            book.AñoPublicacion = libro.AñoPublicacion;
            book.Descripcion = libro.Descripcion;

            bool isUpdate = _repository.Update(book, id);

            if (!isUpdate)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Libro no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Libro Actualizado con éxito.",
                resutl = ""
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool isDeleted = _repository.Delete(id);

            if (!isDeleted)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Libro no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Libro eliminado con éxito.",
                result = ""
            });
        }
    }
}
