using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Common
{
    
    public class Software
    {
        // Подія
        public event ComponentChangedEventHandler Updated;
        public Guid Id { get; set; } = Guid.NewGuid();
        public string OS {  get; set; }

        public string OSVersion { get; set; }

        public string Antivirus { get; set; }

        // Конструктор
        public Software(string os, string antivirus, string version)
        {
            OS = os;
            Antivirus = antivirus;
            OSVersion = version;
        }

        public Software() { }

        // Метод оновлення версії ОС
        public void UpdateVersion(string newVersion)
        {
            string oldVersion = OSVersion;
            OSVersion = newVersion;
            Updated?.Invoke($"Software {OS} оновлено з версії {oldVersion} на {OSVersion}");
        }

        // Метод виводу інформації
        public void ShowInfo()
        {
            Console.WriteLine($"OS: {OS}, Antivirus: {Antivirus}, Version: {OSVersion}");
        }

        // Метод рандомної генерації ПЗ
        public static Software GenerateRandom()
        {
            var rand = new Random();
            var systems = new[] { "Windows", "Linux" };
            var versions = new[] { "10", "11", "Ubuntu 24.04"};
            var antivirus = new[] { "ESET", "Malwarebytes", "Bitdefender" };

       

            return new Software
            {
                OS = systems[rand.Next(systems.Length)],
                OSVersion = versions[rand.Next(versions.Length)]
            };
        }
    }
}
