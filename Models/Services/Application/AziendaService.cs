using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;
using System.ComponentModel.DataAnnotations;
using MyApp.Data;
using HrNexus.Models.Entities;
using HrNexus.Models.ViewModels;




namespace HrNexus.MyApp.Models.Services.Application
{

    public class DipendenteService
    {
        private readonly MyDbContext _context;

        public DipendenteService(MyDbContext context)
        {
            _context = context;
        }


        public async Task<List<DipendenteViewModel>> ElencoLavoratoriById(int aziendaId)
        {
            return await _context.Dipendenti
                .Where(d => d.AziendaId == aziendaId)
                .ToListAsync();
        }

    }


}

