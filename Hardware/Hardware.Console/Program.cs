using System;
using System.Collections.Generic;
using System.Text;
using Setup.Common;
Console.OutputEncoding = UTF8Encoding.UTF8;
ICrudService<Computer> computerService = new ComputerService();
computerService.Load();

while (true)
{
    Console.WriteLine("\n=== Меню ===");
    Console.WriteLine("1 - Створити комп'ютер");
    Console.WriteLine("2 - Показати всі комп'ютери");
    Console.WriteLine("3 - Апгрейд RAM комп'ютера");
    Console.WriteLine("4 - Оновити версію ОС");
    Console.WriteLine("5 - Перейменувати периферійний пристрій");
    Console.WriteLine("6 - Показати кількість створених комп'ютерів");
    Console.WriteLine("7 - Детальний перегляд комп'ютера");
    Console.WriteLine("8 - Видалити комп'ютер");
    Console.WriteLine("9 - Зберегти зміни");
    Console.WriteLine("0 - Вихід");
    Console.Write("Ваш вибір: ");
    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            CreateComputer(computerService);
            break;
        case "2":
            ShowAll(computerService);
            break;
        case "3":
            UpgradeRam(computerService);
            break;
        case "4":
            UpdateSoftware(computerService);
            break;
        case "5":
            RenamePeriphery(computerService);
            break;
        case "6":
            Console.WriteLine($"За весь час існування сервісу було створено {Computer.GetCount()} комп'ютерів");
            break;
        case "7":
            ShowComputerDetails(computerService);
            break;
        case "8":
            RemoveComputer(computerService);
            break;
        case "9":
            computerService.Save();
            break;
        case "0":
            return;
        default:
            Console.WriteLine("Невірний вибір!");
            break;
    }
}
      

        // Метод створення комп'ютера
static void CreateComputer(ICrudService<Computer> service)
{
    Console.Write("Введіть назву комп'ютера: ");
    string name = Console.ReadLine();

    // CPU
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

    // GPU
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

    // RAM та Storage
    Console.Write("RAM (ГБ): ");
    int ram = int.Parse(Console.ReadLine());
    Console.Write("Накопичувач (ГБ): ");
    int storage = int.Parse(Console.ReadLine());

    // Software
    Console.Write("ОС: ");
    string os = Console.ReadLine();
    Console.Write("Антивірус: ");
    string antivirus = Console.ReadLine();
    Console.Write("Версія ОС: ");
    string version = Console.ReadLine();
    var software = new Software(os, antivirus, version);

    // Периферія
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

    // Підписка на події
    comp.Upgraded += msg => Console.WriteLine($"Подія: {msg}");
    comp.Software.Updated += msg => Console.WriteLine($"Подія: {msg}");
    foreach (var p in comp.Periphery)
        p.Changed += msg => Console.WriteLine($"Подія: {msg}");

    service.Create(comp);
    Console.WriteLine($"Комп'ютер {name} створено та додано в систему!");
}

// Показати всі комп'ютери
static void ShowAll(ICrudService<Computer> service)
{
    Console.WriteLine("\n=== Усі комп'ютери ===");
    foreach (var comp in service.ReadAll())
    {
        comp.ShowSummary();
        Console.WriteLine($"ID: {comp.Id}\n");
    }
    Console.WriteLine($"За весь час існування сервісу було створено {Computer.GetCount()} комп'ютерів");
}

// Апгрейд RAM
static void UpgradeRam(ICrudService<Computer> service)
{
    Console.Write("Введіть ID комп'ютера: ");
    if (Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        var comp = service.Read(id);
        if (comp != null)
        {
            Console.Write("На скільки ГБ збільшити RAM: ");
            int add = int.Parse(Console.ReadLine());
            comp.UpgradeRAM(add);
            service.Update(comp);
        }
        else Console.WriteLine("Комп'ютер не знайдено!");
    }
}

// Оновлення ОС
static void UpdateSoftware(ICrudService<Computer> service)
{
    Console.Write("Введіть ID комп'ютера: ");
    if (Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        var comp = service.Read(id);
        if (comp != null)
        {
            Console.Write("Введіть нову версію ОС: ");
            string newVer = Console.ReadLine();
            comp.Software.UpdateVersion(newVer);
            service.Update(comp);
        }
        else Console.WriteLine("Комп'ютер не знайдено!");
    }
}

// Перейменування периферії
static void RenamePeriphery(ICrudService<Computer> service)
{
    Console.Write("Введіть ID комп'ютера: ");
    if (Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        var comp = service.Read(id);
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
                service.Update(comp);
            }
        }
    }
}

// Видалення комп'ютера
static void RemoveComputer(ICrudService<Computer> service)
{
    Console.Write("Введіть ID комп'ютера: ");
    if (Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        var comp = service.Read(id);
        if (comp != null)
        {
            service.Remove(comp);
        }
        else
            Console.WriteLine("Комп'ютер не знайдено!");
    }
}

// Детальний перегляд ПК
static void ShowComputerDetails(ICrudService<Computer> service)
{
    Console.Write("Введіть ID комп'ютера для перегляду деталей: ");
    if (Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        var comp = service.Read(id);
        if (comp != null)
            comp.ShowInfo();
        else
            Console.WriteLine("Комп'ютер не знайдено!");
    }
}