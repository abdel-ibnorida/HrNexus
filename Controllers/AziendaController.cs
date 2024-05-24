using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using HrNexus.Models.Services.Application;
using HrNexus.Models.Entities;
using HrNexus.Models.ViewModels;


namespace HrNexus.Controllers
{
    public class AziendaController : Controller
    {
        private readonly IAziendaService aziendaService;
        public AziendaController(IAziendaService aziendaService)
        {
            this.aziendaService = aziendaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ElencoLavoratori()
        {
            int IdAzienda = Convert.ToInt32(HttpContext.Session.GetString("Id")); //recuperare l'id dell'azienda loggata
            AziendaViewModel azienda = await aziendaService.ElencoLavoratoriById(IdAzienda);
            return View(azienda);
        }
        public async Task<IActionResult> CalendarioLavoratori()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CalendarioLavoratori(string nome, int meseCorrente, int annoCorrente)
        {
            int IdAzienda = Convert.ToInt32(HttpContext.Session.GetString("Id")); //recuperare l'id dell'azienda loggata
            DipendenteViewModel dipendente = await aziendaService.ProgrammazioniLavoratoreByNome(nome,IdAzienda,meseCorrente,annoCorrente);
            return View(dipendente);
        }

        public IActionResult GestioneAssenze()
        {
            return View();
        }
        public IActionResult GestioneValutazioni()
        {
            return View();
        }

    }
}