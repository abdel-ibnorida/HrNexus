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
        public DateTime? TimbraturaInizio { get; set; }
        public DateTime? TimbraturaUscita { get; set; }
        public bool GiornoFerie{ get; set; }
        public bool GiornoPermesso{ get; set; }
        public bool GiornoMalattia{ get; set; }

        public virtual Dipendente Dipendente { get; set; }
       public Programmazione(int idDipendente, DateTime dataGiorno, DateTime inizioTurno, DateTime fineTurno,
                              bool giornoFerie, bool giornoPermesso, bool giornoMalattia)
        {
            IdDipendente = idDipendente;
            DataGiorno = dataGiorno;
            InizioTurno = inizioTurno;
            FineTurno = fineTurno;
            GiornoFerie = giornoFerie;
            GiornoPermesso = giornoPermesso;
            GiornoMalattia = giornoMalattia;
        }
    }
}