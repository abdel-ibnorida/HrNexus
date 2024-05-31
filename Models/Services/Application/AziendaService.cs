using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrNexus.Models.Entities;
using HrNexus.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HrNexus.Models.Services.Application
{
    public class AziendaService : IAziendaService
    {
        private readonly MyDbContext dbContext;

        public AziendaService(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<AziendaViewModel> ElencoLavoratoriById(int aziendaId)
        {
            Azienda azienda = await dbContext.Aziende
                .Include(a => a.Dipendenti)
                .FirstOrDefaultAsync(d => d.IdAzienda == aziendaId);


            return AziendaViewModel.FromEntity(azienda);
        }
        public async Task<DipendenteViewModel> ProgrammazioniLavoratoreByNome(string nome, int idAzienda, int mese, int anno)
        {


            Dipendente dipendente = await dbContext.Dipendenti
                .Where(d => d.Nome == nome && d.IdAzienda == idAzienda)
                .FirstOrDefaultAsync();


            if (dipendente == null)
            {
                DipendenteViewModel modelVuoto = new DipendenteViewModel();
                modelVuoto.DipendenteTrovato = false;
                return modelVuoto;
            }
            else
            {
                /*dipendente.Programmazioni = await dbContext.Programmazioni
                .Where(p => p.IdDipendente == dipendente.IdDipendente && p.DataGiorno.Year == anno && p.DataGiorno.Month == mese)
                .ToListAsync();*/
                dipendente.Programmazioni = await dbContext.Programmazioni
                    .Where(p => p.IdDipendente == dipendente.IdDipendente && p.DataGiorno.Year == anno && p.DataGiorno.Month == mese)
                    .Select(p => new Programmazione
                    {
                        IdProgrammazione = p.IdProgrammazione,
                        IdDipendente = p.IdDipendente,
                        DataGiorno = p.DataGiorno,
                        InizioTurno = p.InizioTurno,
                        FineTurno = p.FineTurno,
                        TimbraturaInizio = p.TimbraturaInizio.HasValue ? p.TimbraturaInizio.Value : DateTime.MinValue,
                        TimbraturaUscita = p.TimbraturaUscita.HasValue ? p.TimbraturaUscita.Value : DateTime.MinValue,
                        GiornoFerie = p.GiornoFerie,
                        GiornoMalattia = p.GiornoMalattia,
                        GiornoPermesso = p.GiornoPermesso,
                    })
                    .ToListAsync();
                return DipendenteViewModel.FromEntity(dipendente, mese, anno);
            }
        }
        public async Task<DipendenteViewModel> ElencoRichieste(string nome, int idAzienda)
        {
            Dipendente dipendente = await dbContext.Dipendenti
                .Where(d => d.Nome == nome && d.IdAzienda == idAzienda)
                .FirstOrDefaultAsync();


            if (dipendente == null)
            {
                DipendenteViewModel modelVuoto = new DipendenteViewModel();
                modelVuoto.DipendenteTrovato = false;
                return modelVuoto;
            }
            else
            {
                dipendente.Richieste = await dbContext.Richieste
                .Where(r => r.IdDipendente == dipendente.IdDipendente)
                .ToListAsync();
                return DipendenteViewModel.FromEntity(dipendente);
            }
        }
        public async Task<DipendenteViewModel> AggiungiGiorniFerie(int numeroGiorni, int idAzienda, int IdDipendente)
        {
            Dipendente dipendente = await dbContext.Dipendenti
                .Where(d => d.IdDipendente == IdDipendente && d.IdAzienda == idAzienda)
                .FirstOrDefaultAsync();


            dipendente.GiorniDiFerie = dipendente.GiorniDiFerie + numeroGiorni;
            await dbContext.SaveChangesAsync();
            dipendente.Richieste = await dbContext.Richieste
                .Where(r => r.IdDipendente == dipendente.IdDipendente)
                .ToListAsync();
            return DipendenteViewModel.FromEntity(dipendente);
        }
        public async Task<DipendenteViewModel> AggiungiGiorniPermesso(int numeroGiorni, int idAzienda, int IdDipendente)
        {
            Dipendente dipendente = await dbContext.Dipendenti
                .Where(d => d.IdDipendente == IdDipendente && d.IdAzienda == idAzienda)
                .FirstOrDefaultAsync();


            dipendente.GiorniDiPermesso = dipendente.GiorniDiPermesso + numeroGiorni;
            await dbContext.SaveChangesAsync();
            dipendente.Richieste = await dbContext.Richieste
                .Where(r => r.IdDipendente == dipendente.IdDipendente)
                .ToListAsync();
            return DipendenteViewModel.FromEntity(dipendente);
        }
        public async Task<DipendenteViewModel> GestisciRichiesta(int idDipendente, string esitoRichiesta, string tipoRichiesta, int idRichiesta, int idAzienda)
        {
            Dipendente dipendente = await dbContext.Dipendenti
                .Where(d => d.IdDipendente == idDipendente && d.IdAzienda == idAzienda)
                .FirstOrDefaultAsync();

            if (tipoRichiesta == "FERIE")
            {
                Console.WriteLine("siamo nella scelta ferie");
                if (esitoRichiesta == "accettata")
                {
                    dipendente.GiorniDiFerie = dipendente.GiorniDiFerie - 1;
                    Richiesta richiesta = await dbContext.Richieste
                       .Where(r => r.IdRichiesta == idRichiesta && r.IdDipendente == idDipendente)
                       .FirstOrDefaultAsync();
                    richiesta.Archiviato = true;
                    richiesta.Confermato = true;
                    Programmazione programmazione = await dbContext.Programmazioni
                       .Where(p => p.DataGiorno.Date == richiesta.DataRichiesta.Date && p.IdDipendente == idDipendente)
                       .FirstOrDefaultAsync();
                    if (programmazione == null)
                    {
                        Programmazione nuovaProgrammazione = new Programmazione
                        {
                            DataGiorno = richiesta.DataRichiesta,
                            InizioTurno = richiesta.DataRichiesta,
                            FineTurno = richiesta.DataRichiesta,
                            IdDipendente = dipendente.IdDipendente,
                            GiornoFerie = true
                        };
                        dbContext.Programmazioni.Add(nuovaProgrammazione);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        programmazione.GiornoFerie = true;
                    }
                    dbContext.SaveChanges();
                }
            }
            else if (tipoRichiesta == "PERMESSO")
            {
                Console.WriteLine("siamo nella scelta permesso");
                if (esitoRichiesta == "accettata")
                {
                    dipendente.GiorniDiPermesso = dipendente.GiorniDiPermesso - 1;
                    Richiesta richiesta = await dbContext.Richieste
                       .Where(r => r.IdRichiesta == idRichiesta && r.IdDipendente == idDipendente)
                       .FirstOrDefaultAsync();
                    richiesta.Archiviato = true;
                    richiesta.Confermato = true;
                    richiesta.Tipo = "permesso";
                    Programmazione programmazione = await dbContext.Programmazioni
                       .Where(p => p.DataGiorno.Date == richiesta.DataRichiesta.Date && p.IdDipendente == idDipendente)
                       .FirstOrDefaultAsync();
                    if (programmazione == null)
                    {
                        Programmazione nuovaProgrammazione = new Programmazione
                        {
                            DataGiorno = richiesta.DataRichiesta,
                            InizioTurno = richiesta.DataRichiesta,
                            FineTurno = richiesta.DataRichiesta,
                            IdDipendente = dipendente.IdDipendente,
                            GiornoPermesso = true
                        };
                        dbContext.Programmazioni.Add(nuovaProgrammazione);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        programmazione.GiornoPermesso = true;
                    }
                    dbContext.SaveChanges();
                }
            }
            else if (tipoRichiesta == "MALATTIA")
            {
                Console.WriteLine("siamo nella scelta malattia");
                if (esitoRichiesta == "accettata")
                {
                    Richiesta richiesta = await dbContext.Richieste
                       .Where(r => r.IdRichiesta == idRichiesta && r.IdDipendente == idDipendente)
                       .FirstOrDefaultAsync();
                    richiesta.Archiviato = true;
                    richiesta.Confermato = true;
                    Programmazione programmazione = await dbContext.Programmazioni
                       .Where(p => p.DataGiorno.Date == richiesta.DataRichiesta.Date && p.IdDipendente == idDipendente)
                       .FirstOrDefaultAsync();
                    if (programmazione == null)
                    {
                        Programmazione nuovaProgrammazione = new Programmazione
                        {
                            DataGiorno = richiesta.DataRichiesta,
                            InizioTurno = richiesta.DataRichiesta,
                            FineTurno = richiesta.DataRichiesta,
                            IdDipendente = dipendente.IdDipendente,
                            GiornoMalattia = true
                        };
                        dbContext.Programmazioni.Add(nuovaProgrammazione);
                        dbContext.SaveChanges();
                    }
                    else
                    {
                        programmazione.GiornoMalattia = true;
                    }
                    dbContext.SaveChanges();
                }
            }
            else if (tipoRichiesta == "rifiuto")
            {
                Richiesta richiesta = await dbContext.Richieste
                   .Where(r => r.IdRichiesta == idRichiesta && r.IdDipendente == idDipendente)
                   .FirstOrDefaultAsync();
                richiesta.Archiviato = true;
                richiesta.Confermato = false;
                dbContext.SaveChanges();
            }
            dipendente.Richieste = await dbContext.Richieste
                .Where(r => r.IdDipendente == dipendente.IdDipendente)
                .ToListAsync();
            return DipendenteViewModel.FromEntity(dipendente);
        }

        public async Task<DipendenteViewModel> GestisciProgrammazione(int idDipendente, int IdProgrammazione, int idAzienda, int giorno, int mese, int anno, string inizioTurno, string FineTurno, bool giornoFerie, bool giornoPermesso, bool giornoMalattia)
        {


            Dipendente dipendente = await dbContext.Dipendenti
                .Where(d => d.IdDipendente == idDipendente && d.IdAzienda == idAzienda)
                .FirstOrDefaultAsync();
            DateTime dataGiorno = new DateTime(anno, mese, giorno);
            var timePartsInizio = inizioTurno.Split(':');
            int oraInzio = int.Parse(timePartsInizio[0]);
            int minutiInizio = int.Parse(timePartsInizio[1]);
            var timePartsFine = FineTurno.Split(':');
            int oraFine = int.Parse(timePartsFine[0]);
            int minutiFine = int.Parse(timePartsFine[1]);
            DateTime datetimeInizio = new DateTime(anno, mese, giorno, oraInzio, minutiInizio, 0);
            DateTime datetimeFine = new DateTime(anno, mese, giorno, oraFine, minutiFine, 0);

            bool nuovaCreazione = IdProgrammazione == 0 ;
            Programmazione programmazione;
            if (nuovaCreazione)
            {
                // Creazione di una nuova programmazione
                programmazione = new Programmazione(idDipendente, dataGiorno, datetimeInizio, datetimeFine, giornoFerie, giornoPermesso, giornoMalattia);
                dbContext.Programmazioni.Add(programmazione);
                Console.WriteLine("ho creato nuova");
            }
            else
            {

                programmazione = await dbContext.Programmazioni.FindAsync(IdProgrammazione);
                programmazione.InizioTurno = datetimeInizio;
                programmazione.FineTurno = datetimeFine;
                programmazione.GiornoFerie = giornoFerie;
                programmazione.GiornoPermesso = giornoPermesso;
                programmazione.GiornoMalattia = giornoMalattia;
                dbContext.Programmazioni.Update(programmazione);
                Console.WriteLine("ho tentato la modifica");
            }
            dbContext.SaveChanges();
            dipendente.Programmazioni = await dbContext.Programmazioni
                .Where(p => p.IdDipendente == idDipendente)
                .ToListAsync();
            return DipendenteViewModel.FromEntity(dipendente, mese, anno);
        }
    }
}