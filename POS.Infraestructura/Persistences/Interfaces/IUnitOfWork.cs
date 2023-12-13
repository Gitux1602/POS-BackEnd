using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructura.Persistences.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        //Declaración de nuestras interfaces a nivel de repositorio
        ICategoryRepository Category { get; }

        void SaveChanges();
        void SaveChangesAsync();
    }
}
