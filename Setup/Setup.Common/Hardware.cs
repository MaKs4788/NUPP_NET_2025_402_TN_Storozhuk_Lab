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
    }


}
