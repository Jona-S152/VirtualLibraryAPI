using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualLibrary.BLL.Services;
using VirtualLibrary.DAL.Repositories;
using VirtualLibrary.Models;
using VirtualLibrary.Models.DTOs;

namespace VirtualLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutorController : ControllerBase
    {
        private readonly IGenericRepository<Autor> _repository;

        public AutorController(IGenericRepository<Autor> repo)
        {
            _repository = repo;
        }
        
        [HttpGet]
        public IActionResult GetAll()
        {
            var autores = _repository.GetAll();

            if (autores.Count <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "No se encontraron autores.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Listado de autores.",
                result = autores
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var autor = _repository.GetById(id);

            if (autor == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Autor no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Autor.",
                result = autor
            });
        }

        [HttpPost]
        public IActionResult Add([FromBody] AutorDTO autor)
        {
            Autor author = new Autor();

            author.Nombre = autor.Nombre;
            author.Nacionalidad = autor.Nacionalidad;
            author.AñoNacimiento = autor.AñoNacimiento;

            bool isAdd = _repository.Insert(author);

            if (!isAdd)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No se pudo agregar el autor.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Autor agregado con éxito.",
                result = ""
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] AutorDTO autor, int id)
        {
            Autor author = new Autor();

            author.Nombre = autor.Nombre;
            author.Nacionalidad = autor.Nacionalidad;
            author.AñoNacimiento = autor.AñoNacimiento;

            bool isUpdate = _repository.Update(author, id);

            if (!isUpdate)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Autor no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Autor actualizado con éxito.",
                result = ""
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
                    message = "Autor no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Autor eliminado con éxito.",
                result = ""
            });
        }
    }
}
