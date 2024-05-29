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
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        IHttpContextAccessor accessor;
        public AuthController(IAuthService authService, IHttpContextAccessor accessor)
        {
            this.authService = authService;
            this.accessor = accessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login(string username, string password)
        {
            User userInput = new User(username, password);
            User user = await authService.Accesso(userInput);

            if (user != null)
            {
                if (user is Azienda azienda)
                {
                    AziendaViewModel model = new AziendaViewModel();
                    model.Username = azienda.Username;
                    model.Nome = azienda.Nome;
                    model.IdAzienda = azienda.IdAzienda;
                    accessor.HttpContext.Session.SetString("Username", azienda.Username); 
                    accessor.HttpContext.Session.SetString("Nome", azienda.Nome);
                    accessor.HttpContext.Session.SetString("TipoUtente", "azienda");
                    accessor.HttpContext.Session.SetString("IdAzienda", azienda.IdAzienda.ToString());

                    return View(model);
                }
                else if (user is Dipendente dipendente)
                {
                    DipendenteViewModel model = new DipendenteViewModel();
                    model.Username = dipendente.Username;
                    model.Nome = dipendente.Nome;
                    model.IdDipendente = dipendente.IdDipendente;
                    accessor.HttpContext.Session.SetString("Username", dipendente.Username);
                    accessor.HttpContext.Session.SetString("Nome", dipendente.Nome);
                    accessor.HttpContext.Session.SetString("TipoUtente", "dipendente");
                    accessor.HttpContext.Session.SetString("IdDipendente", dipendente.IdDipendente.ToString());
                    accessor.HttpContext.Session.SetString("IdAzienda", dipendente.IdAzienda.ToString());
                    return View(model);
                }
            }
            else
            {
                return Content("login fallito");
            }
            return View();
        }
    }
}