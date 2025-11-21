
namespace Setup.Console
{
    using Setup.Common;
    using Setup.Infrastructure;
    using Setup.Infrastructure.Services;
    using Setup.Infrastructure.Models;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Setup.Infrastructure.Repositories;


    public class ConsoleService
    {
        //Створення комп'ютера
        public async Task CreateComputerAsync(ICrudServiceAsync<Computer> service)
            {
                Console.Write("Введіть назву комп'ютера: ");
                string name = Console.ReadLine();

                //CPU
                Console.Write("CPU (бренд): ");
                string cpuBrand = Console.ReadLine();
                Console.Write("CPU (модель): ");
                string cpuModel = Console.ReadLine();
                Console.Write("Кількість ядер: ");
                int cores = int.Parse(Console.ReadLine());
                Console.Write("Кількість потоків: ");
                int threads = int.Parse(Console.ReadLine());
                Console.Write("Частота (GHz): ");
                double freq = double.Parse(Console.ReadLine());
                var cpu = new CPU(cpuBrand, cpuModel, cores, threads, freq);

                //GPU
                Console.Write("GPU (бренд): ");
                string gpuBrand = Console.ReadLine();
                Console.Write("GPU (модель): ");
                string gpuModel = Console.ReadLine();
                Console.Write("VRAM (ГБ): ");
                int vram = int.Parse(Console.ReadLine());
                Console.Write("Тип пам'яті: ");
                string memType = Console.ReadLine();
                Console.Write("Core Clock (MHz): ");
                double clock = double.Parse(Console.ReadLine());
                var gpu = new GPU(gpuBrand, gpuModel, vram, memType, clock);

                //RAM + Storage
                Console.Write("RAM (ГБ): ");
                int ram = int.Parse(Console.ReadLine());
                Console.Write("Накопичувач (ГБ): ");
                int storage = int.Parse(Console.ReadLine());

                //Software
                Console.Write("ОС: ");
                string os = Console.ReadLine();
                Console.Write("Антивірус: ");
                string antivirus = Console.ReadLine();
                Console.Write("Версія ОС: ");
                string version = Console.ReadLine();
                var software = new Software(os, antivirus, version);

                //Периферія
                var periphery = new List<Periphery>();
                Console.Write("Кількість периферійних пристроїв: ");
                int count = int.Parse(Console.ReadLine());
                for (int i = 0; i < count; i++)
                {
                    Console.Write($"[{i + 1}] Тип пристрою: ");
                    string device = Console.ReadLine();
                    Console.Write("Бренд: ");
                    string brand = Console.ReadLine();
                    Console.Write("Тип підключення: ");
                    string connection = Console.ReadLine();
                    periphery.Add(new Periphery(device, brand, connection));
                }

                var comp = new Computer(name, cpu, gpu, ram, storage, software, periphery);

                //Події
                comp.Upgraded += msg => Console.WriteLine($"Подія: {msg}");
                comp.Software.Updated += msg => Console.WriteLine($"Подія: {msg}");
                foreach (var p in comp.Periphery)
                    p.Changed += msg => Console.WriteLine($"Подія: {msg}");

                bool result = await service.CreateAsync(comp);
                Console.WriteLine(result
                    ? $"Комп'ютер {name} створено та додано в систему!"
                    : "Помилка створення комп'ютера.");
            }

            //Показ усіх ПК
            public async Task ShowAllAsync(ICrudServiceAsync<Computer> service)
            {
                Console.Write("Вкажіть сторінку: ");
                int page = int.Parse(Console.ReadLine());
                Console.WriteLine($"\n=== Усі комп'ютери ===\nСторінка: {page}");
                var all = await service.ReadAllAsync(page, 10);

                foreach (var comp in all)
                {
                    comp.ShowSummary();
                    Console.WriteLine($"ID: {comp.Id}\n");
                }

                /*Console.WriteLine($"За весь час існування сервісу створено {Computer.GetCount()} комп'ютерів");*/
            }

            //Апгрейд RAM
            public async Task UpgradeRamAsync(ICrudServiceAsync<Computer> service)
            {
                Console.Write("Введіть ID комп'ютера: ");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    var comp = await service.ReadAsync(id);
                    if (comp != null)
                    {
                        Console.Write("На скільки ГБ збільшити RAM: ");
                        int add = int.Parse(Console.ReadLine());
                        comp.UpgradeRAM(add);
                        await service.UpdateAsync(comp);
                    }
                    else Console.WriteLine("Комп'ютер не знайдено!");
                }
            }

            //Оновлення ОС
            public async Task UpdateSoftwareAsync(ICrudServiceAsync<Computer> service)
            {
                Console.Write("Введіть ID комп'ютера: ");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    var comp = await service.ReadAsync(id);
                    if (comp != null)
                    {
                        Console.Write("Введіть нову версію ОС: ");
                        string newVer = Console.ReadLine();
                        comp.Software.UpdateVersion(newVer);
                        await service.UpdateAsync(comp);
                    }
                    else Console.WriteLine("Комп'ютер не знайдено!");
                }
            }

            //Перейменування периферії
            public async Task RenamePeripheryAsync(ICrudServiceAsync<Computer> service)
            {
                Console.Write("Введіть ID комп'ютера: ");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    var comp = await service.ReadAsync(id);
                    if (comp != null && comp.Periphery.Count > 0)
                    {
                        for (int i = 0; i < comp.Periphery.Count; i++)
                            Console.WriteLine($"{i + 1}. {comp.Periphery[i].DeviceType} ({comp.Periphery[i].Brand})");

                        Console.Write("Виберіть пристрій для перейменування (номер): ");
                        int num = int.Parse(Console.ReadLine()) - 1;
                        if (num >= 0 && num < comp.Periphery.Count)
                        {
                            Console.Write("Введіть нову назву пристрою: ");
                            string newName = Console.ReadLine();
                            comp.Periphery[num].RenameDevice(newName);
                            await service.UpdateAsync(comp);
                        }
                    }
                }
            }

            //Видалення комп'ютера
            public async Task RemoveComputerAsync(ICrudServiceAsync<Computer> service)
            {
                Console.Write("Введіть ID комп'ютера: ");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    var comp = await service.ReadAsync(id);
                    if (comp != null)
                        await service.RemoveAsync(comp);
                    else
                        Console.WriteLine("Комп'ютер не знайдено!");
                }
            }

            //Детальний перегляд
            public async Task ShowComputerDetailsAsync(ICrudServiceAsync<Computer> service)
            {
                Console.Write("Введіть ID комп'ютера: ");
                if (Guid.TryParse(Console.ReadLine(), out Guid id))
                {
                    var comp = await service.ReadAsync(id);
                    if (comp != null)
                        comp.ShowInfo();
                    else
                        Console.WriteLine("Комп'ютер не знайдено!");
                }
            }

        // Генерація комп'ютерів
        public async Task GenerateComputersAsync(ICrudServiceAsync<Computer> service)
        {
            Console.Write("Скільки комп'ютерів створити: ");
            int count = int.TryParse(Console.ReadLine(), out var tmp) ? tmp : 1000;

            Console.WriteLine("Створення комп'ютерів...");
            await Paralell.GenerateComputersAsync(service, count);

            var allComputers = await service.ReadAllAsync();

            Console.WriteLine("Обчислення статистики...");
            Paralell.ComputeStatistics(allComputers);

            Console.WriteLine("Збереження у файл...");
            await service.SaveAsync();

            Console.WriteLine("Генерація, статистика та збереження завершені!");
        }
    }
    }

    