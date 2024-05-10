using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibrary.Models.DTOs
{
    public class EditorialDTO
    {
        public string? Nombre { get; set; }

        public string? Pais { get; set; }

        public int? AñoFundacion { get; set; }
    }
}
