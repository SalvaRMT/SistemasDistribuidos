using Microsoft.AspNetCore.Mvc;
using PokedexApi.Services;
using PokedexApi.Dtos;
using PokedexApi.Mappers;
namespace PokedexApi.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class PokemonsController : ControllerBase
{
    private readonly IPokemonService _pokemonService;
    public PokemonsController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }
    //localhost/api/v1/pokemons/123524-1234
    //localhost/api/v1/pokemons?name=nombre
    [HttpGet("{id}")]
    public async Task<ActionResult<PokemonResponse>> GetPokemonById(Guid id, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonById(id, cancellationToken);
        if (pokemon == null){
            return NotFound();
        }
        return Ok(pokemon.ToDto());
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult<List<PokemonResponse>>> GetPokemonByName(string name, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonByNameAsync(name, cancellationToken);
        if (pokemon == null){
            return NotFound();
        }
        return Ok(pokemon.ToDtoList());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePokemonById(Guid id, CancellationToken cancellationToken)
    {
        var delete = await _pokemonService.DeletePokemonByIdAsync(id, cancellationToken);
        if (delete)
        {
            return NoContent();//204
        }
        //await _pokemonService.DeletePokemonAsync(id, cancellationToken);
        return NotFound();//404
    }
}