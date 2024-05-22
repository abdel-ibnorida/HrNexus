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
        
        Task<AziendaViewModel> ElencoLavoratoriById(int id);
    }
}