using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Infrastructure.Models
{
    [Table("Peripheries")]
    public class PeripheryModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string DeviceType { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Brand { get; set; } = null!;

        [Required, MaxLength(30)]
        public string ConnectionType { get; set; } = null!;

        [ForeignKey("Computer")]
        public Guid ComputerId { get; set; }

        public ComputerModel Computer { get; set; } = null!;
    }
}
