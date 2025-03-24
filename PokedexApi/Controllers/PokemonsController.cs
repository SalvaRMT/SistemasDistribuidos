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
    //localhost/api/v1/pokemons/NAME ---esto no esta bien
    //localhost/api/v1/pokemons/?name=NOMBRE ---esto esta bien, el "?" indica que son query parameters
    //---mala practica---
   /* [HttpGet("name/{name}")]
    public async Task<ActionResult<List<PokemonResponse>>> GetPokemonByName(string name, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonByNameAsync(name, cancellationToken);
        if (pokemon == null){
            return NotFound();
        }
        return Ok(pokemon.ToDtoList());
    }*/

    //---Buena practica---
    [HttpGet]
    public async Task<ActionResult<List<PokemonResponse>>> GetPokemonByName([FromQuery]string name, CancellationToken cancellationToken)
    {
        var pokemon = await _pokemonService.GetPokemonByNameAsync(name, cancellationToken);
        if (pokemon == null){
            return NotFound();
        }
        return Ok(pokemon.ToDtoList());
    }

    [HttpDelete("{id}")]
    //204 - NoContent (Se encontro y se elimino el pokemon de manera correcta pero el body de respuesta esta vacio)
    //200 - ok (se encontro y se elimino y en el body de respuesta se manda un mensaje de exito)
    public async Task<ActionResult> DeletePokemonById(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _pokemonService.DeletePokemonByIdAsync(id, cancellationToken);
        if (deleted){
            return NoContent(); //204
        }
        return NotFound(); //404
    }
}