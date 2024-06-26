using System;
using System.Collections.Generic;
using HrNexus.Models.Entities;

namespace HrNexus.Models.Entities
{
    public partial class Dipendente : User
    {
        public Dipendente()
        {
        }
        
        public int IdDipendente { get; set; }
        public int IdAzienda { get; set; }
        public int GiorniDiFerie { get; set; }
        public int GiorniDiPermesso { get; set; }

        public string Cognome { get; set; }
        public string Valutazione { get; set; }
        public DateTime DataNascita { get; set; }
        public string Email { get; set; }
        public decimal Stipendio { get; set; }

        public virtual Azienda Azienda { get; set; }
        public virtual ICollection<Programmazione> Programmazioni { get; set; }
        public virtual ICollection<Richiesta> Richieste { get; set; }
    }
}