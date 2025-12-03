namespace Setup.REST.Models
{
    public class SoftwareDto
    {
        public Guid Id { get; set; }
        public string OS { get; set; } = null!;
        public string OSVersion { get; set; } = null!;
        public string? Antivirus { get; set; }
        public Guid ComputerId { get; set; }
    }

    public class CreateSoftwareDto
    {
        public string OS { get; set; } = null!;
        public string OSVersion { get; set; } = null!;
        public string? Antivirus { get; set; }
        public Guid ComputerId { get; set; }
    }
}
