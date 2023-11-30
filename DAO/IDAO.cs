using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projeto_algoritmoGenetico.DAO
{
    internal interface IDAO<T>
    {
        T GetByID(int id);

        List<T> GetAll();

    }
}
