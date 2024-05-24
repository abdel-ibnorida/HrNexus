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
        public IActionResult CalendarioLavoratori(DateTime? selectedDate)
        {
            var currentDate = selectedDate ?? DateTime.Now;
            var daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
            var firstDayOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var dayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            ViewBag.CurrentDate = currentDate;
            ViewBag.DaysInMonth = daysInMonth;
            ViewBag.FirstDayOfMonth = firstDayOfMonth;
            ViewBag.DayOfWeek = dayOfWeek-1;
            return View();
        }
        [HttpPost]
        public IActionResult ChangeMonth(int year, int month, string direction)
        {
            DateTime newDate;

            if (direction == "previous")
            {
                newDate = new DateTime(year, month, 1).AddMonths(-1);
            }
            else
            {
                newDate = new DateTime(year, month, 1).AddMonths(1);
            }

            var daysInMonth = DateTime.DaysInMonth(newDate.Year, newDate.Month);
            var firstDayOfMonth = new DateTime(newDate.Year, newDate.Month, 1);
            var dayOfWeek = (int)firstDayOfMonth.DayOfWeek;

            ViewBag.CurrentDate = newDate;
            ViewBag.DaysInMonth = daysInMonth;
            ViewBag.FirstDayOfMonth = firstDayOfMonth;
            ViewBag.DayOfWeek = dayOfWeek-1;

            return View("CalendarioLavoratori");
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