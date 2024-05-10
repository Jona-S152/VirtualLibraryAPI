using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VirtualLibrary.Models;

public partial class Autor
{
    public int AutorId { get; set; }

    public string? Nombre { get; set; }

    public string? Nacionalidad { get; set; }

    public int? AñoNacimiento { get; set; }

    [JsonIgnore]
    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
