using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Infrastructure.Models
{
    [Table("GPUs")]
    public class GPUModel : HardwareModel
    {
        [Range(1, 64)]
        public int VRAM { get; set; }

        [Required, MaxLength(20)]
        public string MemoryType { get; set; } = null!;

        [Range(500, 5000)]
        public double CoreClock { get; set; }

        [ForeignKey("Computer")]
        public Guid ComputerId { get; set; }
        public ComputerModel Computer { get; set; } = null!;
    }
}
