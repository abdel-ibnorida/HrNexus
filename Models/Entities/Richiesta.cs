using System;
using System.Collections.Generic;
using HrNexus.Models.Entities;

namespace HrNexus.Models.Entities
{
    public partial class Richiesta
    {
        public int IdRichiesta { get; set; }
        public int IdDipendente { get; set; }
        public DateTime DataRichiesta { get; set; }
        public bool Confermato{ get; set; }
        public bool Archiviato{ get; set; }
        public string Tipo{ get; set; }
        public virtual Dipendente Dipendente { get; set; }
        public Richiesta()
        {
        }
        public Richiesta(DateTime dataRichiesta, int idDipendente, bool confermato, bool archiviato, string tipo) { 
            this.IdDipendente = idDipendente;
            this.DataRichiesta = dataRichiesta;
            this.Confermato = confermato;
            this.Archiviato = archiviato;
            this.Tipo = tipo;
        }
    }
}