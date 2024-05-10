using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VirtualLibrary.Models;

public partial class Libro
{
    public int LibroId { get; set; }

    public string? Titulo { get; set; }

    public int? AutorIdAutor { get; set; }

    public int? GeneroIdGenero { get; set; }

    public int? EditorialIdEditorial { get; set; }

    public int? AñoPublicacion { get; set; }

    public string? Descripcion { get; set; }

    [JsonIgnore]
    public virtual Autor? AutorIdAutorNavigation { get; set; }

    [JsonIgnore]
    public virtual Editorial? EditorialIdEditorialNavigation { get; set; }

    [JsonIgnore]
    public virtual Genero? GeneroIdGeneroNavigation { get; set; }
}
