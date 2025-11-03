using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Setup.Common
{
    public class ComputerService<T>: ICrudServiceAsync<T> where T: Computer
    {
        private readonly ConcurrentDictionary<Guid, T> _computers = new();
        private static readonly string FilePath = "computers.json";
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };
        private readonly SemaphoreSlim _fileSemaphore = new(1, 1);

        public Task<bool> CreateAsync(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            return Task.FromResult(_computers.TryAdd(element.Id, element));
        }

        public Task<T?> ReadAsync(Guid id)
            => Task.FromResult(_computers.TryGetValue(id, out var c) ? c : null);

        public Task<IEnumerable<T>> ReadAllAsync()
            => Task.FromResult<IEnumerable<T>>(_computers.Values.ToList());

        public Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
        {
            if (page < 1 || amount < 1)
                return Task.FromResult<IEnumerable<T>>(Array.Empty<T>());

            var result = _computers.Values
                .Skip((page - 1) * amount)
                .Take(amount)
                .ToList();

            return Task.FromResult<IEnumerable<T>>(result);
        }

        public Task<bool> UpdateAsync(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            if (!_computers.ContainsKey(element.Id))
                return Task.FromResult(false);

            _computers[element.Id] = element;
            return Task.FromResult(true);
        }

        public Task<bool> RemoveAsync(T element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            return Task.FromResult(_computers.TryRemove(element.Id, out _));
        }

        public async Task<bool> SaveAsync()
        {
            await _fileSemaphore.WaitAsync();
            try
            {
                string json = JsonSerializer.Serialize(_computers.Values.ToList(), _options);
                await File.WriteAllTextAsync(FilePath, json);
                Console.WriteLine($"Дані збережено у файл: {FilePath}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження: {ex.Message}");
                return false;
            }
            finally
            {
                _fileSemaphore.Release();
            }
        }

        public async Task<bool> LoadAsync()
        {
            await _fileSemaphore.WaitAsync();
            try
            {
                if (!File.Exists(FilePath))
                    return false;

                string json = await File.ReadAllTextAsync(FilePath);
                var loaded = JsonSerializer.Deserialize<List<T>>(json, _options);
                if (loaded == null)
                    return false;

                _computers.Clear();
                foreach (var c in loaded)
                    _computers[c.Id] = c;

                Console.WriteLine("Дані завантажено.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка завантаження: {ex.Message}");
                return false;
            }
            finally
            {
                _fileSemaphore.Release();
            }
        }

        public IEnumerator<T> GetEnumerator() => _computers.Values.GetEnumerator();
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

