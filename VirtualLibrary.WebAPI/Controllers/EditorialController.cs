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
    [Authorize]
    public class EditorialController : ControllerBase
    {
        private readonly IGenericRepository<Editorial> _repository;

        public EditorialController(IGenericRepository<Editorial> repo)
        {
            _repository = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var editoriales = _repository.GetAll();

            if (editoriales.Count <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "No se encontraron editoriales.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Listado de editoriales.",
                result = editoriales
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var editorial = _repository.GetById(id);

            if (editorial == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Editorial no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Editorial.",
                result = editorial
            });
        }

        [HttpPost]
        public IActionResult Add([FromBody] EditorialDTO editorial)
        {
            Editorial publisher = new Editorial();

            publisher.Nombre = editorial.Nombre;
            publisher.Pais = editorial.Pais;
            publisher.AñoFundacion = editorial.AñoFundacion;

            bool isAdd = _repository.Insert(publisher);

            if (!isAdd)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No se pudo agregar la editorial.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Editorial agregada con éxito.",
                result = ""
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] EditorialDTO editorial, int id)
        {
            Editorial publisher = new Editorial();

            publisher.Nombre = editorial.Nombre;
            publisher.Pais = editorial.Pais;
            publisher.AñoFundacion = editorial.AñoFundacion;

            bool isUpdate = _repository.Update(publisher, id);

            if (!isUpdate)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Editorial no encontrada.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Editorial actualizada con éxito.",
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
                    message = "Editorial no encontrada.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Editorial eliminada con éxito.",
                result = ""
            });
        }
    }
}
