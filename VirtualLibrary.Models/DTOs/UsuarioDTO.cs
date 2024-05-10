using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibrary.Models.DTOs
{
    public class UsuarioDTO
    {
        public string? Nombre { get; set; }

        public string? CorreoElectronico { get; set; }

        public string? Contraseña { get; set; }

        public int? IdRol {  get; set; }
    }
}
