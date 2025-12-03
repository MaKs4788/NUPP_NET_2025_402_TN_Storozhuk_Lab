namespace Setup.REST.Models
{
    public class PeripheryDto
    {
        public Guid Id { get; set; }
        public string DeviceType { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string ConnectionType { get; set; } = null!;
        public Guid ComputerId { get; set; }
    }

    public class CreatePeripheryDto
    {
        public string DeviceType { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string ConnectionType { get; set; } = null!;
        public Guid ComputerId { get; set; }
    }
}
