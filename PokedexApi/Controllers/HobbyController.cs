using Microsoft.AspNetCore.Mvc;
using PokedexApi.Dtos;
using PokedexApi.Mappers;
using PokedexApi.Services;

namespace PokedexApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HobbiesController : ControllerBase
{
    private readonly IHobbyService _hobbyService;

    public HobbiesController(IHobbyService hobbyService)
    {
        _hobbyService = hobbyService;
    }
    //localhost/api/v1/hobbies/123524-1234
    [HttpGet("{id}")]
    public async Task<ActionResult<HobbyResponse>> GetHobbyById(Guid id, CancellationToken cancellationToken)
    {
        var hobby = await _hobbyService.GetHobbyByIdAsync(id, cancellationToken);
        if (hobby == null){
            return NotFound();
        }
        return Ok(hobby.ToDto());
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<List<HobbyResponse>>> GetHobbyByName(string name, CancellationToken cancellationToken)
    {
        var hobby = await _hobbyService.GetHobbyByNameAsync(name, cancellationToken);
        if (hobby == null){
            return NotFound();
        }
        return Ok(hobby.ToDtoList());
    }
}