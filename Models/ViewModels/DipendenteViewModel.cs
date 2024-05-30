using System.Collections.Generic;
using System.Linq;
using System.Data;
using HrNexus.Models.Entities;


namespace HrNexus.Models.ViewModels
{

    public class DipendenteViewModel : UserViewModel
    {
        public int IdDipendente { get; set; }
        public int IdAzienda { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public decimal Stipendio { get; set; }
         public string Valutazione { get; set; }
        public bool DipendenteTrovato { get; set; }
        public int MeseProgrammazione { get; set; }
        public int AnnoProgrammazione { get; set; }
        public int GiorniDiFerie { get; set; }
        public int GiorniDiPermesso { get; set; }
        public ProgrammazioneViewModel ProgrammazioneSelezionata { get; set; }
        public ICollection<ProgrammazioneViewModel> Programmazioni { get; set; }
        
        public ICollection<RichiestaViewModel> Richieste { get; set; }
        public static DipendenteViewModel FromEntity(Dipendente dipendente, int mese, int anno)
        {
            if (dipendente == null)
            {
                return new DipendenteViewModel
                {
                    DipendenteTrovato = false
                };
            }

            return new DipendenteViewModel
            {
                IdDipendente = dipendente.IdDipendente,
                IdAzienda = dipendente.IdAzienda,
                Nome = dipendente.Nome,
                AnnoProgrammazione = anno,
                MeseProgrammazione = mese,
                DipendenteTrovato = true,
                Programmazioni = dipendente.Programmazioni != null
                    ? dipendente.Programmazioni.Select(p => new ProgrammazioneViewModel
                    {
                        IdProgrammazione = p.IdProgrammazione,
                        DataGiorno = p.DataGiorno,
                        InizioTurno = p.InizioTurno,
                        FineTurno = p.FineTurno,
                        TimbraturaInizio = p.TimbraturaInizio,
                        TimbraturaUscita = p.TimbraturaUscita,
                        GiornoFerie = p.GiornoFerie,
                        GiornoPermesso = p.GiornoPermesso,
                        GiornoMalattia = p.GiornoMalattia,
                        Mese = mese,
                        Anno = anno,
                        IdDipendente = dipendente.IdDipendente,
                    }).ToList()
                    : new List<ProgrammazioneViewModel>()
                
            };
        }
        public static DipendenteViewModel FromEntity(Dipendente dipendente)
        {
            if (dipendente == null)
            {
                return new DipendenteViewModel
                {
                    DipendenteTrovato = false
                };
            }

            return new DipendenteViewModel
            {
                IdDipendente = dipendente.IdDipendente,
                Nome = dipendente.Nome,
                DipendenteTrovato = true,
                GiorniDiFerie = dipendente.GiorniDiFerie,
                GiorniDiPermesso = dipendente.GiorniDiPermesso,

                Richieste = dipendente.Richieste != null
                    ? dipendente.Richieste.Select(r => new RichiestaViewModel
                    {
                        IdRichiesta = r.IdRichiesta,
                        DataRichiesta = r.DataRichiesta,
                        Confermato = r.Confermato,
                        Archiviato = r.Archiviato,
                        Tipo = r.Tipo,
                    }).ToList()
                    : new List<RichiestaViewModel>()
                
            };
        }

    }
}