using Microsoft.AspNetCore.Mvc;
using ProbneKolokwium2.Data;
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
    
}