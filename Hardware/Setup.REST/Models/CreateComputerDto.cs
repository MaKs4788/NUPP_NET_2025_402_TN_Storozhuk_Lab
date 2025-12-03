using Microsoft.AspNetCore.Mvc;

namespace Setup.REST.Models
{
    public class CreateComputerDto
    {
        public string Name { get; set; } = null!;
        public int RAM { get; set; }
        public int Storage { get; set; }

        public CPUDto? CPU { get; set; }
        public GPUDto? GPU { get; set; }
        public SoftwareDto? Software { get; set; }
        public List<PeripheryDto> Peripheries { get; set; } = new();
    }

    public class CPUDto
    {
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Cores { get; set; }
        public int Threads { get; set; }
        public double Frequency { get; set; }
        public string Type { get; set; } = "Processor";
    }

    public class GPUDto
    {
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int VRAM { get; set; }
        public string MemoryType { get; set; } = null!;
        public double CoreClock { get; set; }
        public string Type { get; set; } = "Graphic Card";
    }

    public class SoftwareDto
    {
        public string OS { get; set; } = null!;
        public string OSVersion { get; set; } = null!;
        public string Antivirus { get; set; } = null!;
    }

    public class PeripheryDto
    {
        public string DeviceType { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string ConnectionType { get; set; } = null!;
    }

    public class ComputerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int RAM { get; set; }
        public int Storage { get; set; }

        public CPUDto? CPU { get; set; }
        public GPUDto? GPU { get; set; }
        public SoftwareDto? Software { get; set; }
        public List<PeripheryDto> Peripheries { get; set; } = new();
    }
}
