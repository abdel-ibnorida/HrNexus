using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;

namespace HrNexus.Models.ViewModels
{

    public class RichiestaViewModel
    {
        public int IdRichiesta { get; set; }
        public DateTime DataRichiesta { get; set; }
        public bool Confermato{ get; set; }
        public bool Archiviato{ get; set; }
        public string Tipo{ get; set; }
    }
}