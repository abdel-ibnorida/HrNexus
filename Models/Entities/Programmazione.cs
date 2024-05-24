using System;
using System.Collections.Generic;
using HrNexus.Models.Entities;

namespace HrNexus.Models.Entities
{
    public partial class Programmazione
    {
       public Programmazione()
        {
        }

        public int IdProgrammazione { get; set; }
        public int IdDipendente { get; set; }
        public DateTime DataGiorno { get; set; }
        public DateTime InizioTurno { get; set; }
        public DateTime FineTurno { get; set; }
        public bool GiornoFerie{ get; set; }
        public bool GiornoPermesso{ get; set; }
        public bool GiornoMalattia{ get; set; }

        public virtual Dipendente Dipendente { get; set; }
      
    }
}