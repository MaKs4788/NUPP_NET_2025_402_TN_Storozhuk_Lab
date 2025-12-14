using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Infrastructure.Models
{
    [Table("CPUs")]
    public class CPUModel : HardwareModel
    {
        [Range(1, 128)]
        public int Cores { get; set; }

        [Range(1, 256)]
        public int Threads { get; set; }

        [Range(0.5, 10.0)]
        public double Frequency { get; set; }
        [ForeignKey("Computer")]
        public Guid ComputerId { get; set; }
        public ComputerModel Computer { get; set; } = null!;
    }
}
