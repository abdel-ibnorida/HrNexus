using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HrNexus.Models.Services.Application;
using HrNexus.Models.Entities;


namespace HrNexus.Controllers
{
    public class DipendenteController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CalendarioPersonale()
        {
            return View();
        }
         public IActionResult GestioneAssenze()
        {
            return View();
        }


    }
}