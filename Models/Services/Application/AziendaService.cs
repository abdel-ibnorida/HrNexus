using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using HrNexus.Models.Entities;
using HrNexus.Models.ViewModels;
using HrNexus.Models.Services.Application;




namespace HrNexus.Models.Services.Application
{

    public class AziendaService : IAziendaService
    {
        private readonly MyDbContext _context;

        public AziendaService(MyDbContext context)
        {
            _context = context;
        }


        public async Task<AziendaViewModel> ElencoLavoratoriById(int aziendaId)
        {
            Azienda azienda = await _context.Aziende
                .Include(a => a.Dipendenti)
                .FirstOrDefaultAsync(a => a.IdAzienda == aziendaId);


            return AziendaViewModel.FromEntity(azienda);
        }

    }


}

