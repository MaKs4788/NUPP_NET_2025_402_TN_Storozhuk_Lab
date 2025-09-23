using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Common
{
    public interface ICrudService<T>
    {
        public void Create(T element);
        public T Read(Guid id);
        public IEnumerable<T> ReadAll();
        public void Update(T element);
        public void Remove(T element);

        public void Load();

        public void Save();

    }
}
