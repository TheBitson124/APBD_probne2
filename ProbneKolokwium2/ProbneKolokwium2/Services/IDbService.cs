using System.Runtime.InteropServices.JavaScript;
using ProbneKolokwium2.Models_DTOS;

namespace ProbneKolokwium2.Services;

public interface IDbService
{
    public Task<bool> DoesClientExist(int id);
    public Task<object> GetClientReservations(int id);
    Task<bool> DoesClientHaveActiveReservation(int idClient);
    Task<bool> IsSailboatFree(int idSailboat, DateTime dateFrom, DateTime dateTo);
    Task<int> SailboatsFreeInDates(int idBoatStandard, DateTime dateFrom, DateTime dateTo);
    Task<int> MaxStandard();
}