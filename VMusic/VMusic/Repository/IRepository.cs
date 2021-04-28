using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMusic.Repository
{
    interface IRepository<T>: IDisposable
        where T:class
    {
        IEnumerable<T> GetAllObject();
        T GetById(int id);
        void Create(T obj);
        void Update(T oldObj, T newObj);
        void Delete(int id);
        void Save();
    }
}
