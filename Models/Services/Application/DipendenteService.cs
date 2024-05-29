using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrNexus.Models.Entities;
using HrNexus.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HrNexus.Models.Services.Application
{
    public class DipendenteService : IDipendenteService
    {
        private readonly MyDbContext dbContext;

        public DipendenteService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<DipendenteViewModel> ProgrammazioniLavoratore(int idDipendente, int idAzienda, int mese, int anno)
        {

            Dipendente dipendente = await dbContext.Dipendenti
                .Where(d => d.IdDipendente == idDipendente && d.IdAzienda == idAzienda)
                .FirstOrDefaultAsync();

            if (dipendente == null)
            {
                DipendenteViewModel modelVuoto = new DipendenteViewModel();
                modelVuoto.DipendenteTrovato = false;
                return modelVuoto;
            }
            else
            {
                dipendente.Programmazioni = await dbContext.Programmazioni
                    .Where(p => p.IdDipendente == dipendente.IdDipendente && p.DataGiorno.Year == anno && p.DataGiorno.Month == mese)
                    .Select(p => new Programmazione
                    {
                        IdProgrammazione = p.IdProgrammazione,
                        IdDipendente = p.IdDipendente,
                        DataGiorno = p.DataGiorno,
                        InizioTurno = p.InizioTurno,
                        FineTurno = p.FineTurno,
                        TimbraturaInizio = p.TimbraturaInizio.HasValue ? p.TimbraturaInizio.Value : DateTime.MinValue,
                        TimbraturaUscita = p.TimbraturaUscita.HasValue ? p.TimbraturaUscita.Value : DateTime.MinValue,
                        GiornoFerie = p.GiornoFerie,
                        GiornoMalattia = p.GiornoMalattia,
                        GiornoPermesso = p.GiornoPermesso,
                    })
                    .ToListAsync();
                return DipendenteViewModel.FromEntity(dipendente, mese, anno);
            }
        }
    }
}