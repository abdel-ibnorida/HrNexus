using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrNexus.Models.Entities;

namespace HrNexus.Models.Services.Application
{
    public interface IAuthService
    {
        //Task<User> CreaNuovoUtente(User user);
        Task<User> Accesso(User user);
    }
}