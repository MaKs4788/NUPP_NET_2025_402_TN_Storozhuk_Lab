using System;
using System.Collections.Generic;
using System.Text;
using Setup.Common;
using Setup.Console;
Console.OutputEncoding = UTF8Encoding.UTF8;
ICrudServiceAsync<Computer> computerService = new ComputerService<Computer>();
await computerService.LoadAsync();
ConsoleService consoleService = new ConsoleService();

while (true)
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
            await consoleService.GenerateComputersAsync(computerService);// Фабрика   /*Console.WriteLine($"За весь час існування сервісу було створено {Computer.GetCount()} комп'ютерів");*/
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
}
      

