namespace Setup.REST.Models
{
    public class ComputerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int RAM { get; set; }
        public int Storage { get; set; }
    }

    public class CreateComputerDto
    {
        public string Name { get; set; } = null!;
        public int RAM { get; set; }
        public int Storage { get; set; }
    }
}
