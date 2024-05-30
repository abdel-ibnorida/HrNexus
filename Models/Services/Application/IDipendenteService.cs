using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrNexus.Models.Entities;
using HrNexus.Models.ViewModels;

namespace HrNexus.Models.Services.Application
{
    public interface IDipendenteService
    { 
        Task<DipendenteViewModel> ProgrammazioniLavoratore(int idDipendente,int idAzienda, int mese, int anno);
        Task<DipendenteViewModel> GestisciTimbratura(int idDipendente, int idAzienda,int idProgrammazione, int giorno, int mese, int anno, string timbratura);
        Task<DipendenteViewModel> RichiesteDipendente(int idDipendente, int idAzienda);
         Task<DipendenteViewModel> InviaRichiesta(int idDipendente, int idAzienda,string dataRichiesta, string sceltaTipo);
    }
}