using Microsoft.EntityFrameworkCore;
using Setup.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Setup.Infrastructure
{
    public class SetupContext : DbContext
    {
        public DbSet<ComputerModel> Computers { get; set; }
        public DbSet<CPUModel> CPUs { get; set; }
        public DbSet<GPUModel> GPUs { get; set; }
        public DbSet<SoftwareModel> Softwares { get; set; }
        public DbSet<PeripheryModel> Peripheries { get; set; }
        public DbSet<HardwareModel> Hardwares { get; set; }

        public SetupContext(DbContextOptions<SetupContext> options)
            : base(options)
        {
        }

      
        public SetupContext() { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HardwareModel>().UseTptMappingStrategy();

            modelBuilder.Entity<ComputerModel>(entity =>
            {
                entity.HasOne(e => e.CPU)
                      .WithOne(c => c.Computer)
                      .HasForeignKey<CPUModel>(c => c.ComputerId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);

                entity.HasOne(e => e.GPU)
                      .WithOne(g => g.Computer)
                      .HasForeignKey<GPUModel>(g => g.ComputerId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);

                entity.HasOne(e => e.Software)
                      .WithOne(s => s.Computer)
                      .HasForeignKey<SoftwareModel>(s => s.ComputerId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .IsRequired(false);

                entity.HasMany(e => e.Peripheries)
                      .WithOne(p => p.Computer)
                      .HasForeignKey(p => p.ComputerId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

