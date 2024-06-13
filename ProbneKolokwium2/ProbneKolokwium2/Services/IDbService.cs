using ProbneKolokwium2.Models_DTOS;

namespace ProbneKolokwium2.Services;

public interface IDbService
{
    public Task<bool> DoesClientExist(int id);
    public Task<object> GetClientReservations(int id);
}