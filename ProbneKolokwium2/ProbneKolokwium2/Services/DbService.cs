using Microsoft.EntityFrameworkCore;
using ProbneKolokwium2.Data;
using ProbneKolokwium2.Models_DTOS;

namespace ProbneKolokwium2.Services;

public class DbService : IDbService

{
    public BoatContext Context;
    private IDbService _dbServiceImplementation;

    public DbService(BoatContext context)
    {
        Context = context;
    }

    public async Task<bool> DoesClientExist(int id)
    {
        return Context.Clients.FirstOrDefault(c => c.IdClient == id) != null;
    }

    public async Task<object> GetClientReservations(int id)
    {
        var klient = Context.Clients.Include(e => e.ReservationsNavigation)
            .Include(e => e.ClientCategoryNavigation).FirstOrDefault(c => c.IdClient == id);
        if (klient == null)
        {
            throw new Exception();
        }

        var result = new
        {
            klient.IdClient,
            klient.Name,
            klient.LastName,
            klient.Birthday,
            klient.Pesel,
            klient.email,
            ClientCategory = new
            {
                klient.ClientCategoryNavigation.IdClientCategory,
                klient.ClientCategoryNavigation.Name,
                klient.ClientCategoryNavigation.DiscountPerc
            },
            Reservations = klient.ReservationsNavigation.Select(
                reservation => new
                {
                    reservation.IdReservation,
                    reservation.IdClient,
                    reservation.DateFrom,
                    reservation.DateTo,
                    reservation.IdBoatStandard,
                    reservation.Capacity,
                    reservation.NumOfBoats,
                    reservation.Fullfilled,
                    reservation.Price,
                    reservation.CancelReservation
                }).OrderByDescending(reservation => reservation.DateTo)
        };
        return result;
    }

    public async Task<bool> DoesClientHaveActiveReservation(int idClient)
    {
        return Context.Reservations.FirstOrDefault(r => r.IdClient == idClient && r.Fullfilled == false && r.CancelReservation == null) != null;
    }

    public async Task<bool> IsSailboatFree(int idSailboat, DateTime dateFrom, DateTime dateTo)
    {
        var reservations =  Context.SailboatReservations.Where(s => s.IdSailboat == idSailboat).
            Select(sr => sr.IdReservation);
        foreach (var reserv in reservations)
        {
            var reservation = Context.Reservations.FirstOrDefault(r => r.IdReservation == reserv);
            if ((dateFrom > reservation.DateFrom && dateFrom < reservation.DateTo) || (dateTo > reservation.DateFrom && dateTo < reservation.DateTo))
            {
                return false;
            }
        }
        return true;
    }

    public async Task<int> SailboatsFreeInDates(int idBoatStandard, DateTime dateFrom, DateTime dateTo)
    {
        var BoatsInStandard = Context.Sailboats.Where(s => s.IdBoatStandard == idBoatStandard);
        var freeBoats = 0;
        foreach (var boat in BoatsInStandard)
        {
            if (await IsSailboatFree(boat.IdSailboat, dateFrom, dateTo))
            {
                freeBoats += 1;
            }
        }
        return freeBoats;
    }

    public async Task<int> MaxStandard()
    {
        return Context.BoatStandards.Max(bs => bs.IdBoatStandard);
    }
}