using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualLibrary.DAL.Repositories
{
    public interface IGenericRepository<TEntityModel> where TEntityModel : class
    {
        List<TEntityModel> GetAll();
        TEntityModel GetById(int id);
        bool Insert(TEntityModel model);
        bool Update(TEntityModel model, int id);
        bool Delete(int id);

    }
}
