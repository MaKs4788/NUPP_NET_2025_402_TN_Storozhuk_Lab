using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Common
{
    // Делегат
    public delegate void ComponentChangedEventHandler(string message);
    public class Computer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public CPU CPU { get; set; }

        public GPU GPU { get; set; }

        public int RAM { get; set; }

        public int Storage { get; set; }

        public Software Software { get; set; }

        public List<Periphery> Periphery{ get; set; }

        public event ComponentChangedEventHandler Upgraded;

        public static int Count;

        // Статисний конструктор
        static Computer()
        {
            Count = 0;
        }

        // Конструктор
        public Computer(string name, CPU cPU, GPU gPU, int rAM, int storage, Software software, List<Periphery> periphery)
        {
            Name = name;
            CPU = cPU;
            GPU = gPU;
            RAM = rAM;
            Storage = storage;
            Software = software;
            Periphery = periphery;
            Count++;
        }

        public Computer() { }

        // Метод апгрейду RAM
        public void UpgradeRAM(int additionalRam)
        {
            RAM += additionalRam;
            Upgraded?.Invoke($"Комп'ютер {Name} оновлено RAM на {additionalRam} GB");
        }

        // Метод виводу інформації про ПК
        public void ShowInfo()
        {
            Console.WriteLine($"--- Computer: {Name} ---");
            Console.WriteLine($"RAM: {RAM}GB, Storage: {Storage}GB");
            CPU.ShowInfo();
            GPU.ShowInfo();
            Software.ShowInfo();
            Console.WriteLine("Peripherals:");
            foreach (var p in Periphery)
            {
                p.ShowInfo();
            }
        }

        // Статичний метод
        public static int GetCount() => Count;

        // Метод рандомної генерації комп'ютерів
        public static Computer GenerateRandom()
        {
            var rand = new Random();

            var cpu = CPU.GenerateRandom();
            var gpu = GPU.GenerateRandom();
            var software = Software.GenerateRandom();

            // Генеруємо кілька пристроїв периферії
            int perCount = rand.Next(1, 4);
            var peripheryList = new List<Periphery>();
            for (int i = 0; i < perCount; i++)
                peripheryList.Add(Setup.Common.Periphery.GenerateRandom());

            // Формуємо комп'ютер
            return new Computer
            {
                Id = Guid.NewGuid(),
                Name = $"PC-{rand.Next(1000, 9999)}",
                CPU = cpu,
                GPU = gpu,
                RAM = rand.Next(8, 65),
                Storage = rand.Next(256, 2049),
                Software = software,
                Periphery = peripheryList
            };
        }

    }

    public static class ComputerExtensions
    {
        // Метод розширення для Computer
        public static void ShowSummary(this Computer comp)
        {
            Console.WriteLine($"--- Summary for {comp.Name} ---");
            Console.WriteLine($"CPU: {comp.CPU.Brand} {comp.CPU.Model}");
            Console.WriteLine($"GPU: {comp.GPU.Brand} {comp.GPU.Model}");
            Console.WriteLine($"RAM: {comp.RAM}GB, Storage: {comp.Storage}GB");
            Console.WriteLine($"OS: {comp.Software.OS}, Version: {comp.Software.OSVersion}");
        }
    }

}
