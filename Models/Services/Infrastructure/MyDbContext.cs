using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HrNexus.Models.Entities;

namespace HrNexus.Models.Entities
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Azienda> Aziende { get; set; }
        public virtual DbSet<Dipendente> Dipendenti { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Data/MyDb.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Azienda>(entity =>
            {
                entity.ToTable("AZIENDE");

              
                entity.Property(a => a.IdAzienda).HasColumnName("ID_AZIENDA");
                entity.Property(a => a.Username).HasColumnName("USERNAME");
                entity.Property(a => a.Nome).HasColumnName("NOME");
                entity.Property(a => a.PIva).HasColumnName("P_IVA");
                entity.Property(a => a.Password).HasColumnName("PASSWORD");
                entity.Property(a => a.DataNascita).HasColumnName("DATANASCITA");
                entity.Property(a => a.Email).HasColumnName("EMAIL");

                entity.HasKey(azienda => azienda.IdAzienda);
                //Andiamo a settare la relazione 1 a n tra l'entità Azienda e l'entità Dipendente
                entity.HasMany(azienda => azienda.Dipendenti).WithOne(dipendente => dipendente.Azienda).HasForeignKey(dipendente => dipendente.IdAzienda);
            });


            modelBuilder.Entity<Dipendente>(entity =>
            {
                entity.ToTable("DIPENDENTI");
               
                entity.Property(d => d.IdAzienda).HasColumnName("ID_AZIENDA");
                entity.Property(d => d.IdDipendente).HasColumnName("Id_DIPENDENTE");
                entity.Property(d => d.Username).HasColumnName("USERNAME");
                entity.Property(d => d.Nome).HasColumnName("NOME");
                entity.Property(d => d.Cognome).HasColumnName("COGNOME");
                entity.Property(d => d.Valutazione).HasColumnName("VALUTAZIONE");
                entity.Property(d => d.Password).HasColumnName("PASSWORD");
                entity.Property(d => d.DataNascita).HasColumnName("DATANASCITA");
                entity.Property(d => d.Email).HasColumnName("EMAIL");
                entity.Property(d => d.Stipendio).HasColumnName("STIPENDIO");

                entity.HasKey(dipendente => dipendente.IdDipendente);
                //andiamo a settare la relazione n a 1 tra l'entity Dipendente e l'entity Azienda
                entity.HasOne(dipendente => dipendente.Azienda).WithMany(azienda => azienda.Dipendenti);
            });

        }
    }
}