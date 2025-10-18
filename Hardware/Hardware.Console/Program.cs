using System;
using System.Collections.Generic;
using System.Text;
using Setup.Common;
using Setup.Console;
Console.OutputEncoding = UTF8Encoding.UTF8;
ICrudServiceAsync<Computer> computerService = new ComputerService();
await computerService.LoadAsync();
ConsoleService consoleService = new ConsoleService();

while (true)
{
    Console.WriteLine("\n=== ���� ===");
    Console.WriteLine("1 - �������� ����'����");
    Console.WriteLine("2 - �������� �� ����'�����");
    Console.WriteLine("3 - ������� RAM ����'�����");
    Console.WriteLine("4 - ������� ����� ��");
    Console.WriteLine("5 - ������������� ����������� �������");
    Console.WriteLine("6 - ������� ����'�����");
    Console.WriteLine("7 - ��������� �������� ����'�����");
    Console.WriteLine("8 - �������� ����'����");
    Console.WriteLine("9 - �������� ����");
    Console.WriteLine("0 - �����");
    Console.Write("��� ����: ");
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
            await consoleService.GenerateComputersAsync(computerService);// �������   /*Console.WriteLine($"�� ���� ��� ��������� ������ ���� �������� {Computer.GetCount()} ����'�����");*/
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
            Console.WriteLine("������� ����!");
            break;
    }
}
      

