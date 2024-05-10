using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace VirtualLibrary.Models;

public partial class Editorial
{
    public int EditorialId { get; set; }

    public string? Nombre { get; set; }

    public string? Pais { get; set; }

    public int? AñoFundacion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
