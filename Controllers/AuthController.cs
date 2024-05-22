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
                    model.Username = user.Username;
                    model.Nome = user.Nome;
                    accessor.HttpContext.Session.SetString("Username", user.Username);
                    accessor.HttpContext.Session.SetString("Nome", user.Nome);
                    accessor.HttpContext.Session.SetString("TipoUtente", "azienda");
                    return View(model);
                }
                else if (user is Dipendente dipendente)
                {
                    DipendenteViewModel model = new DipendenteViewModel();
                    model.Username = user.Username;
                    model.Nome = user.Nome;
                    accessor.HttpContext.Session.SetString("Username", user.Username);
                    accessor.HttpContext.Session.SetString("Nome", user.Nome);
                    accessor.HttpContext.Session.SetString("TipoUtente", "dipendente");
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