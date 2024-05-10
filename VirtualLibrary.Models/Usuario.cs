using System;
using System.Collections.Generic;

namespace VirtualLibrary.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Contraseña { get; set; }
}
