using Setup.Common;

CPU cpu = new CPU("Intel", "i9-13900K", 24, 32, 3.0);
GPU gpu = new GPU("NVIDIA", "RTX 4090", 24, "GDDR6X", 1920);
Software software = new Software("Windows 11", "Microsoft Defender", "22H2");
Periphery keyboard = new Periphery("Keyboard", "Logitech", "Wireless");
Periphery mouse = new Periphery("Mouse", "Razer", "USB");

// Підписка на події компонентів
software.Updated += msg => Console.WriteLine(msg);
keyboard.Changed += msg => Console.WriteLine(msg);
mouse.Changed += msg => Console.WriteLine(msg);

// Створення комп'ютера
Computer myPC = new Computer(
    "Gaming PC",
    cpu,
    gpu,
    64,
    2000,
    software,
    new List<Periphery> { keyboard, mouse }
);

// Підписка на подію апгрейду
myPC.Upgraded += msg => Console.WriteLine(msg);

// Виклик методів
myPC.ShowInfo();
myPC.UpgradeRAM(16);              // викликає подію
software.UpdateVersion("23H1");    // викликає подію
keyboard.RenameDevice("Mechanical Keyboard"); // викликає подію

// Метод розширення
myPC.ShowSummary();

// Статичний метод
