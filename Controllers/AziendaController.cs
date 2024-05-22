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
       
        AziendaService aziendaService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ElencoLavoratori()
        {
            string IdAzienda = 1 ;


            List<DipendenteViewModel>lista =  aziendaService.ElencoLavoratoriById(IdAzienda);
            return View(lista);


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