using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Models;

namespace ProjectManager.Repositories.Interfaces
{
    interface IFirmRepo
    {
        IEnumerable<Firm> GetAll();
        Firm GetById(int id);
        bool Create(Firm firm);
        bool Update(int id, Firm firm);
        void Delete(int id);
    }
}
