using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;

namespace HrNexus.Models.ViewModels
{

    public class ProgrammazioneViewModel
    {
        public int IdProgrammazione { get; set; }
        public int Mese { get; set; }
        public int Anno { get; set; }
        public int IdDipendente { get; set; }
        public DateTime DataGiorno { get; set; }
        public DateTime InizioTurno { get; set; }
        public DateTime FineTurno { get; set; }
        public DateTime TimbraturaInizio { get; set; }
        public DateTime TimbraturaFine { get; set; }
        public bool GiornoFerie{ get; set; }
        public bool GiornoPermesso{ get; set; }
        public bool GiornoMalattia{ get; set; }
        
    }
}