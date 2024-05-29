using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrNexus.Models.Entities;
using HrNexus.Models.ViewModels;

namespace HrNexus.Models.Services.Application
{
    public interface IAziendaService
    { 
        Task<DipendenteViewModel> GestisciProgrammazione(int idDipendente, int IdProgrammazione, int idAzienda, int giorno, int mese, int anno, string inizioTurno, string FineTurno, bool giornoFerie, bool giornoPermesso, bool giornoMalattia); 

        Task<DipendenteViewModel> GestisciRichiesta(int IdDipendente, string esitoRichiesta, string tipoRichiesta, int idRichiesta, int idAzienda); 
        Task<DipendenteViewModel> AggiungiGiorniFerie(int numeroGiorni, int idAzienda, int idDipendente);
         Task<DipendenteViewModel> AggiungiGiorniPermesso(int numeroGiorni, int idAzienda, int idDipendente);
        Task<DipendenteViewModel> ElencoRichieste(string nome, int idAzienda);
        Task<AziendaViewModel> ElencoLavoratoriById(int id);
        Task<DipendenteViewModel> ProgrammazioniLavoratoreByNome(string nome, int IdAzienda, int mese, int anno);
    }
}