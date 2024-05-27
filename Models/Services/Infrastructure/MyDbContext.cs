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
        public virtual DbSet<Programmazione> Programmazioni { get; set; }
        public virtual DbSet<Richiesta> Richieste { get; set; }

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
                entity.ToTable("AZIENDA");

              
                entity.Property(a => a.IdAzienda).HasColumnName("ID_AZIENDA");
                entity.Property(a => a.Username).HasColumnName("USERNAME");
                entity.Property(a => a.Nome).HasColumnName("NOME");
                entity.Property(a => a.PIva).HasColumnName("P_IVA");
                entity.Property(a => a.Password).HasColumnName("PASSWORD");
                entity.Property(a => a.DataNascita).HasColumnName("DATA_NASCITA");
                entity.Property(a => a.Email).HasColumnName("EMAIL");

                entity.HasKey(azienda => azienda.IdAzienda);
                //Andiamo a settare la relazione 1 a n tra l'entità Azienda e l'entità Dipendente
                entity.HasMany(azienda => azienda.Dipendenti).WithOne(dipendente => dipendente.Azienda).HasForeignKey(dipendente => dipendente.IdAzienda);
            });


            modelBuilder.Entity<Dipendente>(entity =>
            {
                entity.ToTable("DIPENDENTI");
               
                entity.Property(d => d.IdAzienda).HasColumnName("ID_AZIENDA");
                entity.Property(d => d.IdDipendente).HasColumnName("ID_DIPENDENTE");
                entity.Property(d => d.Username).HasColumnName("USERNAME");
                entity.Property(d => d.Nome).HasColumnName("NOME");
                entity.Property(d => d.Cognome).HasColumnName("COGNOME");
                entity.Property(d => d.Valutazione).HasColumnName("VALUTAZIONE");
                entity.Property(d => d.Password).HasColumnName("PASSWORD");
                entity.Property(d => d.DataNascita).HasColumnName("DATA_NASCITA");
                entity.Property(d => d.Email).HasColumnName("EMAIL");
                entity.Property(d => d.Stipendio).HasColumnName("STIPENDIO");
                entity.Property(d => d.GiorniDiFerie).HasColumnName("GIORNI_DI_FERIE");
                entity.Property(d => d.GiorniDiPermesso).HasColumnName("GIORNI_DI_PERMESSO");

                entity.HasKey(dipendente => dipendente.IdDipendente);
                //andiamo a settare la relazione n a 1 tra l'entity Dipendente e l'entity Azienda
                entity.HasOne(dipendente => dipendente.Azienda).WithMany(azienda => azienda.Dipendenti);
                entity.HasMany(dipendente => dipendente.Programmazioni).WithOne(programmazione => programmazione.Dipendente).HasForeignKey(programmazione => programmazione.IdDipendente);
                entity.HasMany(dipendente => dipendente.Richieste).WithOne(richiesta => richiesta.Dipendente).HasForeignKey(richiesta => richiesta.IdDipendente);
           
            });

            modelBuilder.Entity<Programmazione>(entity =>{
                entity.ToTable("PROGRAMMAZIONI");

                entity.Property(p => p.IdProgrammazione).HasColumnName("ID_PROGRAMMAZIONE");
                entity.Property(p => p.IdDipendente).HasColumnName("ID_DIPENDENTE");
                entity.Property(p => p.DataGiorno).HasColumnName("DATA_GIORNO");
                entity.Property(p => p.InizioTurno).HasColumnName("INIZIO_TURNO");
                entity.Property(p => p.FineTurno).HasColumnName("FINE_TURNO");
                entity.Property(p => p.TimbraturaInizio).HasColumnName("TIMBRATURA_INIZIO");
                entity.Property(p => p.TimbraturaFine).HasColumnName("TIMBRATURA_USCITA");
                entity.Property(p => p.GiornoFerie).HasColumnName("GIORNO_DI_FERIE");
                entity.Property(p => p.GiornoPermesso).HasColumnName("GIORNO_PERMESSO");
                entity.Property(p => p.GiornoMalattia).HasColumnName("GIORNO_MALATTIA");

                entity.HasKey(p => p.IdProgrammazione);

                entity.HasOne(p => p.Dipendente).WithMany(d => d.Programmazioni);
            });

            modelBuilder.Entity<Richiesta>(entity =>{
                entity.ToTable("RICHIESTE");

                entity.Property(r=> r.IdRichiesta).HasColumnName("ID_RICHIESTA");
                entity.Property(r=> r.IdDipendente).HasColumnName("ID_DIPENDENTE");
                entity.Property(r=> r.Confermato).HasColumnName("CONFERMATO");
                entity.Property(r=> r.Archiviato).HasColumnName("ARCHIVIATO");
                entity.Property(r=> r.DataRichiesta).HasColumnName("DATA_RICHIESTA");
                entity.Property(r=> r.Tipo).HasColumnName("TIPO");


                entity.HasKey(p => p.IdRichiesta);

                entity.HasOne(p => p.Dipendente).WithMany(d => d.Richieste);
            });

        }
    }
}