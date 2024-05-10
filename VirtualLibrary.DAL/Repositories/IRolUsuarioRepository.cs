using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualLibrary.Models;

namespace VirtualLibrary.DAL.Repositories
{
    public interface IRolUsuarioRepository
    {
        public bool Insert(RolUsuario rolUsuario);
        public bool Delete(int id);
        public int GetLastUser();
    }
}
