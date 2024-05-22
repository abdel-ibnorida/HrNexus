using System;
using System.Collections.Generic;
using HrNexus.Models.Entities;

namespace HrNexus.Models.Entities
{
    public partial class Azienda : User
    {
       public Azienda()
        {
        }

        public int IdAzienda { get; set; }
        public string PIva { get; set; }
        public DateTime DataNascita { get; set; }
        public string Email { get; set; }
    
    

        public virtual ICollection<Dipendente> Dipendenti { get; set; }

      
    }
}