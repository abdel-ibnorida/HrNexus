using System.Collections.Generic;
using System.Linq;
using System.Data;


namespace HrNexus.Models.ViewModels
{

    public class DipendenteViewModel : UserViewModel
    {
        public int IdDipendente { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }
        public decimal Stipendio { get; set; }


    }
}