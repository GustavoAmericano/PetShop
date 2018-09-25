using Microsoft.EntityFrameworkCore;
using PetShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetShop.Data
{
    public class PetShopContext : DbContext
    {
        public PetShopContext(DbContextOptions<PetShopContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pet>()
                .HasOne(p => p.Owner)
                .WithMany(o => o.Pets)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<PetColor>()
                .HasKey(pc => new { pc.PetId, pc.ColorId });

            modelBuilder.Entity<PetColor>()
                .HasOne<Color>(pc => pc.Color)
                .WithMany(c => c.Pets)
                .HasForeignKey(pc => pc.ColorId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<PetColor>()
                .HasOne<Pet>(pc => pc.Pet)
                .WithMany(p => p.Colors)
                .HasForeignKey(pc => pc.PetId)
                .OnDelete(DeleteBehavior.SetNull);
        }
        
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<PetColor> PetColors { get; set; }
    }
}
