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
        public IActionResult CalendarioLavoratori()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CalendarioLavoratori(string nome, int meseCorrente, int annoCorrente, string direzione)
        {

            if (direzione == null){
                int IdAzienda = Convert.ToInt32(HttpContext.Session.GetString("Id")); //recuperare l'id dell'azienda loggata
                DipendenteViewModel dipendente = await aziendaService.ProgrammazioniLavoratoreByNome(nome,IdAzienda,meseCorrente,annoCorrente);
                return View(dipendente);
            }else{
                if (direzione == "successivo"){
                    if(meseCorrente == 12)
                    {
                        annoCorrente = annoCorrente + 1;
                        meseCorrente = 1;
                    }
                    else{
                        meseCorrente = meseCorrente + 1;
                    }
                }
                if (direzione == "precedente"){
                    if(meseCorrente == 1)
                    {
                        annoCorrente = annoCorrente - 1;
                        meseCorrente = 12;
                    }
                    else{
                        meseCorrente = meseCorrente - 1;
                    }
                }
                int IdAzienda = Convert.ToInt32(HttpContext.Session.GetString("Id")); //recuperare l'id dell'azienda loggata
                DipendenteViewModel dipendente = await aziendaService.ProgrammazioniLavoratoreByNome(nome,IdAzienda,meseCorrente,annoCorrente);
                 return View(dipendente);
            }
        }

        public IActionResult GestioneAssenze()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GestioneAssenze(string nome)
        {  
            int IdAzienda = Convert.ToInt32(HttpContext.Session.GetString("Id")); //recuperare l'id dell'azienda loggata
            DipendenteViewModel dipendente = await aziendaService.ElencoRichieste(nome, IdAzienda);
            return View(dipendente);
        }
        public async Task<IActionResult> AggiungiGiorniFerie(int numeroGiorni, int IdDipendente)
        {  
            int IdAzienda = Convert.ToInt32(HttpContext.Session.GetString("Id")); //recuperare l'id dell'azienda loggata
            DipendenteViewModel dipendente = await aziendaService.AggiungiGiorniFerie(numeroGiorni, IdAzienda, IdDipendente);
            return View("GestioneAssenze",dipendente);
        }
        public async Task<IActionResult> AggiungiGiorniPermesso(int numeroGiorni, int IdDipendente)
        {  
            int IdAzienda = Convert.ToInt32(HttpContext.Session.GetString("Id")); //recuperare l'id dell'azienda loggata
            DipendenteViewModel dipendente = await aziendaService.AggiungiGiorniPermesso(numeroGiorni, IdAzienda, IdDipendente);
            return View("GestioneAssenze", dipendente);
        }
        public async Task<IActionResult> GestisciRichiesta(int idDipendente, string esitoRichiesta, string tipoRichiesta, int idRichiesta)
        {  
            int idAzienda = Convert.ToInt32(HttpContext.Session.GetString("Id")); //recuperare l'id dell'azienda loggata
            DipendenteViewModel dipendente = await aziendaService.GestisciRichiesta(idDipendente, esitoRichiesta, tipoRichiesta, idRichiesta, idAzienda);
            return View("GestioneAssenze", dipendente);
        }
        public IActionResult GestioneValutazioni()
        {
            return View();
        }

    }
}