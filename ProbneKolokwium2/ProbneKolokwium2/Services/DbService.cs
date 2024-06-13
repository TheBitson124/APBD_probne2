using Microsoft.EntityFrameworkCore;
using ProbneKolokwium2.Data;
using ProbneKolokwium2.Models_DTOS;

namespace ProbneKolokwium2.Services;

public class DbService : IDbService

{
    public BoatContext Context;

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
}