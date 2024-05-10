using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VirtualLibrary.Models;

public partial class Genero
{
    public int GeneroId { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
