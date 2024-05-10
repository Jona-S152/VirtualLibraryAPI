using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualLibrary.DAL.Repositories;
using VirtualLibrary.Models;
using VirtualLibrary.Models.DTOs;

namespace VirtualLibrary.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class GeneroController : ControllerBase
    {
        private readonly IGenericRepository<Genero> _repository;

        public GeneroController(IGenericRepository<Genero> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var generos = _repository.GetAll();

            if (generos.Count <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "No se encontraron generos.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Listado de generos.",
                result = generos
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var genero = _repository.GetById(id);

            if (genero == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Genero no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Genero.",
                result = genero
            });
        }

        [HttpPost]
        public IActionResult Add([FromBody] GeneroDTO genero)
        {
            Genero genre = new Genero();

            genre.Nombre = genero.Nombre;
            genre.Descripcion = genero.Descripcion;

            bool isAdd = _repository.Insert(genre);

            if (!isAdd)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No se pudo agregar el genero.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Genero agregado con éxito.",
                result = ""
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] GeneroDTO genero, int id)
        {
            Genero gender = new Genero();

            gender.Nombre = genero.Nombre;
            gender.Descripcion = genero.Descripcion;

            bool isUpdate = _repository.Update(gender, id);

            if (!isUpdate)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Genero no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Genero actualizado con éxito.",
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
                    message = "Genero no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Genero eliminado con éxito.",
                result = ""
            });
        }
    }
}
