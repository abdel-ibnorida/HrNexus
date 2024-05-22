using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrNexus.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HrNexus.Models.Services.Application
{
    public class EfCoreAuthService : IAuthService
    {
        private readonly MyDbContext dbContext;

        public EfCoreAuthService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<User> Accesso(User user)
        {
            Console.WriteLine("Username recuperato");
            bool findPassword = false;
            bool findUserAzienda = await dbContext.Aziende.AnyAsync(a => a.Username == user.Username);
            bool findUserDipendete = await dbContext.Dipendenti.AnyAsync(d => d.Username == user.Username);
            if (findUserAzienda == true)
            {
                findPassword = await dbContext.Aziende.AnyAsync(a => a.Username == user.Username && a.Password == user.Password);
                Azienda azienda = await dbContext.Aziende
                .SingleOrDefaultAsync(a => a.Username == user.Username && a.Password == user.Password);
                return azienda;
            }
            if (findUserDipendete == true)
            {
                findPassword = await dbContext.Dipendenti.AnyAsync(a => a.Username == user.Username && a.Password == user.Password);
                Dipendente dipendente = await dbContext.Dipendenti
                .SingleOrDefaultAsync(d => d.Username == user.Username && d.Password == user.Password);
                return dipendente;
            }
            User userNotFound = new User();
            return userNotFound;
        }
    }
}