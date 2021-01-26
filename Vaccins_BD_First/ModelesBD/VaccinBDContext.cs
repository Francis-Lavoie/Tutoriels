using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Vaccins_BD_First.ModelesBD
{
    public partial class VaccinBDContext : DbContext
    {
        public VaccinBDContext()
        {
        }

        public VaccinBDContext(DbContextOptions<VaccinBDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TypeVaccin> TypeVaccins { get; set; }
        public virtual DbSet<Vaccin> Vaccins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=VaccinBD;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Vaccin>(entity =>
            {
                entity.HasIndex(e => e.TypeVaccinId, "IX_Vaccins_TypeVaccinId");

                entity.Property(e => e.Nampatient).HasColumnName("NAMPatient");

                entity.HasOne(d => d.TypeVaccin)
                    .WithMany(p => p.Vaccins)
                    .HasForeignKey(d => d.TypeVaccinId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
