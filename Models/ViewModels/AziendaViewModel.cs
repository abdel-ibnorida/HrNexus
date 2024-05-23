using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using HrNexus.Models.Entities;


namespace HrNexus.Models.ViewModels
{

    public class AziendaViewModel : UserViewModel
    {
        
        public ICollection<DipendenteViewModel> Dipendenti { get; set; }
        public static AziendaViewModel FromEntity(Azienda azienda)
        {
            return new AziendaViewModel
            {

                Dipendenti = azienda.Dipendenti.Select(d => new DipendenteViewModel
                {
                    Username = d.Username,
                    Id = d.IdDipendente,
                    Nome = d.Nome,
                    Cognome = d.Cognome,
                    Email = d.Email,
                    Stipendio = d.Stipendio,
                }).ToList()
            };
        }
    }
}