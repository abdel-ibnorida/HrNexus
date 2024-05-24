using System.Collections.Generic;
using System.Linq;
using System.Data;
using HrNexus.Models.Entities;


namespace HrNexus.Models.ViewModels
{

    public class DipendenteViewModel : UserViewModel
    {
        public int IdDipendente { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public decimal Stipendio { get; set; }
        public bool DipendenteTrovato { get; set; }
        public int MeseProgrammazione { get; set; }
        public int AnnoProgrammazione { get; set; }
        public ICollection<ProgrammazioniMensiliViewModel> Programmazioni { get; set; }
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
                AnnoProgrammazione = anno,
                MeseProgrammazione = mese,
                DipendenteTrovato = true,
                Programmazioni = dipendente.Programmazioni != null
                    ? dipendente.Programmazioni.Select(p => new ProgrammazioniMensiliViewModel
                    {
                        IdProgrammazione = p.IdProgrammazione,
                        DataGiorno = p.DataGiorno,
                        InizioTurno = p.InizioTurno,
                        FineTurno = p.FineTurno,
                        GiornoFerie = p.GiornoFerie,
                        GiornoPermesso = p.GiornoPermesso,
                        GiornoMalattia = p.GiornoMalattia,
                        Mese = mese,
                        Anno = anno,
                    }).ToList()
                    : new List<ProgrammazioniMensiliViewModel>()
            };
        }

    }
}