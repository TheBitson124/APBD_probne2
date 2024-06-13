using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProbneKolokwium2.Data;
using ProbneKolokwium2.Models_DTOS;
using ProbneKolokwium2.Services;

namespace ProbneKolokwium2.Controllers;
[ApiController]

public class BoatController :ControllerBase
{
    public IDbService _service;

    public BoatController(IDbService service, BoatContext context)
    {
        _service = service;
    }

    [HttpGet]
    [Route("Client/{id:int}")]
    public async Task<IActionResult> GetClient(int id)
    {
        if (!await _service.DoesClientExist(id))
        {
            return NotFound($"Client with id {id:int} not found");
        }

        return Ok(await _service.GetClientReservations(id));
    }

    [HttpPost]
    [Route(
        "api/reservtions/{idClient:int}/{dateFrom:datetime}/{dateTo:datetime}/{idBoatStandard:int}/{numOfBoats:int}")]
    public async Task<IActionResult> AddNewReservation(int idClient, DateTime dateFrom, DateTime dateTo, int idBoatStandard, int numOfBoats)
    {
        if (!await _service.DoesClientExist(idClient))
        {
            return NotFound($"Client with id {idClient:int} not found");
        }
        if (await _service.DoesClientHaveActiveReservation(idClient))
        {
            return NotFound($"Client with id {idClient:int} has an active Reservation");
        }

        int maxStandard = await _service.MaxStandard();
        int howManyWeCanGet = 0;
        for (int i = idBoatStandard; i < maxStandard; i++)
        {
            howManyWeCanGet += await _service.SailboatsFreeInDates(idBoatStandard, dateFrom, dateTo);
        }

        if (howManyWeCanGet  <= numOfBoats)
        {
            return NotFound("Not enough boats");
        }
        
        return Ok();
    }
    
}