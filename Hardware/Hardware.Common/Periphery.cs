using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Common
{
    public class Periphery
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string DeviceType {  get; set; }

        public string Brand { get; set; }

        public string ConnectionType { get; set; }

        // Подія

        public event ComponentChangedEventHandler Changed;

        // Конструктор
        public Periphery(string deviceType, string brand, string connectionType)
        {
            DeviceType = deviceType;
            Brand = brand;
            ConnectionType = connectionType;
        }

        public Periphery() { }
        // Метод для зміни назви пристрою
        public void RenameDevice(string newName)
        {
            string oldName = DeviceType;
            DeviceType = newName;
            Changed?.Invoke($"Periphery {oldName} змінено на {DeviceType}");
        }

        // Метод для відображення інформації
        public void ShowInfo()
        {
            Console.WriteLine($"Device: {DeviceType}, Brand: {Brand}, Connection: {ConnectionType}");
        }
    }
}
