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
                .FirstOrDefaultAsync(a => a.IdAzienda == aziendaId);


            return AziendaViewModel.FromEntity(azienda);
        }


      }
}