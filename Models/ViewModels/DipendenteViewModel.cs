using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace HrNexus.Models.ViewModels
{

    public class DipendenteViewModel : UserViewModel
    {
        public string Cognome { get; set; }
        public string Email { get; set; }
        public decimal Stipendio { get; set; }


    }
}