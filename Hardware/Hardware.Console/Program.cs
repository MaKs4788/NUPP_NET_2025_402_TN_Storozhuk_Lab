using System;
using System.Collections.Generic;
using System.Text;
using Setup.Common;
using Setup.Console;
using Setup.Infrastructure;
using Setup.Infrastructure.Repositories;
using Setup.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Setup.Infrastructure.Models;

Console.OutputEncoding = UTF8Encoding.UTF8;

var dbContext = new SetupContext();
dbContext.Database.EnsureCreated();

ICrudServiceAsync<Computer> computerService = new ComputerService<Computer>();


await computerService.LoadAsync();

ConsoleService consoleService = new ConsoleService();


/*while (true)
{
    Console.WriteLine("\n=== Меню ===");
    Console.WriteLine("1 - Створити комп'ютер");
    Console.WriteLine("2 - Показати всі комп'ютери");
    Console.WriteLine("3 - Апгрейд RAM комп'ютера");
    Console.WriteLine("4 - Оновити версію ОС");
    Console.WriteLine("5 - Перейменувати периферійний пристрій");
    Console.WriteLine("6 - Фабрика комп'ютерів");
    Console.WriteLine("7 - Детальний перегляд комп'ютера");
    Console.WriteLine("8 - Видалити комп'ютер");
    Console.WriteLine("9 - Зберегти зміни");
    *//*Console.WriteLine("10 - Створити повний комп'ютер в БД");
    Console.WriteLine("11 - Переглянути всі комп'ютери з БД");
    Console.WriteLine("12 - Оновити CPU в БД");
    Console.WriteLine("13 - Додати периферію до комп'ютера (БД)");
    Console.WriteLine("14 - Видалити комп'ютер з БД");
    Console.WriteLine("15 - Пошук комп'ютерів за параметрами (БД)");
    Console.WriteLine("16 - Статистика БД");*//*
    Console.WriteLine("0 - Вихід");
    Console.Write("Ваш вибір: ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await consoleService.CreateComputerAsync(computerService);
            break;
        case "2":
            await consoleService.ShowAllAsync(computerService);
            break;
        case "3":
            await consoleService.UpgradeRamAsync(computerService);
            break;
        case "4":
            await consoleService.UpdateSoftwareAsync(computerService);
            break;
        case "5":
            await consoleService.RenamePeripheryAsync(computerService);
            break;
        case "6":
            await consoleService.GenerateComputersAsync(computerService);
            break;
        case "7":
            await consoleService.ShowComputerDetailsAsync(computerService);
            break;
        case "8":
            await consoleService.RemoveComputerAsync(computerService);
            break;
        case "9":
            await computerService.SaveAsync();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Невірний вибір!");
            break;
    }
}*/

var computerRepo = new Repository<ComputerModel>(dbContext);
var computerCrudService = new CrudService<ComputerModel>(computerRepo);

var pc = new ComputerModel
{
    Name = "Gaming PC",
    RAM = 32,
    Storage = 1000,
    CPU = new CPUModel
    {
        Brand = "Intel",
        Model = "Core i7-12700K",
        Cores = 12,
        Threads = 20,
        Frequency = 3.6,
        Type = "Processor"
    },
    GPU = new GPUModel
    {
        Brand = "NVIDIA",
        Model = "RTX 3080",
        VRAM = 10,
        MemoryType = "GDDR6X",
        CoreClock = 1710,
        Type = "Graphic Card"
    },
    Software = new SoftwareModel
    {
        OS = "Windows",
        OSVersion = "11",
        Antivirus = "Kaspersky"
    },
    Peripheries = new List<PeripheryModel>
    {
        new() { DeviceType="Keyboard", Brand="Logitech", ConnectionType="USB" },
        new() { DeviceType="Mouse", Brand="Razer", ConnectionType="Wireless" }
    }
};


await computerCrudService.CreateAsyncDB(pc);

var savedComputers = await computerCrudService.ReadAllAsyncDB();

Console.WriteLine("Список комп'ютерів у базі:");
Console.WriteLine($"SQLite DB path: {dbContext.DbPath}");



Console.WriteLine($"- {pc.Name}, RAM={pc.RAM}GB, Storage={pc.Storage}GB");

    if (pc.CPU != null)
    {
        Console.WriteLine($"  CPU: {pc.CPU.Brand} {pc.CPU.Model}, {pc.CPU.Cores} cores, {pc.CPU.Frequency}GHz");
    }

    if (pc.GPU != null)
    {
        Console.WriteLine($"  GPU: {pc.GPU.Brand} {pc.GPU.Model}, {pc.GPU.VRAM}GB {pc.GPU.MemoryType}, CoreClock={pc.GPU.CoreClock}MHz");
    }

    if (pc.Software != null)
    {
        Console.WriteLine($"  OS: {pc.Software.OS} {pc.Software.OSVersion}, Antivirus: {pc.Software.Antivirus}");
    }

    if (pc.Peripheries != null && pc.Peripheries.Any())
    {
        Console.WriteLine("  Периферія:");
        foreach (var p in pc.Peripheries)
        {
            Console.WriteLine($"    - {p.DeviceType} ({p.Brand}, {p.ConnectionType})");
        }
    }

    Console.WriteLine();
