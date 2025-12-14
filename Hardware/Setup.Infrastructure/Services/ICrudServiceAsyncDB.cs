using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Infrastructure.Services
{
    public interface ICrudServiceAsyncDB<T> : IEnumerable<T>
    {
        public Task<bool> CreateAsyncDB(T element);
        public Task<T> ReadAsyncDB(Guid id);
        public Task<IEnumerable<T>> ReadAllAsyncDB();
        public Task<IEnumerable<T>> ReadAllAsyncDB(int page, int amount);
        public Task<bool> UpdateAsyncDB(T element);
        public Task<bool> RemoveAsyncDB(T element);
    }

}
