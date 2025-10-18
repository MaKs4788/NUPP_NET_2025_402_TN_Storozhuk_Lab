using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Common
{
    public class Paralell
    {
        // Метод генерації комп'ютерів
        public static async Task GenerateComputersAsync(ICrudServiceAsync<Computer> service, int count)
        {
            var tasks = new List<Task>();

            for (int i = 0; i < count; i++)
            {
                int idx = i;
                tasks.Add(Task.Run(async () =>
                {
                    var comp = Computer.GenerateRandom();
                    await service.CreateAsync(comp);
                }));
            }

            await Task.WhenAll(tasks);
        }

        // Метод створення статистики
        public static void ComputeStatistics(IEnumerable<Computer> computers)
        {
            if (!computers.Any()) return;

            var ramList = computers.Select(c => c.RAM).ToList();
            var vramList = computers.Select(c => c.GPU.VRAM).ToList();
            var cpuCores = computers.Select(c => c.CPU.Cores).ToList();

            Console.WriteLine("=== Статистика ===");
            Console.WriteLine($"RAM: min={ramList.Min()}, max={ramList.Max()}, avg={ramList.Average():F2}");
            Console.WriteLine($"VRAM: min={vramList.Min()}, max={vramList.Max()}, avg={vramList.Average():F2}");
            Console.WriteLine($"CPU Cores: min={cpuCores.Min()}, max={cpuCores.Max()}, avg={cpuCores.Average():F2}");
        }

    }
}
