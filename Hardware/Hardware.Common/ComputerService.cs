using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Setup.Common
{
    public class ComputerService: ICrudServiceAsync<Computer>
    {
        private readonly List<Computer> _computers = new();
        private readonly ReaderWriterLockSlim _lock = new(LockRecursionPolicy.NoRecursion);
        private static readonly string FilePath = "computers.json";
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

       
        public Task<bool> CreateAsync(Computer element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            _lock.EnterWriteLock();
            try
            {
                if (_computers.Any(c => c.Id == element.Id))
                    return Task.FromResult(false);

                _computers.Add(element);
                return Task.FromResult(true);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        
        public Task<Computer> ReadAsync(Guid id)
        {
            _lock.EnterReadLock();
            try
            {
                var found = _computers.FirstOrDefault(c => c.Id == id);
                return Task.FromResult(found);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

     
        public Task<IEnumerable<Computer>> ReadAllAsync()
        {
            _lock.EnterReadLock();
            try
            {
                var snapshot = _computers.ToList(); 
                return Task.FromResult<IEnumerable<Computer>>(snapshot);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        
        public Task<IEnumerable<Computer>> ReadAllAsync(int page, int amount)
        {
            if (page < 1 || amount < 1)
                return Task.FromResult<IEnumerable<Computer>>(Array.Empty<Computer>());

            _lock.EnterReadLock();
            try
            {
                var result = _computers
                    .Skip((page - 1) * amount)
                    .Take(amount)
                    .ToList();

                return Task.FromResult<IEnumerable<Computer>>(result);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        
        public Task<bool> UpdateAsync(Computer element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            _lock.EnterUpgradeableReadLock();
            try
            {
                var existing = _computers.FirstOrDefault(c => c.Id == element.Id);
                if (existing == null)
                    return Task.FromResult(false);

                _lock.EnterWriteLock();
                try
                {
                    _computers.Remove(existing);
                    _computers.Add(element);
                    return Task.FromResult(true);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }
            }
            finally
            {
                _lock.ExitUpgradeableReadLock();
            }
        }

       
        public Task<bool> RemoveAsync(Computer element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            _lock.EnterWriteLock();
            try
            {
                bool removed = _computers.RemoveAll(c => c.Id == element.Id) > 0;
                return Task.FromResult(removed);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        
        public async Task<bool> SaveAsync()
        {
            try
            {
                List<Computer> snapshot;
                _lock.EnterReadLock();
                try
                {
                    snapshot = _computers.ToList(); 
                }
                finally
                {
                    _lock.ExitReadLock();
                }

                string json = JsonSerializer.Serialize(snapshot, _options);
                await File.WriteAllTextAsync(FilePath, json);
                Console.WriteLine($"Дані збережено у файл: {FilePath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження: {ex.Message}");
                return false;
            }
        }

      
        public async Task<bool> LoadAsync()
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    Console.WriteLine("Файл не знайдено, створюємо нову колекцію.");
                    return false;
                }

                string json = await File.ReadAllTextAsync(FilePath);
                var loaded = JsonSerializer.Deserialize<List<Computer>>(json, _options);

                if (loaded == null)
                    return false;

                _lock.EnterWriteLock();
                try
                {
                    _computers.Clear();
                    _computers.AddRange(loaded);
                }
                finally
                {
                    _lock.ExitWriteLock();
                }

                Console.WriteLine($"Дані завантажено з файлу: {FilePath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка завантаження: {ex.Message}");
                return false;
            }
        }

        
        public IEnumerator<Computer> GetEnumerator()
        {
            List<Computer> snapshot;
            _lock.EnterReadLock();
            try
            {
                snapshot = _computers.ToList();
            }
            finally
            {
                _lock.ExitReadLock();
            }
            return snapshot.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
    // Синхронні сервіси
    /*private readonly List<Computer> _computers = new();
    private static readonly string FilePath = "computers.json";

    public void Create(Computer element) => _computers.Add(element);

    public Computer Read(Guid id) => _computers.FirstOrDefault(c => c.Id == id);

    public IEnumerable<Computer> ReadAll() => _computers;

    public void Update(Computer element) 
    {
        var existing = Read(element.Id);
        if (existing != null)
        {
            _computers.Remove(existing);
            _computers.Add(element);
        }
    }

    public void Remove(Computer element) => _computers.Remove(element);


    public void Save()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(_computers, options);
        File.WriteAllText(FilePath, json);
        Console.WriteLine($"Дані збережено у файл: {FilePath}");
    }

    public void Load()
    {
        if (!File.Exists(FilePath))
        {
            Console.WriteLine("Файл завантаження не знайдено або його не існує.");
            return;
        }

        string json = File.ReadAllText(FilePath);
        var loaded = JsonSerializer.Deserialize<List<Computer>>(json);
        if (loaded != null)
        {
            _computers.Clear();
            _computers.AddRange(loaded);
            Console.WriteLine($"Дані завантажено з файлу: {FilePath}");
        }
    }
*/
}

