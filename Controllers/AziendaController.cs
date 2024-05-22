using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            return Content("home aziende");
        }

        public IActionResult ElencoLavoratori()
        {
            int IdAzienda = 1 ;
            /*AziendaViewModel azienda =  await aziendaService.ElencoLavoratoriById(IdAzienda);*/

            return Content("c" + IdAzienda.ToString() + "  ciaoo");
        }
        public IActionResult CalendarioLavoratori()
        {
            return View();
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