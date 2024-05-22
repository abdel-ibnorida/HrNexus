using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HrNexus.Controllers
{
    public class CalendarioLavoratoriController : Controller
    {
         public CalendarioLavoratoriController ()

    {

    }
         public IActionResult Index()
        {          
            return View();
        }
        
        public IActionResult Error()
        {
            return View("Error!");
        }   
    }
}