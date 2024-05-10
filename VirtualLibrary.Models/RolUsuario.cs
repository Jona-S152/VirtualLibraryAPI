using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VirtualLibrary.Models;

public partial class RolUsuario
{
    public int? RolIdRol { get; set; }

    public int? UsuarioIdUsuario { get; set; }

    [JsonIgnore]
    public virtual Rol? RolIdRolNavigation { get; set; }

    [JsonIgnore]
    public virtual Usuario? UsuarioIdUsuarioNavigation { get; set; }
}
