using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Common
{
    public class Hardware
    {
        public string Type { get; set; }
        public string Brand {  get; set; }
        public string Model { get; set; }

        // Конструктор
        public Hardware(string brand, string model, string type)
        {
            Brand = brand;
            Model = model;
            Type = type;
        }

        public Hardware() { }

        // Метод для відображення інформації
        public virtual void ShowInfo()
        {
            Console.WriteLine($"Hardware: {Brand} {Model}, Type: {Type}");
        }

    }


    public class CPU : Hardware
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Cores { get; set; }

        public int Threads { get; set; }

        public double Frequency { get; set; }

        // Конструктор
        public CPU(string brand, string model, int cores, int threads, double frequency)
            : base(brand, model, "CPU")
        {
            Cores = cores;
            Threads = threads;
            Frequency = frequency;
        }

        public CPU() { }
        // Метод для відображення інформації
        public override void ShowInfo()
        {
            Console.WriteLine($"CPU: {Brand} {Model}, {Cores} cores, {Threads} threads, {Frequency} GHz");
        }

        public static CPU GenerateRandom()
        {
            var rand = new Random();
            var brands = new[] { "Intel", "AMD" };
            var intelModels = new[] { "i5-12400", "i7-13700K", "i9-13900K" };
            var amdModels = new[] { "Ryzen 5 5600X", "Ryzen 7 7800X3D", "Ryzen 9 7950X" };

            var brand = brands[rand.Next(brands.Length)];
            var model = brand == "Intel"
                ? intelModels[rand.Next(intelModels.Length)]
                : amdModels[rand.Next(amdModels.Length)];

            int cores = rand.Next(4, 17);
            int threads = cores * 2;
            double freq = Math.Round(rand.NextDouble() * 3 + 3.5, 2); // 3 – 5.5 GHz

            return new CPU(brand, model, cores, threads, freq);
        }

    }

    public class GPU : Hardware
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int VRAM { get; set; }

        public string MemoryType {  get; set; }

        public double CoreClock {  get; set; }

        // Конструктор
        public GPU(string brand, string model, int vram, string memoryType, double coreClock)
            : base(brand, model, "GPU")
        {
            VRAM = vram;
            MemoryType = memoryType;
            CoreClock = coreClock;
        }

        public GPU() { }
        // Метод для відображення інформації 
        public override void ShowInfo()
        {
            Console.WriteLine($"GPU: {Brand} {Model}, {VRAM} GB {MemoryType}, {CoreClock} MHz");
        }
        public static GPU GenerateRandom()
        {
            var rand = new Random();
            var brands = new[] { "NVIDIA", "AMD" };
            var nvidiaModels = new[] { "RTX 4060", "RTX 4070", "RTX 4080" };
            var amdModels = new[] { "RX 6700 XT", "RX 7800 XT", "RX 7900 XTX" };

            var brand = brands[rand.Next(brands.Length)];
            var model = brand == "NVIDIA"
                ? nvidiaModels[rand.Next(nvidiaModels.Length)]
                : amdModels[rand.Next(amdModels.Length)];

            int vram = rand.Next(4, 17);
            var memoryTypes = new[] { "GDDR5", "GDDR6", "GDDR6X" };
            var memType = memoryTypes[rand.Next(memoryTypes.Length)];
            double clock = rand.Next(1500, 2800); // MHz

            return new GPU(brand, model, vram, memType, clock);
        }
    }


}
