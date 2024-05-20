using System;
using System.Collections.Generic;
using HrNexus.Models.Entities;

namespace HrNexus.Models.Entities
{
    public partial class Azienda
    {
       public Azienda()
        {
        }

        public int IdAzienda { get; set; }
        public string PIva { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string Password { get; set; }
        public DateTime DataNascita { get; set; }
        public string Email { get; set; }
    
    

        public virtual ICollection<Dipendente> Dipendenti { get; set; }

      
    }
}