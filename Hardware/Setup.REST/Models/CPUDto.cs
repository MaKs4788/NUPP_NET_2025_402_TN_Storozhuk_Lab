namespace Setup.REST.Models
{
    public class CPUDto
    {
        public Guid Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Cores { get; set; }
        public int Threads { get; set; }
        public double Frequency { get; set; }
        public Guid ComputerId { get; set; }
    }

    public class CreateCPUDto
    {
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Cores { get; set; }
        public int Threads { get; set; }
        public double Frequency { get; set; }
        public Guid ComputerId { get; set; }
    }
}
