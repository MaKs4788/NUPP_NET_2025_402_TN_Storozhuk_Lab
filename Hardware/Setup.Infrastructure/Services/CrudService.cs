using Setup.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Setup.Infrastructure.Services
{
    public class CrudService<T> : ICrudServiceAsyncDB<T> where T : class
    {
        private readonly IRepository<T> _repository;

        public CrudService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateAsyncDB(T element)
        {
            if (element == null) return false;

            await _repository.AddAsync(element);
            return true;
        }

        public async Task<T?> ReadAsyncDB(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<T>> ReadAllAsyncDB()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<T>> ReadAllAsyncDB(int page, int amount)
        {
            if (page < 1 || amount < 1)
                return Enumerable.Empty<T>();

            var all = await _repository.GetAllAsync();
            return all.Skip((page - 1) * amount).Take(amount);
        }

        public async Task<bool> UpdateAsyncDB(T element)
        {
            if (element == null) return false;

            await _repository.Update(element);
            return true;
        }

        public async Task<bool> RemoveAsyncDB(T element)
        {
            if (element == null) return false;

            await _repository.Delete(element);
            return true;
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _repository.GetAllAsync().Result.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
