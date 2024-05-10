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
    //[Authorize(Roles = "Admin, Superadmin")]
    public class UsuarioController : ControllerBase
    {
        private readonly IGenericRepository<Usuario> _repository;
        private readonly IRolUsuarioRepository _rolUsuarioRepo;
        private readonly IValidationsService _validations;

        public UsuarioController(IGenericRepository<Usuario> repo, IRolUsuarioRepository rolUsuarioRepo, IValidationsService validations)
        {
            _repository = repo;
            _rolUsuarioRepo = rolUsuarioRepo;
            _validations = validations;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var usuarios = _repository.GetAll();

            if (usuarios.Count <= 0)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "No se encontraron usuarios.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Listado de usuarios.",
                result = usuarios
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var usuario = _repository.GetById(id);

            if (usuario == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    message = "Usuario no encontrado.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Usuario.",
                result = usuario
            });
        }

        [HttpPost]
        public IActionResult Add([FromBody] UsuarioDTO usuario)
        {
            bool isRolExist = _validations.RolExists(usuario.IdRol);

            if (!isRolExist)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "Rol no encontrado.",
                    result = ""
                });
            }
            
            Usuario user = new Usuario();

            user.Nombre = usuario.Nombre;
            user.CorreoElectronico = usuario.CorreoElectronico;
            user.Contraseña = usuario.Contraseña;


            bool isAdd = _repository.Insert(user);

            if (!isAdd)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No se pudo agregar el usuario.",
                    result = ""
                });
            }


            RolUsuario rolUsuario = new RolUsuario();

            rolUsuario.RolIdRol = usuario.IdRol;
            rolUsuario.UsuarioIdUsuario = _rolUsuarioRepo.GetLastUser();

            bool isAddRolUsuario = _rolUsuarioRepo.Insert(rolUsuario);

            if (!isAddRolUsuario)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No se pudo relacionar el usuario con el rol.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Usuario agregado con éxito.",
                result = ""
            });
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromBody] UsuarioDTO usuario, int id)
        {
            Usuario user = new Usuario();

            user.Nombre = usuario.Nombre;
            user.CorreoElectronico = usuario.CorreoElectronico;
            user.Contraseña = usuario.Contraseña;


            bool isUpdate = _repository.Update(user, id);

            if (!isUpdate)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No se pudo agregar el usuario.",
                    result = ""
                });
            }

            var userFromRepo = _repository.GetById(id);

            RolUsuario rolUsuario = new RolUsuario();

            rolUsuario.RolIdRol = usuario.IdRol;

            rolUsuario.UsuarioIdUsuario = userFromRepo.IdUsuario;

            bool isAddRolUsuario = _rolUsuarioRepo.Insert(rolUsuario);

            if (!isAddRolUsuario)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No se pudo relacionar el usuario con el rol.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status201Created, new
            {
                message = "Usuario actualizado con éxito.",
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
                    message = "Usuario no encontrado.",
                    result = ""
                });
            }

            bool rolUsuarioIsDeleted = _rolUsuarioRepo.Delete(id);

            if (!rolUsuarioIsDeleted)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    message = "No se pudo eliminar los roles relacionados con el usuario.",
                    result = ""
                });
            }

            return StatusCode(StatusCodes.Status200OK, new
            {
                message = "Usuario eliminado con éxito.",
                result = ""
            });
        }
    }
}
