using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Infrastructure.Models
{
    [Table("Softwares")]
    public class SoftwareModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string OS { get; set; } = null!;

        [Required, MaxLength(50)]
        public string OSVersion { get; set; } = null!;

        [MaxLength(50)]
        public string? Antivirus { get; set; }

        [ForeignKey("Computer")]
        public Guid ComputerId { get; set; }
        public ComputerModel Computer { get; set; } = null!;
    }
}
