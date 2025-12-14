using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Infrastructure.Models
{
    [Table("Hardware")]
    public abstract class HardwareModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Brand { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Model { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Type { get; set; } = null!;
    }
}
