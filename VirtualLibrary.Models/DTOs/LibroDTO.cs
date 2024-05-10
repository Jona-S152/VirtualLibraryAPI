using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibrary.Models.DTOs
{
    public class LibroDTO
    {
        public string? Titulo { get; set; }

        public int AutorIdAutor { get; set; }

        public int GeneroIdGenero { get; set; }

        public int EditorialIdEditorial { get; set; }

        public int? AñoPublicacion { get; set; }

        public string? Descripcion { get; set; }
    }
}
