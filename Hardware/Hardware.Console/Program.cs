using System;
using System.Collections.Generic;
using System.Text;
using Setup.Common;
Console.OutputEncoding = UTF8Encoding.UTF8;
ICrudService<Computer> computerService = new ComputerService();
computerService.Load();

while (true)
{
    Console.WriteLine("\n=== ���� ===");
    Console.WriteLine("1 - �������� ����'����");
    Console.WriteLine("2 - �������� �� ����'�����");
    Console.WriteLine("3 - ������� RAM ����'�����");
    Console.WriteLine("4 - ������� ����� ��");
    Console.WriteLine("5 - ������������� ����������� �������");
    Console.WriteLine("6 - �������� ������� ��������� ����'�����");
    Console.WriteLine("7 - ��������� �������� ����'�����");
    Console.WriteLine("8 - �������� ����'����");
    Console.WriteLine("9 - �������� ����");
    Console.WriteLine("0 - �����");
    Console.Write("��� ����: ");
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
            Console.WriteLine($"�� ���� ��� ��������� ������ ���� �������� {Computer.GetCount()} ����'�����");
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
            Console.WriteLine("������� ����!");
            break;
    }
}
      

        // ����� ��������� ����'�����
static void CreateComputer(ICrudService<Computer> service)
{
    Console.Write("������ ����� ����'�����: ");
    string name = Console.ReadLine();

    // CPU
    Console.Write("CPU (�����): ");
    string cpuBrand = Console.ReadLine();
    Console.Write("CPU (������): ");
    string cpuModel = Console.ReadLine();
    Console.Write("ʳ������ ����: ");
    int cores = int.Parse(Console.ReadLine());
    Console.Write("ʳ������ ������: ");
    int threads = int.Parse(Console.ReadLine());
    Console.Write("������� (GHz): ");
    double freq = double.Parse(Console.ReadLine());
    var cpu = new CPU(cpuBrand, cpuModel, cores, threads, freq);

    // GPU
    Console.Write("GPU (�����): ");
    string gpuBrand = Console.ReadLine();
    Console.Write("GPU (������): ");
    string gpuModel = Console.ReadLine();
    Console.Write("VRAM (��): ");
    int vram = int.Parse(Console.ReadLine());
    Console.Write("��� ���'��: ");
    string memType = Console.ReadLine();
    Console.Write("Core Clock (MHz): ");
    double clock = double.Parse(Console.ReadLine());
    var gpu = new GPU(gpuBrand, gpuModel, vram, memType, clock);

    // RAM �� Storage
    Console.Write("RAM (��): ");
    int ram = int.Parse(Console.ReadLine());
    Console.Write("����������� (��): ");
    int storage = int.Parse(Console.ReadLine());

    // Software
    Console.Write("��: ");
    string os = Console.ReadLine();
    Console.Write("��������: ");
    string antivirus = Console.ReadLine();
    Console.Write("����� ��: ");
    string version = Console.ReadLine();
    var software = new Software(os, antivirus, version);

    // ��������
    var periphery = new List<Periphery>();
    Console.Write("ʳ������ ����������� ��������: ");
    int count = int.Parse(Console.ReadLine());
    for (int i = 0; i < count; i++)
    {
        Console.Write($"[{i + 1}] ��� ��������: ");
        string device = Console.ReadLine();
        Console.Write("�����: ");
        string brand = Console.ReadLine();
        Console.Write("��� ����������: ");
        string connection = Console.ReadLine();
        periphery.Add(new Periphery(device, brand, connection));
    }

    var comp = new Computer(name, cpu, gpu, ram, storage, software, periphery);

    // ϳ������ �� ��䳿
    comp.Upgraded += msg => Console.WriteLine($"����: {msg}");
    comp.Software.Updated += msg => Console.WriteLine($"����: {msg}");
    foreach (var p in comp.Periphery)
        p.Changed += msg => Console.WriteLine($"����: {msg}");

    service.Create(comp);
    Console.WriteLine($"����'���� {name} �������� �� ������ � �������!");
}

// �������� �� ����'�����
static void ShowAll(ICrudService<Computer> service)
{
    Console.WriteLine("\n=== �� ����'����� ===");
    foreach (var comp in service.ReadAll())
    {
        comp.ShowSummary();
        Console.WriteLine($"ID: {comp.Id}\n");
    }
    Console.WriteLine($"�� ���� ��� ��������� ������ ���� �������� {Computer.GetCount()} ����'�����");
}

// ������� RAM
static void UpgradeRam(ICrudService<Computer> service)
{
    Console.Write("������ ID ����'�����: ");
    if (Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        var comp = service.Read(id);
        if (comp != null)
        {
            Console.Write("�� ������ �� �������� RAM: ");
            int add = int.Parse(Console.ReadLine());
            comp.UpgradeRAM(add);
            service.Update(comp);
        }
        else Console.WriteLine("����'���� �� ��������!");
    }
}

// ��������� ��
static void UpdateSoftware(ICrudService<Computer> service)
{
    Console.Write("������ ID ����'�����: ");
    if (Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        var comp = service.Read(id);
        if (comp != null)
        {
            Console.Write("������ ���� ����� ��: ");
            string newVer = Console.ReadLine();
            comp.Software.UpdateVersion(newVer);
            service.Update(comp);
        }
        else Console.WriteLine("����'���� �� ��������!");
    }
}

// �������������� �������
static void RenamePeriphery(ICrudService<Computer> service)
{
    Console.Write("������ ID ����'�����: ");
    if (Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        var comp = service.Read(id);
        if (comp != null && comp.Periphery.Count > 0)
        {
            for (int i = 0; i < comp.Periphery.Count; i++)
                Console.WriteLine($"{i + 1}. {comp.Periphery[i].DeviceType} ({comp.Periphery[i].Brand})");

            Console.Write("������� ������� ��� �������������� (�����): ");
            int num = int.Parse(Console.ReadLine()) - 1;
            if (num >= 0 && num < comp.Periphery.Count)
            {
                Console.Write("������ ���� ����� ��������: ");
                string newName = Console.ReadLine();
                comp.Periphery[num].RenameDevice(newName);
                service.Update(comp);
            }
        }
    }
}

// ��������� ����'�����
static void RemoveComputer(ICrudService<Computer> service)
{
    Console.Write("������ ID ����'�����: ");
    if (Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        var comp = service.Read(id);
        if (comp != null)
        {
            service.Remove(comp);
        }
        else
            Console.WriteLine("����'���� �� ��������!");
    }
}

// ��������� �������� ��
static void ShowComputerDetails(ICrudService<Computer> service)
{
    Console.Write("������ ID ����'����� ��� ��������� �������: ");
    if (Guid.TryParse(Console.ReadLine(), out Guid id))
    {
        var comp = service.Read(id);
        if (comp != null)
            comp.ShowInfo();
        else
            Console.WriteLine("����'���� �� ��������!");
    }
}