using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrNexus.Models.Entities;
using HrNexus.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HrNexus.Models.Services.Application
{
    public class AziendaService : IAziendaService
    {
        private readonly MyDbContext dbContext;

        public AziendaService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AziendaViewModel> ElencoLavoratoriById(int aziendaId)
        {
            Azienda azienda = await dbContext.Aziende
                .Include(a => a.Dipendenti)
                .FirstOrDefaultAsync(d => d.IdAzienda == aziendaId);


            return AziendaViewModel.FromEntity(azienda);
        }
        public async Task<DipendenteViewModel> ProgrammazioniLavoratoreByNome(string nome, int idAzienda, int mese, int anno)
        {


            Dipendente dipendente = await dbContext.Dipendenti
                .Where(d => d.Nome == nome && d.IdAzienda == idAzienda)
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
                .ToListAsync();
                return DipendenteViewModel.FromEntity(dipendente, mese, anno);
            }
        }
    }
}