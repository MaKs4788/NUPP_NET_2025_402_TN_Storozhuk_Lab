using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Infrastructure.Models
{
    [Table("Computers")]
    public class ComputerModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [Range(4, 512)]
        public int RAM { get; set; }

        [Range(64, 8192)]
        public int Storage { get; set; }
        public CPUModel? CPU { get; set; }
        public GPUModel? GPU { get; set; }
        public SoftwareModel? Software { get; set; }

        public ICollection<PeripheryModel> Peripheries { get; set; } = new List<PeripheryModel>();
    }

}
