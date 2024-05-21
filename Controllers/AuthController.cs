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
    public class AuthController : Controller
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        public IActionResult Index()
        {
            return Content("home");
        }

        public async Task<IActionResult> Login()
        {
            User user = new User("Mario44", "password2");
            bool logged = await authService.Accesso(user);
            if (logged)
            {
                return Content("fatto");
            }
            else
            {
                return Content("fallito");
            }
        }
    }
}