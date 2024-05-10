using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibrary.BLL.Services
{
    public interface IValidationsService
    {
        public bool RolExists(int? id);
        public bool AutorExists(int? id);
        public bool GeneroExists(int? id);
        public bool EditorialExists(int? id);
        //public bool LibroExists(int? id);
    }
}
