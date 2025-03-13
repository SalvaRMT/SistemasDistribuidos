using Microsoft.AspNetCore.Mvc;
using PokedexApi.Services;
using PokedexApi.Mappers;
using PokedexApi.Dtos;


namespace PokedexApi.AddControllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PokemonsController : ControllerBase
{
    private readonly IPokemonService _pokemonService;

    public PokemonsController(IPokemonService pokemonService)
    {
        _pokemonService = pokemonService;
    }


    //localhost/api/v1/pokemons/12971293-1283812
  [HttpGet("{id}")]
  public async Task<ActionResult<PokemonResponse>> GetPokemonById(Guid id, CancellationToken cancellationToken)
  {
    var pokemon = await _pokemonService.GetPokemonById(id, cancellationToken);
    if (pokemon is null){
    return NotFound();
    }
    return Ok(pokemon.ToDto());
  }
  

}