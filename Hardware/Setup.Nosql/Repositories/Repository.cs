using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Setup.Nosql.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task Update(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException("Entity must have an Id property");

            var idValue = (Guid)idProperty.GetValue(entity);
            var filter = Builders<T>.Filter.Eq("Id", idValue);

            await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task Delete(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException("Entity must have an Id property");

            var idValue = (Guid)idProperty.GetValue(entity);
            var filter = Builders<T>.Filter.Eq("Id", idValue);

            await _collection.DeleteOneAsync(filter);
        }
    }
}
