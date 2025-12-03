namespace Setup.REST.Models
{
    public class GPUDto
    {
        public Guid Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string MemoryType { get; set; } = null!;
        public int VRAM { get; set; }
        public double CoreClock { get; set; }
        public Guid ComputerId { get; set; }
    }

    public class CreateGPUDto
    {
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string MemoryType { get; set; } = null!;
        public int VRAM { get; set; }
        public double CoreClock { get; set; }
        public Guid ComputerId { get; set; }
    }
}
