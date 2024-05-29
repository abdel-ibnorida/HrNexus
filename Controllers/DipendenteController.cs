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
    public class DipendenteController : Controller
    {
        private readonly IDipendenteService dipendenteService;
        public DipendenteController(IDipendenteService dipendenteService)
        {
            this.dipendenteService = dipendenteService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> CalendarioPersonale()
        {
            int idDipendente = Convert.ToInt32(HttpContext.Session.GetString("IdDipendente"));
            int idAzienda = Convert.ToInt32(HttpContext.Session.GetString("IdAzienda")); //recuperare l'id dell'azienda loggata
            int meseCorrente = DateTime.Now.Month;
            int annoCorrente = DateTime.Now.Year;
            Console.WriteLine(meseCorrente.ToString(), annoCorrente.ToString(), idDipendente.ToString(), idAzienda.ToString());
            DipendenteViewModel dipendente = await dipendenteService.ProgrammazioniLavoratore(idDipendente, idAzienda, meseCorrente, annoCorrente);
            return View(dipendente);
        }
        [HttpGet]
        public async Task<IActionResult> CalendarioPersonaleForm(int meseCorrente, int annoCorrente, string direzione)
        {
            int idDipendente = Convert.ToInt32(HttpContext.Session.GetString("IdDipendente"));
            int idAzienda = Convert.ToInt32(HttpContext.Session.GetString("IdAzienda")); //recuperare l'id dell'azienda loggata
            if (direzione == "successivo")
            {
                if (meseCorrente == 12)
                {
                    annoCorrente = annoCorrente + 1;
                    meseCorrente = 1;
                }
                else
                {
                    meseCorrente = meseCorrente + 1;
                }
                DipendenteViewModel dipendente = await dipendenteService.ProgrammazioniLavoratore(idDipendente, idAzienda, meseCorrente, annoCorrente);
                return View("CalendarioPersonale", dipendente);
            }
            else if (direzione == "precedente")
            {
                if (meseCorrente == 1)
                {
                    annoCorrente = annoCorrente - 1;
                    meseCorrente = 12;
                }
                else
                {
                    meseCorrente = meseCorrente - 1;
                }
                DipendenteViewModel dipendente = await dipendenteService.ProgrammazioniLavoratore(idDipendente, idAzienda, meseCorrente, annoCorrente);
                return View("CalendarioPersonale", dipendente);
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<ActionResult> GestisciTimbratura(int idProgrammazione, int giornoModale, int meseModale, int annoModale, string timbratura)
        {
            int idDipendente = Convert.ToInt32(HttpContext.Session.GetString("IdDipendente"));
            int idAzienda = Convert.ToInt32(HttpContext.Session.GetString("IdAzienda")); 

            DipendenteViewModel dipendente = await dipendenteService.GestisciTimbratura(idDipendente, idAzienda, idProgrammazione, giornoModale, meseModale,annoModale, timbratura);
            return View("CalendarioPersonale", dipendente);
        }
        public IActionResult RichiestaAssenze()
        {
            return View();
        }
    }
}