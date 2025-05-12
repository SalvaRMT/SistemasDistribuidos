using Microsoft.AspNetCore.Mvc;
using PokedexApi.Dtos;
using PokedexApi.Mappers;
using PokedexApi.Models;
using PokedexApi.Services;
namespace PokedexApi.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class HobbiesController : ControllerBase
{
    private readonly IHobbiesService _hobbiesService;
    public HobbiesController(IHobbiesService hobbiesService)
    {
        _hobbiesService = hobbiesService;
    }
    //localhost/api/v1/hobbies/123524-1234
    [HttpGet("{id}")]
    public async Task<ActionResult<HobbiesResponse>> GetHobbiesById(Guid id, CancellationToken cancellationToken)
    {
    var hobbies = await _hobbiesService.GetHobbiesByIdAsync(id, cancellationToken);
    if (hobbies == null) {
        return NotFound();
    }
    return Ok(hobbies.ToDto());
    }

    /* MALA PRACTICA
    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<List<HobbiesResponse>>> GetHobbiesByName(string name, CancellationToken cancellationToken)
    {
        var hobbies = await _hobbiesService.GetHobbiesByNameAsync(name, cancellationToken);
        if (hobbies == null){
            return NotFound();
        }
        return Ok(hobbies.ToDtoList());
    }*/

    [HttpGet]
    public async Task<ActionResult<List<HobbiesResponse>>> GetHobbiesByName([FromQuery]string name, CancellationToken cancellationToken)
    {
        var hobbies = await _hobbiesService.GetHobbiesByNameAsync(name, cancellationToken);
        if (hobbies == null){
            return NotFound();
        }
        return Ok(hobbies.ToDtoList());
    }
     // DELETE: api/v1/hobbies/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteHobbiesById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _hobbiesService.DeleteHobbiesByIdAsync(id, cancellationToken);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
    
}