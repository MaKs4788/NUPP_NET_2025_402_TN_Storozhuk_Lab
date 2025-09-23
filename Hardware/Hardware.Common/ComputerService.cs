using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Setup.Common
{
    public class ComputerService: ICrudService<Computer>
    {
        private readonly List<Computer> _computers = new();
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
                Console.WriteLine("Файл не знайдено!");
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
    }
}
